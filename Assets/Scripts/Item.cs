using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;
}

public enum ItemTypes
{
    SEED,
    TOOL,
    NONE
}