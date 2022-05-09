using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator BlinkAndFall()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log(gameObject.name);
        Renderer r = gameObject.GetComponent<Renderer>();
        Color newColor = r.material.color;
        float tmpR = newColor.r;
        int blinkTimes = 3;
        while (blinkTimes != 0)
        {
            newColor.r = 255;
            r.material.color = newColor;
            yield return new WaitForSeconds(0.5f);

            newColor.r = tmpR;
            r.material.color = newColor;
            yield return new WaitForSeconds(0.5f);

            --blinkTimes;
        }

        //gameObject.GetComponent<Rigidbody>().useGravity = true;

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Arrowhead collider")
        {
            StartCoroutine(BlinkAndFall());
        }
        if (other.gameObject.tag == "BadBalloon")
        {
            Destroy(gameObject);
        }
    }
}
