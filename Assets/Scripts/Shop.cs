using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop
{
    [System.Serializable]
    public class ShopSlot
    {
        public string title;
        public int price;

        public Sprite icon;

        public ShopSlot()
        {
            title = "";
            price = 0;
        }
    }

    public List<ShopSlot> slots = new List<ShopSlot>();

    public Shop(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            ShopSlot slot = new ShopSlot();
            slots.Add(slot);
        }
    }
}
