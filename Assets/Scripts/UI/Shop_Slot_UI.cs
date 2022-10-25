using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop_Slot_UI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI title, price;

    public void SetItem(Shop.ShopSlot shopSlot)
    {
        if (shopSlot != null)
        {
            icon.sprite = shopSlot.icon;
            icon.color = new Color(1, 1, 1, 1);
            title.text = shopSlot.title.ToString();
            price.text = shopSlot.price.ToString();
        }
    }
}
