using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMenu : IMenu
{
    [SerializeField] Inventory itemDatabase;
    [SerializeField] List<PageEntry> pageEntries;
    [SerializeField] int totalPages;

    int currentPage = 1;

    private void OnEnable()
    {
        ToggleMenuDisplay(false);
    }

    protected override void SetUpButtons()
    {
        menuButtons.ToggleButtons(true);
        menuButtons.buttonPressed += SwitchCurrentPage;

        DisplayPage();
    }

    private void SwitchCurrentPage(int buttonIndex)
    {
        if(buttonIndex != 0)
        {
            ChangeCurrentPage(1);
        }
        else
        {
            ChangeCurrentPage(-1);
        }

        DisplayPage();
    }

    private void ChangeCurrentPage(int pageAdjustment)
    {
        currentPage += pageAdjustment;

        if (currentPage > totalPages)
        {
            currentPage = 1;
        }
        else if (currentPage <= 0)
        {
            currentPage = totalPages;
        }
    }

    private void DisplayPage()
    {
        int pageEntriesOffset = pageEntries.Count * (currentPage - 1);
        for (int i = 0; i < pageEntries.Count; i++)
        {
            pageEntries[i].SetEntryToItem(itemDatabase.ItemList[i + pageEntriesOffset]);
        }
    }


    protected override void TearDownButtons()
    {
        menuButtons.buttonPressed -= SwitchCurrentPage;
        menuButtons.ToggleButtons(false);
    }
}
