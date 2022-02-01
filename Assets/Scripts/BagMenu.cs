using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using PixelCrushers.DialogueSystem;

public class BagMenu : IMenu
{
    [SerializeField] ButtonController craftingButtons;
    [SerializeField] Inventory playerInventory;
    [SerializeField] Inventory itemDatabase;
    [SerializeField, ReadOnly] bool selectingSpotForCrafting = false;
    [SerializeField] DialogueSystemTrigger dialogueTriggerOne;
    [SerializeField] DialogueSystemTrigger dialogueTriggerTwo;
    [SerializeField] DialogueSystemTrigger dialogueTriggerThree;


    Item currentItemSelected;

    Dictionary<Item, int> craftingSlots = new Dictionary<Item, int>();
    List<Item> craftingIngredients = new List<Item>();

    private void OnEnable()
    {
        ToggleMenuDisplay(false);
    }

    protected override void SetUpButtons() 
    {
        SetUpBagButtons();
        craftingSlots = new Dictionary<Item, int>();
        craftingIngredients = new List<Item>();
    }

    private void SetUpBagButtons()
    {
        menuButtons.ToggleButtons(true, playerInventory.ItemList);
        menuButtons.buttonPressed += SelectItem;
    }

    private void SetUpCraftingButtons()
    {
        craftingButtons.ToggleButtons(true);
        craftingButtons.buttonPressed += SetItemInSlot;
    }

    private void SelectItem(int itemIndex)
    {
        if (itemIndex == 6)
        {
            CraftItem();
        }
        else if(!selectingSpotForCrafting)
        {
            currentItemSelected = playerInventory.ItemList[itemIndex];
            selectingSpotForCrafting = true;
            TearDownBagButtons();
            SetUpCraftingButtons();
        }
    }

    private void CraftItem()
    {
        if (craftingIngredients.Count == 3)
        {
            if (itemDatabase.ItemMatchesRecipe(craftingIngredients))
            {
                Item itemMatched = itemDatabase.GetItem(craftingIngredients);

                int triggerIndex = itemDatabase.DialogueTriggerNumber(itemMatched);

                if (triggerIndex == 0)
                {
                    dialogueTriggerOne.OnUse();
                }
                else if (triggerIndex == 1)
                {
                    dialogueTriggerTwo.OnUse();
                }
                else if (triggerIndex == 2)
                {
                    dialogueTriggerThree.OnUse();
                }

                for (int index = craftingIngredients.Count - 1; index >= 0; index--)
                {
                    playerInventory.RemoveItemFromInventory(craftingIngredients[index]);
                    RemoveItemFromCrafting(craftingIngredients[index]);
                }
                playerInventory.AddItemToInventory(itemMatched);
                TearDownBagButtons();
                SetUpBagButtons();
            }
            else
            {
                for (int index = craftingIngredients.Count - 1; index >= 0; index--)
                {
                    RemoveItemFromCrafting(craftingIngredients[index]);
                }
            }
        }
    }

    private void SetItemInSlot(int slotIndex)
    {
        Debug.Log("Putting item " + currentItemSelected + " in slot " + slotIndex);
        
        if (craftingSlots.ContainsKey(currentItemSelected))
        {
            RemoveItemFromCrafting(currentItemSelected);
        }
        
        if (craftingSlots.ContainsValue(slotIndex))
        {
            FindAndRemoveItemInSlot(slotIndex);
        }

        AddItemToCrafting(currentItemSelected, slotIndex);
        DeactivateMenu();
    }

    private void FindAndRemoveItemInSlot(int slotIndex)
    {
        Item itemMatch = null;
        foreach(KeyValuePair<Item, int> itemPair in craftingSlots)
        {
            if(itemPair.Value == slotIndex)
            {
                itemMatch = itemPair.Key;
            }
        }
        RemoveItemFromCrafting(itemMatch);
    }

    private void AddItemToCrafting(Item item, int slotIndex)
    {
        craftingIngredients.Add(item);
        craftingSlots.Add(item, slotIndex);
        craftingButtons.SetButtonImage(slotIndex, item.ItemImage);
    }

    private void RemoveItemFromCrafting(Item item)
    {
        craftingButtons.SetButtonImage(craftingSlots[item], null);
        craftingSlots.Remove(item);
        craftingIngredients.Remove(item);
    }

    protected override void TearDownButtons()
    {
        TearDownBagButtons();
        TearDownCraftingButtons();
    }

    private void TearDownBagButtons()
    {
        menuButtons.buttonPressed -= SelectItem;
        menuButtons.ToggleButtons(false);
    }

    private void TearDownCraftingButtons()
    {
        craftingButtons.buttonPressed -= SetItemInSlot;
        craftingButtons.ToggleButtons(false);
    }

    protected override void DeactivateMenu()
    {
        if (!selectingSpotForCrafting)
        {
            base.DeactivateMenu();
        }
        else
        {
            selectingSpotForCrafting = false;
            TearDownCraftingButtons();
            SetUpBagButtons();
        }
    }

    private void OnDisable()
    {
        selectingSpotForCrafting = false;
        TearDownButtons();
    }
}
