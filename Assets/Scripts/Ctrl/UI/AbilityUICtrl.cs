using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum AbilityUIIndex
{
    AttackDamage,
    AttackSpeed,
    CriticalChance,
    Defence,
    Dodge,
    MovementSpeed,

}

public class AbilityUICtrl : MonoBehaviour
{
    public Text[] AbilityText;

    public void SetAbilityUI(PlayerAbility Player)
    {
        AbilityText[(int)AbilityUIIndex.AttackDamage].text = Player.AD.ToString("f0");
        AbilityText[(int)AbilityUIIndex.AttackSpeed].text = Player.AS.ToString("f2");
        AbilityText[(int)AbilityUIIndex.CriticalChance].text = Player.CC.ToString("f0") + "%" + "  |  " + Player.CM.ToString("f1");
        AbilityText[(int)AbilityUIIndex.Defence].text = Player.DF.ToString("f0");
        AbilityText[(int)AbilityUIIndex.Dodge].text = Player.DG.ToString("f0");
        AbilityText[(int)AbilityUIIndex.MovementSpeed].text = Player.MS.ToString("f1");
    }
}
