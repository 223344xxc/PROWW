using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BulletCtrl : MonoBehaviour
{
    [Header("총알 옵션")]
    [Tooltip("총알 속도")]
    public float BulletSpeed;
    [Tooltip("목표")]
    public GameObject Target;
    public Ability MobAbility;
    [Tooltip("총알 공격력")]
    public float Damage;
    [Tooltip("폭발 이펙트")]
    public GameObject ExplotionEffect;
    [Tooltip("스폰 이펙트")]
    public GameObject SpawnEffect;
    [Tooltip("이미지 각도 오프셋")]
    public float AngleOffset;

    public delegate void CallAttackTarget(Ability target);
    public CallAttackTarget CallAttack;

    public GameObject SpawnEffect_Object;
    public GameObject Image;

    public EffectSpriteRenderer SpawnEffectRenderer;
    public EffectSpriteRenderer Image_Renderer;

    public bool DrawGizmo = true;

    bool IsLunch = false;
    bool IsHit = false;

    //총알 발사 위치
    public Vector3 LunchPos;
    public Vector3 TempVector;

    public virtual void Start()
    {
        BulletInit();
    }

    public virtual void Update()
    {

        BulletUpdate();
        BulletMove();
    }

    public void BulletUpdate()
    {
        try
        {
            if (SpawnEffectRenderer.Render_End && !IsLunch)
            {
                IsLunch = true;
                Destroy(SpawnEffect_Object);
                Image.SetActive(true);
            }
        }
        catch(NullReferenceException ie)
        {
            Destroy(gameObject);
        }

    }


    public void BulletInit()
    {
        TempVector.x = Image.transform.localEulerAngles.x;
        TempVector.y = Image.transform.localEulerAngles.y;
        TempVector.z = Mathf.Atan2((Target.transform.position - transform.position).z, (Target.transform.position - transform.position).x) * Mathf.Rad2Deg - AngleOffset;

        Image.SetActive(false);
        SpawnEffect_Object = Instantiate(SpawnEffect, transform);
        SpawnEffectRenderer = SpawnEffect_Object.GetComponent<EffectSpriteRenderer>();
        SpawnEffect_Object.transform.localEulerAngles = TempVector;
        if (Image_Renderer)
        {
            Image_Renderer = Image.GetComponent<EffectSpriteRenderer>();
            Image_Renderer.IsStop = true;
        }
      
    }

    public void ChackHit()
    {
        try
        {
            if (Vector3.SqrMagnitude(Target.transform.position - transform.position) < 0.5f && !IsHit)
            {
                IsHit = true;
                Hit();
            }

        }
        catch (MissingReferenceException ie)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Hit()
    {
        CallAttack(MobAbility);
        Destroy(gameObject);
    }

    public virtual void BulletMove()
    {
        if (!IsLunch)
            return;
        try{
            transform.Translate((Target.transform.position - transform.position).normalized * Time.deltaTime * BulletSpeed);

            TempVector.x = 90; // Image.transform.localEulerAngles.x;
            TempVector.y = 0; // Image.transform.localEulerAngles.y;
            TempVector.z = Mathf.Atan2((Target.transform.position - transform.position).z, (Target.transform.position - transform.position).x) * Mathf.Rad2Deg - AngleOffset;
            //Debug.Log("TempVectorz" + TempVector.z);
            Image.transform.localEulerAngles = TempVector;  
            
            ChackHit();
        }
        catch(MissingReferenceException ie)
        {
            Destroy(gameObject);
        }
        catch (NullReferenceException iie)
        {
            Destroy(gameObject);
        }

    }
}
