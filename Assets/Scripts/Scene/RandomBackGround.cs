using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackGround : MonoBehaviour
{
    public Image BackGround;

    public Sprite[] BackGroundImages;

    private void Awake()
    {
        BackGround.sprite = BackGroundImages[Random.Range(0, BackGroundImages.Length)];
    }
}
