using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSpawner : MonoBehaviour
{
    public GameObject chainHandler;

    private float chainAppearCount;
    private float chaindisappearCount;
    private bool isChainAppeared;

    public float chainAppearTime;

    public float chainLifetime;
    private float chainLifeCountdown;

    // Start is called before the first frame update
    void Start()
    {
        isChainAppeared = false;
        chainLifeCountdown = chainLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChainAppeared)
        {
            if (chainAppearCount > chainAppearTime)
            {
                chainHandler.SetActive(true);
                isChainAppeared = true;
            }
            else
            {
                chainAppearCount += Time.deltaTime;
            }
        }
        else // count the appear time
        {
            if (chainLifeCountdown <= 0)
            {
                chainHandler.SetActive(false);
                isChainAppeared = false;
                chainAppearCount = 0.0f;
                chainLifeCountdown = chainLifetime;
            }
            else
            {
                chainLifeCountdown -= Time.deltaTime;
            }
        }
    }

    public void chainIsDestroied()
    {
        chainHandler.SetActive(false);
        isChainAppeared = false;
        chainAppearCount = 0.0f;
        chainLifeCountdown = chainLifetime;
    }
}
