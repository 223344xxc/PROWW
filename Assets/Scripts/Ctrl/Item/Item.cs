using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    None,
    Heart,
    Mask,
    Gloves,
    Helmet,
    Knife,
    Song,
    Wind,
    Bless,
}

public class Item : PlayerAbility
{
    public ItemType type;
    public string ItemName;
    public string ItemText;
    public Sprite ItemImage;
    public StaticItem sti;

    public ActiveSlotCtrl ActiveShadow;

    public bool SkillActive = false;

    public float CollTime = 0;

    SoundCtrl sc;
    public Item(ItemType type)
    {
        this.type = type;

        sti = GameObject.FindWithTag("MainCamera").GetComponent<StaticItem>();
        SetItem(this.type);
    }


    public void SetItem(ItemType type)
    {
        if (!sti)
            sti = GameObject.FindWithTag("MainCamera").GetComponent<StaticItem>();
        this.type = type;
        SkillActive = false;
        //ItemName = Name;
        //ItemText = Text;
        ItemImage = sti.ItemImage[(int)this.type];
        CollTime = 0;
         AddAbilityReset();
        switch (type)
        {
            case ItemType.Heart:
                MH = 120;
                PersentMH = 0.07F;
                DefaultAddRespawnTime = -7;
                PersentAddReSpawnHp = 0.05F;
                break;
            case ItemType.Mask:
                AddCC += 0.2F;
                AddCM -= 0.1f;
                AddDG += 10;
                break;
            case ItemType.Gloves:
                AddCC += 0.4F;
                AddCM += 0.15f;
                break;
            case ItemType.Helmet:
                DefaultHR += 15;
                PersentMinusDf = 0.33F;
                DefaultAddHitDisableTime += 0.03f;
                DefaultAddHitDisableSpeed += 0.05f;
                break;
            case ItemType.Knife:
                PersentAD += 0.25f;
                break;
            case ItemType.Song:
                SkillActive = true;
                DefaultMS += 0.5f;
                DefaultAS += -0.15F;
                CollTime = 14;
                break;
            case ItemType.Wind:
                SkillActive = true;
                DefaultAD += 30;
                PersentAD += 0.1F;
                DefaultMS = 0.1f;
                CollTime = 22;
                break;
            case ItemType.Bless:
                SkillActive = true;
                DefaultAD += 25;
                DefaultAS += 0.15f;
                CollTime = 17;
                break;
            default:
                SkillActive = false;
                break;
        }
        ActiveShadow = transform.GetComponentInChildren<ActiveSlotCtrl>(); 
        
    }

    void InputItem(PlayerCtrl player)
    {
        player.AddAbility(this);
    }

    void OutPutItem(PlayerCtrl player)
    {
        player.MinusAbility(this);
    }

    public void UseSkill(PlayerCtrl[] Users, int PlayerIndex)
    {
        if (ActiveShadow.TempTime <= 0)
        {
            ActiveShadow.ShadowImage.fillAmount = 1;
            ActiveShadow.TempTime = CollTime;
            switch (type)
            {
                case ItemType.Song:
                    StartCoroutine(Song(Users));
                    break;
                case ItemType.Wind:
                    StartCoroutine(Wind(Users[PlayerIndex]));
                    break;
                case ItemType.Bless:
                   Bless(Users);
                    break;
            }
        }
    }


    IEnumerator Song(PlayerCtrl[] Users)
    {
        SoundCtrl.sc.PlaySound(SoundType.Song);
        for (int i = 0; i < Users.Length; i++)
        {
            Users[i].DefaultAS -= 0.5f;
            Users[i].DefaultMS += 2;
            Users[i].abilityCtrl.SetAbilityUI(Users[i]);
            Instantiate(StaticItem.Skill_Effects[0]).transform.position = Users[i].transform.position;
        }

        yield return new WaitForSeconds(4);

        for (int i = 0; i < Users.Length; i++)
        {
            Users[i].DefaultAS += 0.5f;
            Users[i].DefaultMS -= 2;
            Users[i].abilityCtrl.SetAbilityUI(Users[i]);
        }
    }


    IEnumerator Wind(PlayerCtrl User)
    {
        SoundCtrl.sc.PlaySound(SoundType.Wind);
        Collider[] colls = Physics.OverlapSphere(User.transform.position, 12.5f, 1 << 9);
        MonsterCtrl[] MonsterCtrls;
        MonsterCtrls = new MonsterCtrl[colls.Length];
        float[] Distance = new float[colls.Length];
        for(int i = 0; i < colls.Length; i++)
        {
            Distance[i] = Vector3.Distance(colls[i].transform.position, User.transform.position);
            MonsterCtrls[i] = colls[i].GetComponent<MonsterCtrl>();
        }

        Instantiate(StaticItem.Skill_Effects[1]).transform.position = User.transform.position;
        for (float ii = 0; ii < 0.5f; ii += Time.deltaTime)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                if (!MonsterCtrls[i].IsBoss)
                {
                    colls[i].transform.Translate((colls[i].transform.position - User.transform.position).normalized * Distance[i] * (Time.deltaTime * 2));
                    float AddAd = User.AD * 0.5f;

                    User.DefaultAD += AddAd;

                    User.Attack(MonsterCtrls[i]);

                    User.DefaultAD -= AddAd;
                }
            }
            yield return new WaitForSeconds(0);
        }
    }
    PlayerCtrl[] _Users;

    void Bless(PlayerCtrl[] Users)
    {
        SoundCtrl.sc.PlaySound(SoundType.Bless);
        GameObject[] effect = new GameObject[3];

        for (int i = 0; i < Users.Length; i++)
        {
            Users[i].DefaultAD += 77f;
            Users[i].abilityCtrl.SetAbilityUI(Users[i]);
            _Users = Users;
            effect[i] = Instantiate(StaticItem.Skill_Effects[2], Users[i].transform);
            effect[i].transform.position = Users[i].transform.position;
            Destroy(effect[i], 2.5f);
            Users[i].Shield = true;
        }


        Invoke("Del", 2.5f);
    }

    void Del()
    {
        for (int i = 0; i < _Users.Length; i++)
        {
            _Users[i].Shield = false;
            _Users[i].DefaultAD -= 77f;
            _Users[i].abilityCtrl.SetAbilityUI(_Users[i]);

            Instantiate(StaticItem.Skill_Effects[3]).transform.position = _Users[i].transform.position;

        }
    }

}
