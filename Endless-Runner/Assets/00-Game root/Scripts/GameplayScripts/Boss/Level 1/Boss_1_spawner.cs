using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Spawner : BossSpawner
{
    Transform[] bossWorld = new Transform[4];

    GameObject[] _children = new GameObject[3];

    GameObject[] bossObstacles = new GameObject[4];

    bool _activateWalkway = true;

    void Start()
    {
        bossWorld = Resources.LoadAll<Transform>("Prefabs/Boss/Level 1/World");

        for( int i = 0; i < 3; i++) 
        {
            _children[i] = transform.GetChild(i).gameObject;
        }

        _children[2].SetActive(false);
    }

    public void ActivateWalkway()
    {
        if (_activateWalkway)
        {
            _children[2].SetActive(true);
        }
    }

    public void SpawnObject(GameObject obj)
    {
        int randNumber = Random.Range(-53, -50); // pick random lane
        if (obj != null)
        {
            obj = Instantiate(obj, new Vector3(randNumber + 0.3f, 1, transform.position.z), obj.transform.rotation, transform);
            obj.AddComponent<Boss_1_OjectController>();
        }
    }

    public void ReleasePlayer()
    {
        Destroy(_children[1]);
        _children[0].gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}
