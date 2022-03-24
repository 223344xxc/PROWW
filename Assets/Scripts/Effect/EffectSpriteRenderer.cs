using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpriteRenderer : MonoBehaviour
{
    [Header("스프라이트 이펙트 옵션")]
    [Tooltip("스프라이트")]
    public Sprite[] EffectSprite;
    [Tooltip("애니메이션 속도")]
    public float ChangeTime;
    [Tooltip("정지")]
    public bool IsStop;
    [Tooltip("반복")]
    public bool IsLoop;
    [Tooltip("자동 소멸")]
    public bool AutoDestroy = true;
    [Tooltip("재생 종료")]
    public bool Render_End = false;
    [Tooltip("자동 비활성화")]
    public bool AutoActiveFalse = false;

    SpriteRenderer renderer;
    public int SpriteIndex = 0;  //스프라이트 인덱스
    float AcuTime = 0;  //누적 시간

    private void OnEnable()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        EffectUpdate();
    }

    void EffectUpdate()
    {
        if (IsStop)
            return;

        AcuTime += Time.deltaTime;

        if(AcuTime >= ChangeTime)
        {
            if (!Render_End)
                SpriteIndex += 1;

            if(SpriteIndex == EffectSprite.Length - 1)
            {
                if (AutoActiveFalse)
                {
                    SpriteIndex = 0;
                    gameObject.SetActive(false);
                    return;
                }
                if (!AutoDestroy && IsLoop)
                    SpriteIndex = 0;
                else if (!IsLoop && AutoDestroy)
                    Destroy(gameObject);
                else
                    Render_End = true;
            }

            renderer.sprite = EffectSprite[SpriteIndex];
            AcuTime = 0;
        }
    }
}
