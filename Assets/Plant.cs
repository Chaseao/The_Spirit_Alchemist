using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] Item plantType;

    public Item CollectPlant => plantType;
}
