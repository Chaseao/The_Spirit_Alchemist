using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField] Sprite itemImage;
    [SerializeField, TextArea] string itemDescription;
    [SerializeField] List<Item> recipe;

    public Sprite ItemImage => itemImage;
    public string ItemDescription => itemDescription;
    public List<Item> Recipe => recipe;
}
