using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform sceneParent;

    // Bit shift the index of the layer (6) to get a bit mask
    int layerMask = 1 << 6;

    // This would cast rays only against colliders in layer 6.

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down /*transform.TransformDirection(Vector3.down)*/, out hit, 2.5f, layerMask))
        {
            //Debug.DrawRay(transform.position, /*transform.TransformDirection(Vector3.down)*/ Vector3.down * hit.distance, Color.red);
            //Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            //Debug.DrawRay(transform.position, /*transform.TransformDirection(Vector3.down)*/ Vector3.down * 10, Color.white);
            //Debug.Log(hit.collider.gameObject.name);
            //gameObject.transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.down * 10);
            //Debug.Log(gameObject.transform.parent.name);
            sceneParent.Translate(Vector3.up * Time.deltaTime * 30);
            if (Physics.Raycast(transform.position, Vector3.down /*transform.TransformDirection(Vector3.down)*/, out hit, 5f, layerMask))
            {
                hit.collider.gameObject.SetActive(false);
            }
            if (!GameManager.GameManager.getGM().isGameEnd)
            {
                GameManager.GameManager.getGM().isGameEnd = true;
                GameManager.GameManager.getGM().GameLost();
            }
            
        }

    }
}
