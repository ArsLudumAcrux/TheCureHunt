using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour {

    public GameObject[] waypoints1;
    public GameObject[] waypoints2;
    public GameObject[] waypoints3;
    public Transform[] monstros;
    public int maxMonstros;
    public int maxMonstros2;
    public int maxMonstros3;


    void Start()
    {
        StartCoroutine(spawnMonster_CR());
    }

    public void SpawnMonster()
    {
        for (int i = 0; i < maxMonstros;i ++)
        {
            int intWayPoint = i;
            Instantiate(monstros[0], waypoints1[intWayPoint].transform.position, Quaternion.identity);
        }
        for (int i = 0; i < maxMonstros2; i++)
        {
            int intWayPoint = i;
            Instantiate(monstros[1], waypoints2[intWayPoint].transform.position, Quaternion.identity);
        }
        for (int i = 0; i < maxMonstros3; i++)
        {
            int intWayPoint = i;
            int intMonsterToSpawn = Random.Range(0, monstros.Length);
            Instantiate(monstros[intMonsterToSpawn], waypoints3[intWayPoint].transform.position, Quaternion.identity);
        }


    }

    IEnumerator spawnMonster_CR()
    {
        SpawnMonster();
        yield return true;
    }

}


