using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneInventory : MonoBehaviour
{
    public Inventory[] PlayerInventorys;

    public ItemType[] Player1Inventorys;
    public ItemType[] Player2Inventorys;
    public ItemType[] Player3Inventorys;

    private void Start()
    {

        Player1Inventorys = new ItemType[6];
        Player2Inventorys = new ItemType[6]; 
        Player3Inventorys = new ItemType[6];

        DontDestroyOnLoad(this);
    }

    public void EndGame()
    {
        for(int i = 0; i < PlayerInventorys.Length; i++)
        {
            Player1Inventorys[i] = PlayerInventorys[0].invenSlots[i].item.type;
            Player2Inventorys[i] = PlayerInventorys[1].invenSlots[i].item.type;
            Player3Inventorys[i] = PlayerInventorys[2].invenSlots[i].item.type;
        }
    }

}
