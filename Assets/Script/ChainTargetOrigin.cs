using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTargetOrigin : MonoBehaviour
{
    public GameObject CloudSpwaner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("------");
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("------");
    //    if(collision.gameObject.name == "Arrowhead collider")
    //    {

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Arrowhead collider")
        {
            other.gameObject.transform.SetParent(this.transform);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            GameManager.GameManager.getGM().addScore(100);
            if (gameObject.GetComponent<AudioSource>())
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
            StartCoroutine(destoryTarget(other));
        }
    }
    private IEnumerator destoryTarget(Collider other)
    {
        yield return new WaitForSeconds(1.5f);
        other.gameObject.SetActive(false);
        CloudSpwaner.GetComponent<ChainSpawner>().chainIsDestroied();
        
    }
}
