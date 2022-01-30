using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] MenuTypes menuType;
    [SerializeField] bool hasConfirmButton = false;

    bool onConfirmButton = false;

    public delegate void ButtonSelected(int buttonIndex);
    public event ButtonSelected buttonSelected;
    public delegate void ButtonPressed(int buttonIndex);
    public event ButtonPressed buttonPressed;

    bool buttonsEnabled = false;
    int currentButtonSelected;

    List<UIButton> buttonList;
    List<Item> itemList;
    bool hasItems = false;

    private void OnEnable()
    {
        GenerateButtonsList();
    }

    public void ToggleButtons(bool enableButtons)
    {
        buttonsEnabled = enableButtons;
        currentButtonSelected = 0;

        if(!buttonsEnabled)
        {
            RemoveSelfFromControllerEvents();
        }
        else
        {
            AddSelfToControllerEvents();
        }

        ToggleButtonsDisplay();
    }

    public void ToggleButtons(bool enableButtons, List<Item> itemList)
    {
        this.itemList = itemList;
        hasItems = true;

        buttonsEnabled = enableButtons;
        currentButtonSelected = 0;
        RemoveSelfFromControllerEvents();

        if(buttonsEnabled)
        {
            AddSelfToControllerEvents();
        }

        ToggleButtonsDisplay();
    }

    private void GenerateButtonsList()
    {
        buttonList = new List<UIButton>();

        foreach (Transform childTransform in transform)
        {
            UIButton newButton = childTransform.GetComponent<UIButton>();

            if (newButton)
            {
                buttonList.Add(newButton);
            }
        }
    }

    private void ToggleButtonsDisplay()
    {
        for (int buttonIndex = 0; buttonIndex < buttonList.Count; buttonIndex++)
        {
            ToggleButtonDisplay(buttonIndex);
        }
    }

    private void ToggleButtonDisplay(int buttonIndex)
    {
        UIButton button = buttonList[buttonIndex];

        if (hasItems)
        {
            Sprite itemImage = null;
            if(itemList.Count > buttonIndex)
            {
                itemImage = itemList[buttonIndex].ItemImage;
            }
 
            SetButtonImage(buttonIndex, itemImage);

        }

        bool isCurrentlySelectedButton = buttonsEnabled && buttonIndex == currentButtonSelected;

        button.DisplayButton(isCurrentlySelectedButton);
    }

    public void SetButtonImage(int buttonIndex, Sprite image)
    {
        buttonList[buttonIndex].SetImage(image);
    }

    private void ActivateCurrentButton()
    {
        buttonPressed?.Invoke(currentButtonSelected);
    }

    private void SelectNewButton(Vector2 navigationDirection)
    {
        int buttonsAvailable = hasItems ? itemList.Count : buttonList.Count;
        int newButtonOffset = 0;
        int oldButtonSelected = currentButtonSelected;

        if (menuType.Equals(MenuTypes.horizontal))
        {
            newButtonOffset = Mathf.RoundToInt(navigationDirection.x);
        }
        else if (menuType.Equals(MenuTypes.vertical))
        {
            // Negative is to take into count that my brain thinks up is down and down is up
            newButtonOffset = -Mathf.RoundToInt(navigationDirection.y);
        }

        if (hasConfirmButton && navigationDirection.y != 0)
        {
            if (!onConfirmButton)
            {
                currentButtonSelected = buttonList.Count - 1;
                onConfirmButton = true;
            }
            else
            {
                currentButtonSelected = 0;
                onConfirmButton = false;
            }
        }

        if (!onConfirmButton)
        {
            currentButtonSelected += newButtonOffset;

            if (currentButtonSelected < 0)
            {
                currentButtonSelected = buttonsAvailable - 1;
            }
            else if (currentButtonSelected >= buttonsAvailable)
            {
                currentButtonSelected = 0;
            }
        }

        ToggleButtonDisplay(oldButtonSelected);
        ToggleButtonDisplay(currentButtonSelected);
        buttonSelected?.Invoke(currentButtonSelected);
    }

    private void AddSelfToControllerEvents()
    {
        ControllerInput.confirm += ActivateCurrentButton;
        ControllerInput.navigateMenu += SelectNewButton;
    }

    private void RemoveSelfFromControllerEvents()
    {
        ControllerInput.confirm -= ActivateCurrentButton;
        ControllerInput.navigateMenu -= SelectNewButton;
    }

    private void OnDisable()
    {
        RemoveSelfFromControllerEvents();
    }

    enum MenuTypes
    {
        horizontal,
        vertical
    }
}
