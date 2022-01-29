using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] List<Item> itemList;
    [SerializeField] int maxItems;

    public List<Item> ItemList => itemList;

    public bool AddItemToInventory(Item itemToAdd)
    {
        bool canAddItem = itemList.Count < maxItems;

        if (canAddItem)
        {
            itemList.Add(itemToAdd);
        }

        return canAddItem;
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
            if (itemMatches.Equals(item.Recipe))
            {
                itemFound = true;
            }
        }

        return itemFound;
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
