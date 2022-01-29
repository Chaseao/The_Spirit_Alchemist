using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMenu : IMenu
{
    [SerializeField] ButtonController previousPage;
    [SerializeField] Inventory itemDatabase;
    [SerializeField] List<PageEntry> firstPageEntries;
    [SerializeField] List<PageEntry> secondPageEntries;

    int currentPage = 1;

    private void OnEnable()
    {
        ToggleMenuDisplay(false);
    }

    protected override void SetUpButtons()
    {
        SetUpNextPageButton();
    }

    private void SetUpNextPageButton()
    {
        menuButtons.ToggleButtons(true);
        menuButtons.buttonPressed += SwitchCurrentPage;
    }

    private void SetUpPreviousPageButton()
    {
        previousPage.ToggleButtons(true);
        previousPage.buttonPressed += SwitchCurrentPage;
    }

    private void SwitchCurrentPage(int buttonIndex)
    {
        if(currentPage == 1)
        {
            SetUpPreviousPageButton();
            TearDownNextPageButton();
            DisplayPageTwo();
            currentPage = 2;
        }
        else if(currentPage == 2)
        {
            SetUpNextPageButton();
            TearDownPreviousPageButton();
            DisplayPageOne();
            currentPage = 1;
        }
    }

    private void DisplayPageOne()
    {
        
    }

    private void DisplayPageTwo()
    {

    }

    private void TearDownNextPageButton()
    {
        menuButtons.buttonPressed -= SwitchCurrentPage;
        menuButtons.ToggleButtons(false);
    }

    private void TearDownPreviousPageButton()
    {
        previousPage.buttonPressed -= SwitchCurrentPage;
        previousPage.ToggleButtons(false);
    }

    protected override void TearDownButtons()
    {
        TearDownNextPageButton();
        TearDownPreviousPageButton();
    }
}
