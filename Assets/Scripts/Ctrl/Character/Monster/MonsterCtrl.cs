using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonsterAbility
{
    [Header("몬스터 컨트롤 옵션")]
    public bool IsDeath = false;
    public bool IsAttack = false;
    public bool InAttackRange = false;
    public bool IsFreez = false;

    public NavMeshAgent nav;

    public GameObject Target;
    public Ability TargetAbility;
    public GameObject Lux;
    public MonsterSpriteCtrl M_Sp_C;

    public delegate void RemoveMonster(MonsterCtrl mnC);
    public RemoveMonster removeMonster;

    public Vector3 Scale;

    public override void Awake()
    {

        Lux = GameObject.FindGameObjectWithTag("LUX");
        Target = Lux;
        nav = GetComponent<NavMeshAgent>();
        Scale = transform.localScale;
        M_Sp_C = transform.GetComponentInChildren<MonsterSpriteCtrl>();
        Hp = MaxHp;
    }

    public override void Start()
    {
        nav.enabled = true;
        nav.speed = MovementSpeed;
    }

    public override void Update()
    {
        base.Update();
        MonsterUpdate();
    }

    public virtual void MonsterUpdate()
    {
        if (Hp <= 0)
        {
            Death();
        }

        MonsterSpriteUpdate();
        MonsterNavUpdate();
    }

    public void ChackPlayer(PlayerCtrl[] pc)
    {
        float Distance = TargetArea * TargetArea + 1;
        bool PlayerSet = false;
        for (int i = 0; i < pc.Length; i++)
        {
            
            if (!pc[i].IsDeath && 
                Vector3.SqrMagnitude(pc[i].transform.position - transform.position) < TargetArea * TargetArea &&
                Vector3.SqrMagnitude(pc[i].transform.position - transform.position) < Distance)
            {
                Distance = Vector3.SqrMagnitude(pc[i].transform.position - transform.position);
                Target = pc[i].gameObject;
                PlayerSet = true;
            }
        }
        if (!PlayerSet)
            Target = Lux;
    }

    void MonsterSpriteUpdate()
    {
        if ((Target.transform.position - transform.position).x > 0)
        {
            Scale.x = -Mathf.Abs(transform.localScale.x);
            transform.localScale = Scale;
        }
        else if ((Target.transform.position - transform.position).x < 0)
        {
            Scale.x = Mathf.Abs(transform.localScale.x);
            transform.localScale = Scale;
        }
    }

    void MonsterNavUpdate()
    {
        bool InRange = Vector3.SqrMagnitude(Target.transform.position - transform.position) > AttackRange * AttackRange;

        if (!IsAttack && !IsDeath && !IsFreez && InRange)
        {
            nav.SetDestination(Target.transform.position);
            nav.isStopped = false;
            M_Sp_C.SetBool("InAttackRange", false);
        }
        else if (!IsFreez && !IsAttack)
        {
            nav.isStopped = true;

            M_Sp_C.SetBool("InAttackRange", true);
            _Attack();
        }
    }

    public override bool Damaged(Ability Enemy)
    {
        if (base.Damaged(Enemy))
        {
            Freez(StunTime);
            return true;
        }
        else
            return false;
    }

    public void Freez(float Seconds)
    {
        if (Seconds <= 0)
            return;

        CancelInvoke();
        IsFreez = true;
        nav.isStopped = true;
        M_Sp_C.anim.speed = 0;
        Invoke("Melt", Seconds);
    }

    public void Melt()
    {
        IsFreez = false;
        if (!IsAttack)
            nav.isStopped = false;
        M_Sp_C.anim.speed = 1;
    }

    public virtual void Death()
    {
        if (IsDeath)
            return;
        IsDeath = true;
        gameObject.layer = 0;
        nav.isStopped = true;
        removeMonster(this);
        if (IsSmallBoss)
            SoundCtrl.sc.PlaySound(SoundType.DeathBoss);
        M_Sp_C.Death();
    }
    public void Destroy_Monster()
    {
        Destroy(gameObject);
    }

    public void _Attack()
    {
        if (IsDeath || IsAttack)
            return;
        M_Sp_C.Attack();
    }

    public void AttackTarget()
    {
        Attack(Target.GetComponent<Ability>());
    }
}
