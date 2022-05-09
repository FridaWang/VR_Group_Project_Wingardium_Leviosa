using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSpawner : MonoBehaviour
{
    public Transform parent;

    [SerializeField]
    private Object balloon;
    [SerializeField]
    private float waitAtStart = 2f;
    [SerializeField]
    private float interval = 5f;
    [SerializeField]
    private float radius = 20f;

    Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        InvokeRepeating("SpawnBall", waitAtStart, interval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnBall()
    {
        Vector3 pos = RandomCircle(center, radius);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
        GameObject spawnedBalloon = Instantiate(balloon, pos, rot) as GameObject;
        spawnedBalloon.transform.SetParent(parent);
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}
