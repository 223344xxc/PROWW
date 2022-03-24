using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCtrl : PlayerAbility
{
    [Header("플레이어 옵션")]
    [Tooltip("목표 좌표")]
    public Vector3 movePos;
    [Tooltip("공격 스폰 범위")]
    public float Attack_Spawn_Range;

    [Header("플레이어 공격")]
    [Tooltip("총알 프리펩")]
    public GameObject BulletPrefab;
    [Tooltip("공격 타겟")]
    public GameObject AttackTarget;
    [Tooltip("공격 딜레이")]
    public float AttackDelay;
    [Tooltip("공격 유무")]
    public bool PassibleAttack = false;

    public bool IsDeath = false;

    public Vector3 Scale;
    public Vector3 TempVector;
    public float AccTime = 0;

    Rigidbody rigid;
    NavMeshAgent nav;
    public Animator anim;

    public bool Ctrl = false;

    public Inventory ActiveInven;
    public AbilityUICtrl abilityCtrl;

    public override void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //movePos = transform.position;
        nav = GetComponent<NavMeshAgent>();
        anim = transform.GetComponentInChildren<Animator>();
        Scale = transform.localScale;
        nav.speed = MS;
    }

    public override void Start()
    {
        base.Start();
        movePos = transform.position;
    }

    public override void Update()
    {
        base.Update();
        nav.speed = MS;
        SerchAttackTarget();
        PlayerUpdate();
    }

    void PlayerUpdate()
    {
        if (IsDeath)
            return;
        if (!AttackTarget)
            PassibleAttack = false;

        if(Hp <= 0)
        {
            Hp = 0;
            Death();
        }

        if (anim != null)
        {
            anim.SetBool("IsWalk", (transform.position - movePos).sqrMagnitude > 1 ? true : false);
        }

        if ((movePos - transform.position).x > 0)
        {
            Scale.x = -Mathf.Abs(transform.localScale.x);
            transform.localScale = Scale;
        }
        else if((movePos - transform.position).x < 0)
        {
            Scale.x = Mathf.Abs(transform.localScale.x);
            transform.localScale = Scale;
        }

        ChackAttack();
        //abilityCtrl.SetAbilityUI(this);
    }

    public virtual void ChackAttack()
    {
        if (IsDeath)
            return;

        AccTime += Time.deltaTime;
        if (PassibleAttack && AccTime > AttackDelay)
        {
            SerchAttackTarget();
            _Attack();
            AccTime = 0;
        }
    }

    public virtual void _Attack()
    {
        GameObject bullet = Instantiate(BulletPrefab);
        BulletCtrl b_c = bullet.GetComponent<BulletCtrl>();
        b_c.Target = AttackTarget;
        b_c.CallAttack += BulletAttack;
        b_c.MobAbility = AttackTarget.GetComponent<Ability>();
        TempVector.x = (Mathf.Cos(Random.Range(0, 360f)) * Attack_Spawn_Range) + transform.position.x;
        TempVector.y = transform.position.y;
        TempVector.z = (Mathf.Sin(Random.Range(0, 360f)) * Attack_Spawn_Range) + transform.position.z;
        bullet.transform.position = TempVector;


        b_c.LunchPos = bullet.transform.position;
    }

    public void BulletAttack(Ability ability)
    {
        this.Attack(ability);
    }

    public void Move(Vector3 target)
    {
        if (IsDeath)
            return;
        movePos = target;
        nav.SetDestination(target);

        anim.SetBool("IsWalk", true);
    }

    void SerchAttackTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, AR, 1<<9);
        
        if (colls.Length > 0)
            PassibleAttack = true;
        else
            PassibleAttack = false;

        float NeerDis = AR * AR;
        foreach (var coll in colls)
        {
            if(Vector3.SqrMagnitude(coll.transform.position - transform.position) < NeerDis)
            {
                AttackTarget = coll.transform.gameObject;
                NeerDis = Vector3.SqrMagnitude(coll.transform.position - transform.position);
            }
        }
    }


    public void UseSkill(int SkillIndex, PlayerCtrl[] Players, int PlayerIndex)
    {
        Debug.Log(gameObject.name);
        ActiveInven.invenSlots[SkillIndex].item.UseSkill(Players, PlayerIndex);

    }

    public void Death()
    {
        death = true;
        IsDeath = true;
        Hp = 0;
        anim.Play("Death");
        gameObject.layer = 0;
        Invoke("Respawn", RST);
    }

    public void Respawn()
    {
        death = false;
        IsDeath = false;
        Hp = RSH;
        anim.Play("Idle");
        gameObject.layer = 8;
    }
}
