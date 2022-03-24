using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InvenSlot> invenSlots;


    private void Awake()
    {

        InitInventory();

    }

    void InitInventory()
    {
        for (int i = 0; i < invenSlots.Count; i++)
            invenSlots[i].SlotNumber = i;
    }

    public virtual bool AddItem(ItemType type)
    {
        for (int i = 0; i < invenSlots.Count; i++)
        {
            if (invenSlots[i].item.type == ItemType.None)
            {
                invenSlots[i].SetSlotItem(type);
                return true;
            }
        }
        return false;
    }


    public virtual bool DeleteItem(int SlotIndex)
    {
        if (invenSlots[SlotIndex].item.type != ItemType.None)
        {
            invenSlots[SlotIndex].SetSlotItem(ItemType.None);
            for (int i = SlotIndex + 1; i < invenSlots.Count; i++)
            {
                invenSlots[i - 1].SetSlotItem(invenSlots[i].item.type);
            }
            if (invenSlots[5].item.type != ItemType.None)
                invenSlots[5].SetSlotItem(ItemType.None);
            return true;
        }
        return false;
    }

    public virtual void DeleteAllItem()
    {
        for(int i = 0; i < invenSlots.Count; i++)
        {
            invenSlots[i].SetSlotItem(ItemType.None);
        }
    }
}
