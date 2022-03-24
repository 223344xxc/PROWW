using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventory : MonoBehaviour
{
    public Inventory mainInven;
    public PlayerInventory[] playerInven;
    public AbilityUICtrl playerUI;
    public PlayerCtrl[] playerCtrl;
    public static int CtrlPlayerIndex = 0;


    private void Awake()
    {
        for(int i = 0; i < mainInven.invenSlots.Count; i++)
        {
            mainInven.invenSlots[i].MoveItem += GiveItemToPlayer;
        }

        for (int n = 0; n < playerInven.Length; n++)
            for (int i = 0; i < playerInven[n].iteminven.invenSlots.Count; i++)
            {
                playerInven[n].iteminven.invenSlots[i].MoveItem += ReturnItem;
            }
    }

    public void Start()
    {
    }

    void GiveItemToPlayer(int index)
    {
        if (playerInven[CtrlPlayerIndex].AddItem(mainInven.invenSlots[index].item.type))
        {
            playerUI.SetAbilityUI(playerCtrl[CtrlPlayerIndex]);
            mainInven.DeleteItem(index);
        }
    }

    public void ReturnItem(int index)
    {
        //mainInven.AddItem(playerInven[CtrlPlayerIndex].iteminven.invenSlots[index].item.type); 
     
        //playerInven[CtrlPlayerIndex].Deleteitem(index); 
        //playerUI.SetAbilityUI(playerCtrl[CtrlPlayerIndex]);

    }
}
