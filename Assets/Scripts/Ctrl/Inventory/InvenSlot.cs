using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvenSlot : MonoBehaviour
{
    public Item item;
    public Image slotImage;

    public int SlotNumber = 0;


    public delegate void ItemFunction(int number);
    public ItemFunction MoveItem;
    
    private void Awake()
    {
       
        item = gameObject.AddComponent<Item>();
        item.SetItem(ItemType.None);
        SetSlotItem(item.type);
    }

    private void Start()
    {
    }

    public void SetSlotItem(ItemType item)
    {
        this.item.SetItem(item);
        SetSlot();
    }

    void DeleteItem()
    {
        item = null;
    }

    void SetSlot()
    {
        if (item.type != ItemType.None)
        {
            slotImage.gameObject.SetActive(true);
            slotImage.sprite = item.ItemImage;
        }
        else
        {
            slotImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        SetSlot();
    }
    public void SlotButtonDown()
    {
        PlayerHpUICtrl.UIDown = true;
        MoveItem(SlotNumber);
    }
}
