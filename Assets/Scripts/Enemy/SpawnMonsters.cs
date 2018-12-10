using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour {

    public GameObject[] waypoints1;
    public GameObject[] waypoints2;
    public GameObject[] waypoints3;
    public GameObject[] waypoints4;
    public Transform[] monstros;
    public int maxMonstros;
    public int maxMonstros2;
    public int maxMonstros3;
    public int maxMonstros4;

    [Header("Transform")]
    public Transform monstrosChild;


    void Start()
    {
        StartCoroutine(spawnMonster_CR());
    }

    public void SpawnMonster()
    {
        for (int i = 0; i < maxMonstros;i ++)
        {
            int intWayPoint = i;
            Instantiate(monstros[0], waypoints1[intWayPoint].transform.position, Quaternion.identity, monstrosChild);
        }
        for (int i = 0; i < maxMonstros2; i++)
        {
            int intWayPoint = i;
            Instantiate(monstros[1], waypoints2[intWayPoint].transform.position, Quaternion.identity, monstrosChild);
        }
        for (int i = 0; i < maxMonstros3; i++)
        {
            int intWayPoint = i;
            int intMonsterToSpawn = Random.Range(0, 2);
            Instantiate(monstros[intMonsterToSpawn], waypoints3[intWayPoint].transform.position, Quaternion.identity, monstrosChild);
        }
        for (int i = 0; i < maxMonstros4; i++)
        {
            int intWayPoint = i;
            Instantiate(monstros[2], waypoints4[intWayPoint].transform.position, Quaternion.identity, monstrosChild);
        }


    }

    IEnumerator spawnMonster_CR()
    {
        SpawnMonster();
        yield return true;
    }

}


