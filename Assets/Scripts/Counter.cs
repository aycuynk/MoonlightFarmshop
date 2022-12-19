using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Counter
{
    [System.Serializable]
    public class CounterSlot
    {
        public string itemName;
        public int IconID;
        public int sellPrice;

        public CounterSlot()
        {
            itemName = "";
            IconID = -1;
            sellPrice = 0;
        }

        public bool IsEmpty
        {
            get
            {
                if (itemName == "")
                {
                    return true;
                }

                return false;
            }
        }

        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.IconID = item.data.iconID;
            this.sellPrice = item.data.sellPrice;
        }

        public void RemoveItem()
        {
            if (itemName != "")
            {
                IconID = -1;
                itemName = "";
                sellPrice = 0;
            }
        }
    }

    public List<CounterSlot> slots = new List<CounterSlot>();

    public Counter(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            CounterSlot slot = new CounterSlot();
            slots.Add(slot);
        }
    }

    public void Add(Item itemToAdd)
    {
        foreach (CounterSlot slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(itemToAdd);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
}
