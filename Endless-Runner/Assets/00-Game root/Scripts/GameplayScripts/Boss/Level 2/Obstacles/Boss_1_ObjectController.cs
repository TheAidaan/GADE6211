using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_ObjectController : MonoBehaviour
{
    Transform _boss;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.BossMode)
        {
            Destroy(gameObject.GetComponent<Boss_1_ObjectController>());
        }else
        {
            _boss = GameObject.FindObjectOfType<Boss_1_Manager>().transform;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (BossManager.bossActive)
        {
            transform.RotateAround(_boss.position, Vector3.up, -.29f);
        }

        if (transform.position.x>40f)
        {
            Destroy(gameObject);
        }
    }
}
