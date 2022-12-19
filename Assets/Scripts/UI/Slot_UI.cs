using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_UI : MonoBehaviour
{
    public int slotID;
    public Inventory inventory;

    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    [SerializeField] Image highlight;

    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            itemIcon.sprite = SpriteDataProvider.Instance.GetIconByID(slot.iconID);
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
    }

    public void Setempty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    public void SetHighlight(bool isOn)
    {
        highlight.gameObject.SetActive(isOn);
    }
}
