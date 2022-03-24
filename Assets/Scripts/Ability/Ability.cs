using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("능력치")]
    
    [Tooltip("공격력")]
    public float AttackDamage; float DefaultAddAttackDamage, PersentAddAttackDamage, PersentMinusAttackDamage;
    public float AD {
        get { return (AttackDamage + DefaultAddAttackDamage) + 
                ((AttackDamage + DefaultAddAttackDamage) * PersentAddAttackDamage - 
                (AttackDamage + DefaultAddAttackDamage) * PersentMinusAttackDamage); }
        set { AttackDamage = value; }
    }
    public float DefaultAD {
        get { return DefaultAddAttackDamage; }
        set { DefaultAddAttackDamage = value; }
    }
    public float PersentAD {
        get { return PersentAddAttackDamage; }
        set { PersentAddAttackDamage = value; }
    }
    public float PersentMinusAD
    {
        get { return PersentMinusAttackDamage; }
        set { PersentMinusAttackDamage = value; }
    }

    [Tooltip("공격 속도")]
    public float AttackSpeed; float DefaultAddAttackSpeed, PersentAddAttackSpeed, PersentMinusAttackSpeed; 
    public float AS {
        get { return (AttackSpeed + DefaultAddAttackSpeed) + 
                ((AttackSpeed + DefaultAddAttackSpeed) * PersentAddAttackSpeed - 
                (AttackSpeed + DefaultAddAttackSpeed) * PersentMinusAttackSpeed); }
        set { AttackSpeed = value; }
    }
    public float DefaultAS {
        get { return DefaultAddAttackSpeed; }
        set { DefaultAddAttackSpeed = value; }
    }
    public float PersentAS {
        get { return PersentAddAttackSpeed; }
        set { PersentAddAttackSpeed = value; }
    }
    public float PersentMinusAS
    {
        get { return PersentMinusAttackSpeed; }
        set { PersentMinusAttackSpeed = value; }
    }


    [Tooltip("방어력")]
    public float Armor; float DefaultAddArmor, PersentAddArmor, PersentMinusArmor;
    public float DF
    {
        get { return (Armor + DefaultAddArmor) + ((Armor + DefaultAddArmor) * PersentAddArmor - (Armor + DefaultAddArmor) * PersentMinusArmor); }
        set { Armor = value; }
    }
    public float DefaultDF {
        get { return DefaultAddArmor; }
        set { DefaultAddArmor = value; }
    }
    public float PersentDF {
        get { return PersentAddArmor; }
        set { PersentAddArmor = value; }
    }
    public float PersentMinusDf
    {
        get { return PersentMinusArmor; }
        set { PersentMinusArmor = value; }
    }


    [Tooltip("회피율")]
    public float Dodge; float AddDodge;
    public float DG
    {
        get { return (Dodge + AddDodge); }
        set { Dodge = value; }
    }
    public float AddDG {
        get { return AddDodge; }
        set { AddDodge = value; }
    }

    [Tooltip("치명타 확률")]
    public float CriticalChance; float AddCriticalChance;
    public float CC
    {
        get { return (CriticalChance + AddCriticalChance); }
        set { CriticalChance = value; }
    }
    public float AddCC {
        get { return AddCriticalChance; }
        set { AddCriticalChance = value; }
    }

    [Tooltip("치명타 데미지 배율")]
    public float CriticalMultiplier; float AddCriticalMultiplier;
    public float CM {
        get { return (CriticalMultiplier + AddCriticalMultiplier); }
        set { CriticalMultiplier = value; }
    }
    public float AddCM {
        get { return AddCriticalMultiplier; }
        set { AddCriticalMultiplier = value; }
    }

    [Tooltip("최대 체력")]
    public float MaxHp; float DefaultAddMaxHp, PersentAddMaxHp, PersentMinusMaxHp;
    public float MH
    {
        get { return (MaxHp + DefaultAddMaxHp) + 
                ((MaxHp + DefaultAddMaxHp) * PersentAddMaxHp - 
                (MaxHp + DefaultAddMaxHp) * PersentMinusMaxHp); }
        set { MaxHp = value; }
    }
    public float DefaultMH {
        get { return DefaultAddMaxHp; }
        set { DefaultAddMaxHp = value; }
    }
    public float PersentMH {
        get { return PersentAddMaxHp; }
        set { PersentAddMaxHp = value; }
    }

    [Tooltip("체력")]
    public float Hp;

    [Tooltip("체력 재생")]
    public float HpRegen; float DefaultAddHpRegen, PersentAddHpRegen;
    public float HR
    {
        get { return (HpRegen + DefaultAddHpRegen) + (HpRegen + DefaultAddHpRegen) * PersentAddHpRegen; }
        set { HpRegen = value; }
    }
    public float DefaultHR {
        get { return DefaultAddHpRegen; }
        set { DefaultAddHpRegen = value; }
    }
    public float PersentHR
    {
        get { return PersentAddHpRegen; }
        set { PersentAddHpRegen = value; }
    }


    [Tooltip("공격 사거리")]
    public float AttackRange; float DefaultAddAttackRange, PersentAddAttackRange;
    public float AR
    {
        get { return (AttackRange + DefaultAddAttackRange) + (AttackRange + DefaultAddAttackRange) * PersentAddAttackRange; }
        set { AttackRange = value; }
    }
    public float DefaultAR {
        get { return DefaultAddAttackRange; }
        set { DefaultAddAttackRange = value; }
    }
    public float PersentAR {
        get { return PersentAddAttackRange; }
        set { PersentAddAttackRange = value; }
    }


    [Tooltip("이동 속도")]
    public float MovementSpeed; float DefaultAddMovementSpeed, PersentAddMovementSpeed;
    public float MS
    {
        get { return (MovementSpeed + DefaultAddMovementSpeed) + (MovementSpeed + DefaultAddMovementSpeed) * PersentAddMovementSpeed; }
        set { MovementSpeed = value; }
    }
    public float DefaultMS {
        get { return DefaultAddMovementSpeed; }
        set { DefaultAddMovementSpeed = value; }
    }
    public float PersentMS
    {
        get { return PersentAddMovementSpeed; }
        set { PersentAddMovementSpeed = value; }
    }

    protected bool death = false;
    public bool Shield = false;
    public virtual void Awake() { }
    public virtual void Start() { Hp = MaxHp; }
    public virtual void Update() {
        if (death)
            return;
        Hp += HR * Time.deltaTime;
        if (Hp > MH)
            Hp = MH;
    }

    public bool GetRandom(float Range)
    {
        return Random.Range(0, 100) > Range ? false : true; 
    }


    public virtual bool Damaged(Ability Enemy)
    {
        if (Shield == true)
            return true;

        if (GetRandom(DG))
            return false;
        if (GetRandom(Enemy.CC))
        {
            Hp = Hp - ((Enemy.AD * Enemy.CM) - DF > Enemy.AD * Enemy.CM * 0.2f ? (Enemy.AD * Enemy.CM) - DF : Enemy.AD * Enemy.CM * 0.2f);
        }
        else
        {
            Hp = Hp - (Enemy.AD - DF > Enemy.AD * 0.2f ? Enemy.AD - DF : Enemy.AD * 0.2f);
        }
        return true;
    }

    public virtual void Attack(Ability Target)
    {
        Target.Damaged(this);
    }

    public virtual void AddAbility(Ability ability)
    {
        DefaultAD += ability.DefaultAD;
        PersentAD += ability.PersentAD;
        PersentMinusAD += ability.PersentMinusAD;

        DefaultAS += ability.DefaultAS;
        PersentAS += ability.DefaultAS;
        PersentMinusAS += ability.PersentMinusAS;

        DefaultDF += ability.DefaultDF;
        PersentDF += ability.PersentDF;
        PersentMinusDf += ability.PersentMinusDf;

        AddDG += ability.AddDG;

        AddCC += ability.AddCC;
        AddCM += ability.AddCM;

        DefaultMH += ability.DefaultMH;
        PersentMH += ability.PersentMH;

        DefaultHR += ability.DefaultHR;
        PersentHR += ability.PersentHR;

        DefaultAR += ability.DefaultAR;
        PersentAR += ability.PersentAR;

        DefaultMS += ability.DefaultMS;
        PersentMS += ability.PersentMS;
    }
    public virtual void MinusAbility(Ability ability)
    {
        DefaultAD -= ability.DefaultAD;
        PersentAD -= ability.PersentAD;
        PersentMinusAD -= ability.PersentMinusAD;

        DefaultAS -= ability.DefaultAS;
        PersentAS -= ability.DefaultAS;
        PersentMinusAS -= ability.PersentMinusAS;

        DefaultDF -= ability.DefaultDF;
        PersentDF -= ability.PersentDF;
        PersentMinusDf -= ability.PersentMinusDf;

        AddDG -= ability.AddDG;

        AddCC -= ability.AddCC;
        AddCM -= ability.AddCM;

        DefaultMH -= ability.DefaultMH;
        PersentMH -= ability.PersentMH;

        DefaultHR -= ability.DefaultHR;
        PersentHR -= ability.PersentHR;

        DefaultAR -= ability.DefaultAR;
        PersentAR -= ability.PersentAR;

        DefaultMS -= ability.DefaultMS;
        PersentMS -= ability.PersentMS;
    }

    public virtual void AddAbilityReset()
    {
        DefaultAD = 0;
        PersentAD = 0;
        PersentMinusAD = 0;

        DefaultAS = 0;
        PersentAS = 0;
        PersentMinusAS = 0;

        DefaultDF = 0;
        PersentDF = 0;
        PersentMinusDf = 0;

        AddDG = 0;

        AddCC = 0;
        AddCM = 0;

        DefaultMH = 0;
        PersentMH = 0;

        DefaultHR = 0;
        PersentHR = 0;

        DefaultAR = 0;
        PersentAR = 0;

        DefaultMS = 0;
        PersentMS = 0;
    }
}