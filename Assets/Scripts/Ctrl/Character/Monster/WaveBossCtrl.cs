using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class WaveBossCtrl : MonsterCtrl
{
    public float SkillCoolTime;

    public float StartZPos;
    public float ReadyTime;
    public float UpTime;
    public float UpWaitTime;
    public float JumpSpeed;

    public bool IsJump = false;

    public EffectSpriteRenderer RendingEffect;
    public EffectSpriteRenderer ReadyEffect;

    public GameObject BossWarningEffect;

    GameObject[] Players;
    public PlayerCtrl[] PlayerCtrls;

    public GameObject HpBar;
    Image[] Bars;
    //SoundCtrl sc;

    public override void Start()
    {
        RendingEffect.gameObject.SetActive(false);
        ReadyEffect.gameObject.SetActive(false);

        HpBar = GameObject.FindWithTag("MainCamera").GetComponent<BossUICtrl>().BossHpBar;
        //sc = GameObject.FindWithTag("MainCamera").GetComponent<SoundCtrl>();
        HpBar.SetActive(true);
        Bars = HpBar.transform.GetComponentsInChildren<Image>();


        RendingEffect.SpriteIndex = 0;
        ReadyEffect.SpriteIndex = 0;

        RendingEffect.IsStop = true;
        ReadyEffect.IsStop = true;

        Players = GameObject.FindGameObjectsWithTag("Player");

        PlayerCtrls = new PlayerCtrl[Players.Length];
        for(int i = 0; i < PlayerCtrls.Length; i++)
        {
            PlayerCtrls[i] = Players[i].GetComponent<PlayerCtrl>();
        }

        StartZPos = M_Sp_C.transform.localPosition.z;
        base.Start();
        IsBoss = true;
        InvokeRepeating("Skill", 0, SkillCoolTime);
        //sc.PlaySound(SoundType.SpawnWaveBoss);
        SoundCtrl.sc.PlaySound(SoundType.SpawnWaveBoss);
    }

    public override void Update()
    {
        UpdateWaveBoss();
    }


    public void UpdateWaveBoss()
    {
        for(int i = 0; i <  Bars.Length;i++)
        {
            Bars[i].fillAmount = Hp / MH;
        }

        if (Hp <= 0)
        {
            HpBar.SetActive(false);
            Death();
        }



        if(RendingEffect.SpriteIndex >= RendingEffect.EffectSprite.Length - 1)
        {
            RendingEffect.IsStop = true;
            RendingEffect.gameObject.SetActive(false);
            RendingEffect.SpriteIndex = 0;
        }
        //if (ReadyEffect.SpriteIndex == RendingEffect.EffectSprite.Length - 1)
        //{

        //    ReadyEffect.IsStop = true;
        //    ReadyEffect.gameObject.SetActive(false);
        //    ReadyEffect.SpriteIndex = 0;
        //}
    }

    public virtual void Skill()
    {
        if (IsJump)
            return;

        M_Sp_C.anim.Play("ReadyJump");
        SoundCtrl.sc.PlaySound(SoundType.ReadyWaveBoss);

        IsJump = true;
        ReadyEffect.IsStop = false;
        ReadyEffect.gameObject.SetActive(true);
        ReadyEffect.SpriteIndex = 0;
        Invoke("StartJump", ReadyTime);
    }

    public void StartJump()
    {
        M_Sp_C.anim.Play("Jump");
        SoundCtrl.sc.PlaySound(SoundType.JumpWaveBoss);

        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        ReadyEffect.IsStop = true;
        ReadyEffect.gameObject.SetActive(false);
        ReadyEffect.SpriteIndex = 0;
        gameObject.layer = 0;
        for (float i = 0; i < UpTime;)
        {
            M_Sp_C.transform.Translate(0, JumpSpeed * Time.deltaTime, 0);
            i += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }

        transform.position = Players[Random.Range(0, Players.Length)].transform.position;

        GameObject Rending =  Instantiate(BossWarningEffect);
        Rending.transform.position = transform.position;
        yield return new WaitForSeconds(UpWaitTime);
        M_Sp_C.anim.Play("Rending");

        for(float i = 0; i < UpTime;)
        {
            M_Sp_C.transform.Translate(0, -JumpSpeed * Time.deltaTime, 0);
            i += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        SoundCtrl.sc.PlaySound(SoundType.RendingWaveBoss);

        RendingEffect.IsStop = false;
        RendingEffect.gameObject.SetActive(true);
        RendingEffect.SpriteIndex = 0;
        gameObject.layer = 9;

        Destroy(Rending);

        for(int i = 0; i < Players.Length; i++)
        {
            if (Vector3.SqrMagnitude(Players[i].transform.position - transform.position) < AttackRange * AttackRange)
                Attack(PlayerCtrls[i]);
        }

        M_Sp_C.AnimRelease();
        IsJump = false;
    }

    public override void Death()
    {
        if (IsDeath)
            return;
        IsDeath = true;
        gameObject.layer = 0;
        removeMonster(this);
        CancelInvoke("Skill");
        CancelInvoke("StartJump");
        StopCoroutine(Jump());
        SoundCtrl.sc.PlaySound(SoundType.DeathWaveBoss);
        M_Sp_C.Death();
    }
}
