using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAbility : Ability
{
    [Header("몬스터 능력치")]
    [Tooltip("타겟팅 범위")]
    public float TargetArea;
    [Tooltip("피격 경직 시간")]
    public float StunTime;

    public bool IsBoss = false;

    public bool IsSmallBoss = false;
}
