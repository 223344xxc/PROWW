using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpriteCtrl : MonoBehaviour
{
    public GameObject Monster;
    public Animator anim;
    public MonsterCtrl Mn_Ctrl;
    delegate void WaveBossSkill();

    WaveBossSkill Skill;

    public void SetAttackTo_False() {
        Mn_Ctrl.IsAttack = false;
    }
    public void SetAttackTo_True() {
        Mn_Ctrl.IsAttack = true;
    }
    public void Attack() {
        anim.Play("Attack");
    }
    public void Death() {
        anim.Play("Death");
    }
    public void Event_Destroy() {
        Destroy(Monster);
    }
    public void SetBool(string BOOL, bool Value) {
        anim.SetBool(BOOL, Value);
    }
    public void AnimStop() {
        anim.speed = 0;
    }
    public void AnimRelease() {
        anim.speed = 1;
    }
    public void AnimWait(float Seconds)
    {
        AnimStop();
        Invoke("AnimRelease", Seconds);
    }

    public void UseSkill() {
        Skill();
    }

    public void AttackEvent()
    {
        Mn_Ctrl.AttackTarget();
    }
}
