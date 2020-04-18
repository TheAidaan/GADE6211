using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CharacterBossMovement : MonoBehaviour
{
    //private Vector3 target = new Vector3(0f, 0f, 0f);
    //Vector3 final = new Vector3(-24f, 100f, 8f);

    float timeCounter = 0;
    NavMeshAgent itself;
    Rigidbody self;
    // Start is called before the first frame update
    void Start()
    {
        //self = GetComponent<Rigidbody>();
        itself = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //self.velocity = new Vector3(0, 0,.89f);
        //self.velocity = Vector3.up * .89f;
        //self.velocity = Vector3.left * .65f;

        //transform.Rotate(0,.03f,0);
    }

    private void Update()
    {
        //transform.position =
        //    Quaternion.AngleAxis(timeCounter, Vector3.left) *
        //    new Vector3(50, 0f);
        //timeCounter += Time.deltaTime;

        //float x = transform.position.x + Mathf.Cos (timeCounter);
        //float y = transform.position.y+ Mathf.Sin (timeCounter);
        //float z = 0;

        //transform.position = new Vector3(x, y, z);

        //transform.RotateAround(target, Vector3.up, 20 * Time.deltaTime);
        // transform.Rotate(0f,0f,.01f*Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, final, .025f);
    }
}
