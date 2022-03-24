using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMgr : MonoBehaviour
{
    [Header("플레이어 케릭터")]
    public PlayerCtrl[] Player;
    public int CtrlPlayerIndex = 0;
    public bool FollowPlayer = false;

    [Header("카메라")]
    public CameraCtrl mainCam;

    public AbilityUICtrl abilityUI;

    public static PlayerCtrl[] Players;

    public GameObject[] PlayerInventorys;

    Vector3 MovePos;


    void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCtrl>();
        MainInventory.CtrlPlayerIndex = CtrlPlayerIndex;
    }

    void Start()
    {
        Players = new PlayerCtrl[Player.Length];
        for(int i = 0; i < Player.Length; i++)
        {
            Players[i] = Player[i];
        }

        //Player[CtrlPlayerIndex].Ctrl = false;
        //FollowPlayer = !FollowPlayer;

        //mainCam.TargetObject = FollowPlayer ? Player[CtrlPlayerIndex].gameObject : mainCam.StartPosition;
        //Player[CtrlPlayerIndex].Ctrl = true;
        //MainInventory.CtrlPlayerIndex = CtrlPlayerIndex;
        //UpdateInvenUI();

        //abilityUI.SetAbilityUI(Player[CtrlPlayerIndex]);

        Player[CtrlPlayerIndex].Ctrl = true;
        FollowPlayer = true;
        //mainCam.TargetObject = FollowPlayer ? Player[CtrlPlayerIndex].gameObject : mainCam.StartPosition;
        MainInventory.CtrlPlayerIndex = CtrlPlayerIndex;
        UpdateInvenUI();
        abilityUI.SetAbilityUI(Player[CtrlPlayerIndex]);
    }

    void Update()
    {
        MgrUpdate();   
    }

    private void FixedUpdate()
    {
        if (FollowPlayer)
        {
            if (Input.GetMouseButton(1))
            {
               // Player[CtrlPlayerIndex].movePos = mainCam.Target;
                Player[CtrlPlayerIndex].Move(mainCam.Target);
            }

        }
    }
    void MgrUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Player[CtrlPlayerIndex].Ctrl = false;
        //    FollowPlayer = !FollowPlayer;

        //    mainCam.TargetObject = FollowPlayer ? Player[CtrlPlayerIndex].gameObject : mainCam.StartPosition;
        //    Player[CtrlPlayerIndex].Ctrl = true;
        //    MainInventory.CtrlPlayerIndex = CtrlPlayerIndex;
        //    UpdateInvenUI();

        //    abilityUI.SetAbilityUI(Player[CtrlPlayerIndex]);
        //}

        if (Input.GetKeyDown(KeyCode.D) /*&& !EventSystem.current.IsPointerOverGameObject()*/)
        {
                Player[CtrlPlayerIndex].Ctrl = false;
                CtrlPlayerIndex = (CtrlPlayerIndex + 1) % Player.Length;
                MainInventory.CtrlPlayerIndex = CtrlPlayerIndex;
                UpdateInvenUI();
                mainCam.TargetObject = FollowPlayer ? Player[CtrlPlayerIndex].gameObject : mainCam.StartPosition;
                Player[CtrlPlayerIndex].Ctrl = true;
                abilityUI.SetAbilityUI(Player[CtrlPlayerIndex]);
            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!Players[CtrlPlayerIndex].IsDeath)
                Players[CtrlPlayerIndex].UseSkill(0, Players, CtrlPlayerIndex);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!Players[CtrlPlayerIndex].IsDeath)
                Players[CtrlPlayerIndex].UseSkill(1, Players, CtrlPlayerIndex);
        }
    }

    void UpdateInvenUI()
    {
        for (int i =  0; i < PlayerInventorys.Length; i++)
        {
            PlayerInventorys[i].SetActive(false);
        }

        PlayerInventorys[CtrlPlayerIndex].SetActive(true);
    }
}
