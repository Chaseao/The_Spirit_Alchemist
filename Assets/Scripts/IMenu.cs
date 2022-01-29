using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class IMenu : MonoBehaviour
{
    [SerializeField] protected ButtonController menuButtons;
    [SerializeField, ReadOnly] protected bool menuIsActive;

    public delegate void MenuDeactivated();
    public event MenuDeactivated menuDeactivated;

    public void ActivateMenu()
    {
        ControllerInput.cancel -= DeactivateMenu;
        ToggleMenuDisplay(true);
        ControllerInput.cancel += DeactivateMenu;
    }

    protected void ToggleMenuDisplay(bool menuEnabled)
    {
        menuIsActive = menuEnabled;

        foreach (Transform menuElement in transform)
        {
            menuElement.gameObject.SetActive(menuIsActive);
        }

        if (menuEnabled)
        {
            SetUpButtons();
        }
    }

    virtual protected void DeactivateMenu()
    {
        ControllerInput.cancel -= DeactivateMenu;
        ToggleMenuDisplay(false);
        TearDownButtons();
        menuDeactivated?.Invoke();
    }

    protected abstract void SetUpButtons();

    protected abstract void TearDownButtons();
}
