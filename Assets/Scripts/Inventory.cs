using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public string itemName;
        public int count;
        public int maxAllowed;

        public int iconID;
        public Sprite icon;

        public Slot()
        {
            itemName = "";
            iconID = -1;
            count = 0;
            maxAllowed = 99;
        }

        public bool IsEmpty
        {
            get
            {
                if (itemName == "" && count == 0)
                {
                    return true;
                }

                return false;
            }
        }

        public bool CanAddItem(string itemName)
        {
            if (this.itemName == itemName && count < maxAllowed)
            {
                return true;
            }

            return false;
        }

        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.iconID = item.data.iconID;
            this.icon = item.data.icon;
            count++;
        }

        public void AddItem(string itemName, Sprite icon, int iconID, int maxAllowed)
        {
            this.itemName = itemName;
            this.iconID = iconID;
            this.icon = icon;
            count++;
            this.maxAllowed = maxAllowed;
        }

        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;

                if (count == 0)
                {
                    icon = null;
                    iconID = -1;
                    itemName = "";
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Item itemToAdd)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemName == itemToAdd.data.itemName && slot.CanAddItem(itemToAdd.data.itemName))
            {
                slot.AddItem(itemToAdd);
                return;
            }
        }

        foreach (Slot slot in slots)
        {
            if (slot.itemName == "")
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

    public void Remove(int index, int numToRemove)
    {
        if (slots[index].count >= numToRemove)
        {
            for (int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if (toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for (int i = 0; i < numToMove; i++)
            {
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.iconID, fromSlot.maxAllowed);
                fromSlot.RemoveItem();
            }
        }
    }

    public bool IsFull(string itemName, int maxSlotCount)
    {
        int fullSlotCount = 0;
        foreach (Slot slot in slots)
        {
            if (slot.itemName != "" && !slot.CanAddItem(itemName))
            {
                fullSlotCount++;
            }
        }

        if (fullSlotCount == maxSlotCount)
        {
            return true;
        }

        return false;

    }
}
