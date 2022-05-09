using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowParticlesystem : MonoBehaviour
{
    public GameObject particleSystem;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject spawnedParticle = GameObject.Instantiate(particleSystem, transform.position, transform.rotation);
        //ps = spawnedParticle.GetComponent<ParticleSystem>();
        //ps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //spawnedParticle.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        //ps = spawnedParticle.GetComponent<ParticleSystem>();
        //ps.Play();
        //Destroy(spawnedParticle, ps.duration);
    }

    public void ActiveTail()
    {
        GameObject spawnedParticle = GameObject.Instantiate(particleSystem, transform.position, transform.rotation);
        ps = spawnedParticle.GetComponent<ParticleSystem>();
        ps.Play();
    }
}
