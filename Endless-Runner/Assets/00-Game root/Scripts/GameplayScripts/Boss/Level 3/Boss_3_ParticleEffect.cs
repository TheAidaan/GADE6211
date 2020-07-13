using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3_ParticleEffect : MonoBehaviour
{
    ParticleSystem em;
    // Start is called before the first frame update
    void Start()
    {
        em = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            em.Play(true);
        }
    }
}
