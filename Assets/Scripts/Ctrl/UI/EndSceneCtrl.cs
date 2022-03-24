using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneCtrl : MonoBehaviour
{
    public EndSceneInventory endinven;

    public Inventory[] PlayerInventorys;

    void Start()
    {
        endinven = GameObject.FindWithTag("ENDINVEN").GetComponent<EndSceneInventory>();
        Init();
    }

     void Update()
    {
        
    }

    void Init()
    {
        for(int i = 0; i < endinven.Player1Inventorys.Length; i++)
        {
            PlayerInventorys[0].AddItem(endinven.Player1Inventorys[i]);
            PlayerInventorys[1].AddItem(endinven.Player2Inventorys[i]);
            PlayerInventorys[2].AddItem(endinven.Player3Inventorys[i]);
        }
    }

    public void ReturnMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
