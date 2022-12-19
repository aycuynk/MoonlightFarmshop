using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop_UI : MonoBehaviour
{
    [SerializeField] List<Shop_Slot_UI> shopSlots = new List<Shop_Slot_UI>();
    public Shop shop;

    private void Start()
    {
        shop = GameManager.instance.player.inventory.shop;
        Setup();
    }

    public void Setup()
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            if (shop.slots[i].title != "")
            {
                shopSlots[i].SetItem(shop.slots[i]);
                shopSlots[i].index = i;
            }
        }
    }
}
