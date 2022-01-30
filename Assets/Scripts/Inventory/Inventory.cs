using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] List<Item> itemList;
    [SerializeField] int maxItems;
    [SerializeField] Item questItemOne = null;
    [SerializeField] Item questItemTwo = null;
    [SerializeField] Item questItemThree = null;

    public List<Item> ItemList => itemList;

    public void AddItemToInventory(Item itemToAdd)
    {
        bool canAddItem = itemList.Count < maxItems;

        if (!canAddItem)
        {
            itemList.RemoveAt(0);
        }

        if (Contains(itemToAdd))
        {
            itemList.Remove(itemToAdd);
        }

        itemList.Add(itemToAdd);
    }

    public void RemoveItemFromInventory(Item itemToRemove)
    {
        if (itemList.Contains(itemToRemove))
        {
            itemList.Remove(itemToRemove);
        }
        else
        {
            Debug.Log("Unable to remove " + itemToRemove.name + " from " + name);
        }
    }

    public bool Contains(Item itemToFind)
    {
        return itemList.Contains(itemToFind);
    }

    public bool ItemMatchesRecipe(List<Item> itemsBeingCombined)
    {
        bool itemFound = false;

        foreach(Item item in itemList)
        {
            List<Item> itemMatches = new List<Item>();
            foreach (Item itemBeingCombined in itemsBeingCombined)
            {
                if (item.Recipe.Contains(itemBeingCombined) && !itemMatches.Contains(itemBeingCombined))
                {
                    itemMatches.Add(item);
                }
            }
            if (itemMatches.Count == 3)
            {
                itemFound = true;
            }
        }

        return itemFound;
    }

    public int DialogueTriggerNumber(Item item)
    {
        int dialogueTrigger = 0;
        if (item.Equals(questItemOne))
        {
            dialogueTrigger = 0;
        }
        else if (item.Equals(questItemTwo))
        {
            dialogueTrigger = 1;
        }
        else if (item.Equals(questItemThree))
        {
            dialogueTrigger = 2;
        }
        return dialogueTrigger;
    }

    public Item GetItem(List<Item> itemsBeingCombined)
    {
        Item itemResult = null;

        foreach (Item item in itemList)
        {
            List<Item> itemMatches = new List<Item>();
            foreach(Item itemBeingCombined in itemsBeingCombined)
            {
                if (item.Recipe.Contains(itemBeingCombined) && !itemMatches.Contains(itemBeingCombined))
                {
                    itemMatches.Add(item);
                }
            }
            if(itemMatches.Count == itemsBeingCombined.Count)
            {
                itemResult = item;
            }
        }

        return itemResult;
    }
}
