using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuxUICtrl : MonoBehaviour
{
    public LuxCtrl lux;

    public Image[] hpbar;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < hpbar.Length; i++)
        {
            hpbar[i].fillAmount = lux.Hp / lux.MH;
        }
    }
}
