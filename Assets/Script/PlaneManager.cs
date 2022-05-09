using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public GameObject[] planes;
   
    [SerializeField]
    private float fallInterval = 5f;
    bool[] destroyed;
    int numOfdestroyed;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        numOfdestroyed = 0;
        int count = planes.Length;
        destroyed = new bool[count];
        for(int i = 0; i < destroyed.Length; i++)
        {
            destroyed[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > fallInterval && numOfdestroyed < planes.Length)
        {
            collapse();
            timer = 0;
        }
    }

    void collapse()
    {
        int randomNum = Random.Range(0, planes.Length);
        while (destroyed[randomNum] == true) {
            randomNum = Random.Range(0, planes.Length);
        }

        StartCoroutine(BlinkAndFall(planes[randomNum]));
  
        destroyed[randomNum] = true;
        numOfdestroyed++;
    }

    IEnumerator BlinkAndFall(GameObject g)
    {
        Renderer r = g.GetComponent<Renderer>();
        Color newColor = r.material.color;
        float tmpR = newColor.r;
        int blinkTimes = 5;
        while(blinkTimes != 0)
        {
            newColor.r = 255;
            r.material.color = newColor;
            yield return new WaitForSeconds(0.5f);

            newColor.r = tmpR;
            r.material.color = newColor;
            yield return new WaitForSeconds(0.5f);

            --blinkTimes;
        }
          

        g.GetComponent<Rigidbody>().useGravity = true;

        yield return new WaitForSeconds(2f);
        Destroy(g);
    }
}
