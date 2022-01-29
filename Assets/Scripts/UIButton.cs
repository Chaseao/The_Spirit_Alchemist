using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] Image grayoutImage;

    public void DisplayButton(bool shouldDisplayButton)
    {
        grayoutImage.gameObject.SetActive(!shouldDisplayButton);
    }

    public void SetImage(Sprite image)
    {
        GetComponent<Image>().sprite = image;
    }
}
