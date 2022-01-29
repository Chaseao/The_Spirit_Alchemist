using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageEntry : MonoBehaviour
{
    [SerializeField] Image pageEntryImage;
    [SerializeField] TextMeshProUGUI pageEntryText;

    public void SetEntryToItem(Item item)
    {
        pageEntryImage.sprite = item.ItemImage;
        pageEntryText.text = item.ItemDescription;
    }
}
