using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSlotCtrl : MonoBehaviour
{
    public float TempTime = 0;

    public Image ShadowImage;

    public Item item;


    public void Start()
    {
        item = gameObject.transform.GetComponentInParent<Item>();
        TempTime = 0;
        ShadowImage.fillAmount = 0;
    }

    public void Update()
    {
        if (item.CollTime == 0)
            ShadowImage.fillAmount = 0;

        if (TempTime > 0)
        {
            TempTime -= Time.deltaTime;
            ShadowImage.fillAmount = TempTime / item.CollTime;
        }
    }
}
