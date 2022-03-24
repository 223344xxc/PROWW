using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimirCtrl : PlayerCtrl
{
    [Header("티미르 공격 개수")]
    public int AttackCount;
    BulletCtrl b;
    bool firstB = false;
    public override void _Attack()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, AttackRange, 1 << 9);

        //if(colls.Length > 0)
        //{
        //    int temp = 0;
        //    for(int i = 0; i < AttackCount; i++)
        //    {
        //        GameObject bullet = Instantiate(BulletPrefab);
        //        BulletCtrl b_c = bullet.GetComponent<BulletCtrl>();
        //        b_c.Target = colls[temp].gameObject;
        //        TempVector.x = (Mathf.Cos(Random.Range(0, 360f)) * Attack_Spawn_Range) + transform.position.x;
        //        TempVector.y = transform.position.y;
        //        TempVector.z = (Mathf.Sin(Random.Range(0, 360f)) * Attack_Spawn_Range) + transform.position.z;
        //        bullet.transform.position = TempVector;
        //        b_c.LunchPos = bullet.transform.position;
        //        temp = (temp + 1) % colls.Length;
        //    }
        //}
        StartCoroutine(DelayAttack());
    }

    IEnumerator DelayAttack()
    {
        for (int i = 0; i < AttackCount; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab);
            BulletCtrl b_c = bullet.GetComponent<BulletCtrl>();
            if (!firstB)
            {
                b = b_c;
                firstB = true;
            }
            b_c.Target = AttackTarget;
            //b_c.Damage = AttackDamage;
            b_c.MobAbility = AttackTarget.GetComponent<Ability>();

            b_c.CallAttack += BulletAttack;
            //TempVector.x = (Mathf.Cos(Random.Range(0, 360f)) * Attack_Spawn_Range) + transform.position.x;
            TempVector.y = transform.position.y;
            //TempVector.z = (Mathf.Sin(Random.Range(0, 360f)) * Attack_Spawn_Range) + transform.position.z;

            TempVector.x = Mathf.Cos((360 / AttackCount * i) * Mathf.Deg2Rad) * Attack_Spawn_Range + transform.position.x;
            TempVector.z = Mathf.Sin((360 / AttackCount * i) * Mathf.Deg2Rad) * Attack_Spawn_Range + transform.position.z;
            bullet.transform.position = TempVector;


            b_c.LunchPos = bullet.transform.position;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public override void Update()
    {
        base.Update();
        if (firstB == true && !b)
        {
            SoundCtrl.sc.PlaySound(SoundType.Bullet);
            firstB = false;
        }
    }
}
