using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawn_Mgr : MonoBehaviour
{
    [Header("몬스터 프리펩")]
    public GameObject[] Monsters;
    public GameObject[] Boss;
    public GameObject WaveBoss;

    public Text WaveText;

    public MainInventory mainInven;

    [Header("스폰 옵션")]
    [Tooltip("스폰 반경")]
    public float SpawnRange;
    public float SpawnInRange;
    public float SpawnDelay;
    [Tooltip("무리 개수")]
    public int GroupCount = 10;
    int MendagiumCount = 7;
    bool SpawnEnd = false;
    public int WaveCount = 1;



    public List<MonsterCtrl> WorldMonsters;

    public GameObject Lux;

    void Start()
    {
        InvokeRepeating("ChackWorldMonsters", 1, 1f);
        InvokeRepeating("SerchMonsterTarget", 0, 0.3f);
        WorldMonsters = new List<MonsterCtrl>(); 
        StartWave(WaveCount, 10);
    }

    void Update()
    {
        if (SpawnEnd && WorldMonsters.Count == 0)
        {
            SpawnEnd = false;
            WaveCount += 1;
            StartWave(WaveCount, 10);

        }

        
    }

    public void ChackWorldMonsters()
    {
        for (int i = 0; i < WorldMonsters.Count; i++)
        {
            if (!WorldMonsters[i])
            {

                //WorldMonsters.RemoveAt(i);
            }
        }
    }

    public void StartWave(int Wave, float WaitTime)
    {
        StartCoroutine(WaveSpawn(Wave, WaitTime));
    }

    IEnumerator WaveSpawn(int Wave, float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        WaveText.text = "Wave " + Wave;
        GroupCount = Wave / 2 + 10;
        if(Wave % 5 == 0)
            SpawnMonster(WaveBoss, new Vector3(100, 1, 100));
        for (int i = 0; i < GroupCount; i++)
        {
            Vector3 pos, posin;
            pos.x = Mathf.Cos(Random.Range(0, 360f) * Mathf.Deg2Rad) * SpawnRange;
            pos.z = Mathf.Sin(Random.Range(0, 360f) * Mathf.Deg2Rad) * SpawnRange;
            pos.y = 1;

            int MonsterNum = Random.Range(0, Monsters.Length);


            SpawnMonster(Boss[Random.Range(0, Boss.Length)], pos);

            for (int ii = 0; ii < MendagiumCount + (int)(Wave * 0.35); ii++)
            {
                posin.x = Mathf.Cos(Random.Range(0, 360f) * Mathf.Deg2Rad) * Random.Range(0, SpawnInRange) * 0.3F;
                posin.z = Mathf.Sin(Random.Range(0, 360f) * Mathf.Deg2Rad) * Random.Range(0, SpawnInRange) * 0.3F;
                posin.y = 1;


                SpawnMonster(Monsters[MonsterNum], Lux.transform.position + pos + posin);
            }
            yield return new WaitForSeconds(SpawnDelay);
        }

        SpawnEnd = true;
    }

    void Spawn()
    {
        Vector3 pos, posin;
        pos.x = Mathf.Cos(Random.Range(0, 360f) * Mathf.Deg2Rad) * SpawnRange;
        pos.z = Mathf.Sin(Random.Range(0, 360f) * Mathf.Deg2Rad) * SpawnRange;
        pos.y = 1;

        int MonsterNum = Random.Range(0, Monsters.Length);


        SpawnMonster(Boss[Random.Range(0, Boss.Length)], pos);

        for (int i = 0; i < MendagiumCount; i++)
        {
            posin.x = Mathf.Cos(Random.Range(0, 360f) * Mathf.Deg2Rad) * Random.Range(0, SpawnInRange);
            posin.z = Mathf.Sin(Random.Range(0, 360f) * Mathf.Deg2Rad) * Random.Range(0, SpawnInRange);
            posin.y = 1;

            SpawnMonster(Monsters[MonsterNum], Lux.transform.position + pos + posin);
        }
    }

    void SpawnMonster(GameObject MonsterPrefab, Vector3 MonsterPosition)
    {
        GameObject mob = Instantiate(MonsterPrefab);
        mob.transform.position = MonsterPosition;
        MonsterCtrl MobCtrl = mob.GetComponent<MonsterCtrl>();
        MobCtrl.removeMonster += DeleteMonsterCtrl;
        WorldMonsters.Add(MobCtrl);
    }

    void SerchMonsterTarget()
    {
        for (int i = 0; i < WorldMonsters.Count; i++)
        {
            if (WorldMonsters[i])
            {
                WorldMonsters[i].ChackPlayer(PlayerMgr.Players);
            }
        }
    }

    void DeleteMonsterCtrl(MonsterCtrl mnC)
    {
        WorldMonsters.Remove(mnC);
        if (Random.Range(0, 100) < 1)
            mainInven.mainInven.AddItem((ItemType)Random.Range(0, (float)ItemType.Bless));
    }
}
