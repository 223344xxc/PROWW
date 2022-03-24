using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : Ability
{
    [Header("플레이어 능력치")]
    [Tooltip("부활 소요 시간")]
    public float ReSpawnTime; protected float DefaultAddRespawnTime;
    public float RST
    {
        get { return ReSpawnTime + DefaultAddRespawnTime; }
        set { ReSpawnTime = value; }
    }
    public float DefaultRST
    {
        get { return DefaultAddRespawnTime; }
        set { DefaultAddRespawnTime = value; }
    }

    [Tooltip("부활 후 체력")]
    public float ReSpawnHp; protected float PersentAddReSpawnHp;
    public float RSH
    {
        get { return ReSpawnHp + PersentAddReSpawnHp; }
        set { ReSpawnHp = value; }
    }
    public float PersentRSH
    {
        get { return PersentAddReSpawnHp; }
        set { PersentAddReSpawnHp = value; }
    }

    [Tooltip("피격 후 무적 시간")]
    public float HitDisableTime, DefaultAddHitDisableTime;
    public float HDT
    {
        get { return HitDisableTime - DefaultAddHitDisableTime; }
        set { HitDisableTime = value; }
    }
    public float DefaultHDT
    {
        get { return DefaultAddHitDisableTime; }
        set { DefaultAddHitDisableTime = value; }
    }

    [Tooltip("피격 시 이동 속도")]
    public float HitDisableSpeed, DefaultAddHitDisableSpeed;
    public float HDS
    {
        get { return HitDisableSpeed - DefaultAddHitDisableSpeed; }
        set { HitDisableSpeed = value; }
    }
    public float DefaultHDS
    {
        get { return DefaultAddHitDisableSpeed; }
        set { DefaultAddHitDisableSpeed = value; }
    }


    public void Attack(PlayerAbility Target)
    {
        base.Attack(Target);
    }

    public virtual void AddAbility(PlayerAbility ability)
    {
        base.AddAbility(ability);
        DefaultHDS += ability.DefaultHDS;
        DefaultHDT += ability.DefaultHDT;
        PersentRSH += ability.PersentRSH;
        DefaultRST += ability.DefaultRST;
    }
    public virtual void MinusAbility(PlayerAbility ability)
    {
        base.MinusAbility(ability);
        DefaultHDS -= ability.DefaultHDS;
        DefaultHDT -= ability.DefaultHDT;
        PersentRSH -= ability.PersentRSH;
        DefaultRST -= ability.DefaultRST;
    }

    public override void AddAbilityReset()
    {
        base.AddAbilityReset();
        DefaultHDS = 0;
        DefaultHDT = 0;
        PersentRSH = 0;
        DefaultRST = 0;
    }
}
