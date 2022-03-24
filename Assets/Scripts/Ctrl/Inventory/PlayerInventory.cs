using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory iteminven;
    public Inventory activeinven;
    public MainInventory mainInven;
    public PlayerCtrl player;

    int ItemCount = 0;
    private void Start()
    {
    }

    public bool AddItem(ItemType type)
    {
        int activeCount = 0;
        for (int i = 0; i < iteminven.invenSlots.Count; i++)
            if (iteminven.invenSlots[i].item.SkillActive)
                activeCount += 1;

        if (activeCount < activeinven.invenSlots.Count || (type != ItemType.Song && type != ItemType.Wind && type != ItemType.Bless))
            if (iteminven.AddItem(type))
            {
                ItemCount += 1;
                player.AddAbility(iteminven.invenSlots[ItemCount - 1].item);
                ChackActiveItem();
                return true;
            }
            else
                return false;
        else
            return false;
    }

    public void Deleteitem(int DelIndex)
    {
        if(iteminven.invenSlots[DelIndex].item.type != ItemType.None)
        {
            ItemCount -= 1;
            player.MinusAbility(iteminven.invenSlots[DelIndex].item);
        }

        iteminven.DeleteItem(DelIndex);
        
        ChackActiveItem();
    
    }

    public void ChackActiveItem()
    {
        activeinven.DeleteAllItem();

        for (int i = 0; i < iteminven.invenSlots.Count; i++)
            if (iteminven.invenSlots[i].item.SkillActive)
            {
                activeinven.AddItem(iteminven.invenSlots[i].item.type);
            }
    }
}
