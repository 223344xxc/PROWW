using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticItem : MonoBehaviour
{
    public Sprite[] ItemImage;

    public GameObject[] Effects;
    public static GameObject[] Skill_Effects;

    private void Start()
    {
        Skill_Effects = Effects;
    }
}
