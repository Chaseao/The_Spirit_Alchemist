using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MenuController : MonoBehaviour
{
    [SerializeField, ReadOnly] bool menuIsActive;

    private void OnEnable()
    {
        ControllerInput.toggleUI += ToggleMenuDisplay;
        ToggleMenuDisplay(false);
    }

    private void ToggleMenuDisplay(bool menuEnabled)
    {
        menuIsActive = menuEnabled;

        foreach(Transform menuElement in transform)
        {
            menuElement.gameObject.SetActive(menuIsActive);
        }
    }

    private void OnDisable()
    {
        ControllerInput.toggleUI -= ToggleMenuDisplay;
    }
}
