using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimBulletCtrl : BulletCtrl
{

    public override void Hit()
    {
        Object_Hit();
        base.Hit();
   
    }

    public void Object_Hit()
    {
        GameObject Temp = Instantiate(ExplotionEffect);

        Temp.transform.position = transform.position;

        TempVector.x = Temp.transform.localEulerAngles.x;
        TempVector.y = Temp.transform.localEulerAngles.y;
        TempVector.z = Random.Range(0, 360f);

        Temp.transform.localEulerAngles = TempVector;
   
    }
}
