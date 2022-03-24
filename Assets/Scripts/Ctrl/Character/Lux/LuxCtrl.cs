using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LuxCtrl : Ability
{
    [Header("룩스 능력치")]
    public float AttackPersentDamage;
    public float AttackDelay;

    public Sprite[] LuxSprite;
    public float SplitTime;
    float TempTime = 0;
    int RenderIndex = 0;
    int Anchor = 1;
    SpriteRenderer Renderer;
    public GameObject LuxDeathEffect;
    public Image image;
    public EndSceneInventory EndInven;
    public override void Start()
    {
        base.Start();
        Renderer = transform.GetComponentInChildren<SpriteRenderer>();
        InvokeRepeating("BurnMonsters", AttackDelay, AttackDelay);
        LuxDeathEffect.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
        TempTime += Time.deltaTime;
        if(TempTime > SplitTime)
        {
            RenderIndex += Anchor;
            if (RenderIndex == LuxSprite.Length - 1 || RenderIndex == 0)
                Anchor = Anchor * -1;
            TempTime = 0;

            Renderer.sprite = LuxSprite[RenderIndex];
        }

        if (Hp <= 0)
        {
            StartCoroutine(endGame());
        }
    }
    
    IEnumerator endGame()
    {
        Renderer.gameObject.SetActive(false);
        LuxDeathEffect.SetActive(true);
        LuxDeathEffect.GetComponent<EffectSpriteRenderer>().IsStop = false;
        EndInven.EndGame();
        float t = 0.001f;
        image.gameObject.SetActive(true);
        while (true)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.01f * Time.deltaTime);
            if (image.color.a >= 1)
                break;
            yield return new WaitForSeconds(0);
        }
        SceneManager.LoadScene("EndScene");
    }

    void BurnMonsters()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, AttackRange, 1 << 9);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i])
            {
                MonsterAbility MonsterAbility = colls[i].GetComponent<MonsterAbility>();
                if (!MonsterAbility.IsBoss)
                    MonsterAbility.Hp -= MonsterAbility.MH * AttackPersentDamage;
                else
                    MonsterAbility.Hp -= MonsterAbility.MH * AttackPersentDamage * 0.1f;
            }
        }
    }
}
