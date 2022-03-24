using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleBoomCtrl : SplashBulletCtrl
{
    [Header("캡슐 옵션")]
    [Tooltip("캡슐 반지름")]
    public float CapsuleRadius;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Explosion()
    {
        SoundCtrl.sc.PlaySound(SoundType.WitchBoom);

        Collider[] colls = Physics.OverlapCapsule(transform.position, transform.position + ((transform.position - LunchPos).normalized * BackDistance), CapsuleRadius, 1 << 9);
        GameObject Ex_Effect = Instantiate(ExplotionEffect);
        Ex_Effect.transform.position = transform.position;
        Ex_Effect.transform.localEulerAngles = TempVector;

        foreach (var coll in colls)
        {
            //Destroy(coll.gameObject);
            //coll.GetComponent<MonsterCtrl>().Damaged(Damage);
            CallAttack(coll.GetComponent<Ability>());
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (DrawGizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + (transform.position - LunchPos).normalized * (CapsuleRadius * 0.5f), CapsuleRadius);


            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + ((transform.position - LunchPos).normalized * (BackDistance + (CapsuleRadius * 0.5f))), CapsuleRadius);
        }
    }
}
