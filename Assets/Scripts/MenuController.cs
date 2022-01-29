using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MenuController : IMenu
{
    [SerializeField] List<IMenu> subMenus;
    int currentSubMenuOpened = -1;

    private void OnEnable()
    {
        ControllerInput.toggleUI += ToggleMenuDisplay;
        ToggleMenuDisplay(false);
    }

    private void OnDisable()
    {
        DeactivateMenu();
        ControllerInput.toggleUI -= ToggleMenuDisplay;

        if (currentSubMenuOpened != -1)
        {
            subMenus[currentSubMenuOpened].menuDeactivated -= SetUpButtons;
        }
    }

    private void OpenSubMenu(int menuIndex)
    {
        if(currentSubMenuOpened != -1)
        {
            subMenus[currentSubMenuOpened].menuDeactivated -= SetUpButtons;
        }

        if(menuIndex < subMenus.Count)
        {
            currentSubMenuOpened = menuIndex;
            IMenu subMenu = subMenus[menuIndex];
            subMenu.ActivateMenu();
            subMenu.menuDeactivated += SetUpButtons;
            TearDownButtons();
        }

        if(menuIndex == 2)
        {
            Application.Quit();
            Debug.Log("Quitting Game");
        }
    }

    override protected void SetUpButtons()
    {
        menuButtons.ToggleButtons(true);
        menuButtons.buttonPressed += OpenSubMenu;
    }

    override protected void TearDownButtons()
    {
        menuButtons.buttonPressed -= OpenSubMenu;
        menuButtons.ToggleButtons(false);
    }
}
