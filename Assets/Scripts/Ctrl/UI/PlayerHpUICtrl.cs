using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUICtrl : MonoBehaviour
{
    public PlayerCtrl player;

    public Text text;
    public Image image;

    public static bool UIDown = false;

    void Update()
    {
        image.fillAmount =  player.Hp / player.MH;
        text.text = player.Hp.ToString("f0") + "/" + player.MH + " (+" + player.HR.ToString("f1") + ")";
    }
}
