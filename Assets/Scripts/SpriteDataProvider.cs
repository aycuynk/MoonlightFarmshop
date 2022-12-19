using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDataProvider : MonoBehaviour
{
    public static SpriteDataProvider Instance;
    [SerializeField] List<Sprite> itemIcons;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetIconByID(int id)
    {
        if (id >= 0)
        {
            return itemIcons[id];
        }
        return null;
    }
}
