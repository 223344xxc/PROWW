using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBulletCtrl : BulletCtrl
{
    [Header("폭발 옵션")]
    [Tooltip("폭발 반경")]
    public float SplashRadius;
    [Tooltip("폭발 데미지")]
    public float SplashDamage;
    [Tooltip("뒤로 이어지는 폭발 위치")]
    public float BackDistance;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        BulletMove();
    }

    public override void BulletMove()
    {
        base.BulletMove();
        ChackHit();
    }

    public override void Hit()
    {
        SoundCtrl.sc.PlaySound(SoundType.Boom);
        Explosion();
        //CallAttack(MobAbility);
        //Destroy(gameObject);
        base.Hit();
    }

    public virtual void Explosion()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position + ((transform.position - LunchPos).normalized * BackDistance), SplashRadius, 1 << 9);
        Instantiate(ExplotionEffect).transform.position = transform.position;
        foreach(var coll in colls)
        {
            // Destroy(coll.gameObject);
            //coll.GetComponent<MonsterCtrl>().Damaged(Damage);
            CallAttack(coll.GetComponent<Ability>());
        }
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position , SplashRadius);


    }
}
