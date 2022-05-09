using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
enum BalloonType
{   
    BT_normal, 
    BT_acceleration,
    BT_deceleration,
    BT_explode,
    BT_mud
}

public class Balloon : MonoBehaviour
{
    public GameObject balloonExplosionPrefab;
    private Rigidbody balloonRigidbody;
    public float velY = 1f;
    public int addedScore = 10;
    public float lifeTime = 5;
    [SerializeField]
    private BalloonType balloonType;
    

    // Start is called before the first frame update
    void Start()
    {
        balloonRigidbody = GetComponent<Rigidbody>();
        Vector3 velocity = new Vector3(0.0f, velY, 0.0f);
        balloonRigidbody.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            if (gameObject)
            {
                DestroyImmediate(this.gameObject);
            }
            if (isBadBalloon())
                badBallonNotShot();
        }
    }

    private bool isBadBalloon()
    {
        if (balloonType == BalloonType.BT_acceleration || balloonType == BalloonType.BT_mud)
            return true;
        else
            return false;
    }
    public void changeVelocity(float velY)
    {
        Vector3 velocity = new Vector3(0.0f, velY, 0.0f);
        balloonRigidbody.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Arrowhead collider")
        {
            switch (balloonType)
            {
                case BalloonType.BT_normal:
                    GameManager.GameManager.getGM().playNormalBalloonSound();
                    normalCollision();
                    break;
                case BalloonType.BT_acceleration:
                    GameManager.GameManager.getGM().playBadBalloonSound();
                    badBallonCollision();
                    break;
                case BalloonType.BT_deceleration:
                    GameManager.GameManager.getGM().playGoodBalloonSound();
                    goodBallonCollision();
                    break;
                case BalloonType.BT_mud:
                    GameManager.GameManager.getGM().playBadBalloonSound();
                    badBallonCollision();
                    break;
                case BalloonType.BT_explode:
                    GameManager.GameManager.getGM().playGoodBalloonSound();
                    goodBallonCollision();
                    break;
                default:
                    break;
            }
        }
        if(other.gameObject.tag == "PlaySoundCollider")
        {
            if(gameObject.GetComponent<AudioSource>())
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }


    public void normalCollision()
    {
        GameManager.GameManager.getGM().addScore(addedScore);
        GameObject.Instantiate(balloonExplosionPrefab, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }

    private void badBallonCollision()
    {
        GameObject.Instantiate(balloonExplosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void goodBallonCollision()
    {
        switch (balloonType)
        {
            case BalloonType.BT_deceleration:
                GameManager.GameManager.getGM().changeToDeceleratedVelocity();
                GameObject.Instantiate(balloonExplosionPrefab, transform.position, transform.rotation);
                Destroy(this.gameObject);
                break;
            case BalloonType.BT_explode:
                GameManager.GameManager.getGM().destroyNormalBalloon();
                GameObject.Instantiate(balloonExplosionPrefab, transform.position, transform.rotation);
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
    
    private void badBallonNotShot()
    {
        switch (balloonType)
        {
            case BalloonType.BT_acceleration:
                GameManager.GameManager.getGM().changeToAcceleratedVelocity();
                break;
            case BalloonType.BT_mud:
                GameManager.GameManager.getGM().changeToBlockedView();
                break;
            default:
                break;
        }
    }

    private void mudNotShot()
    {
        GameManager.GameManager.getGM().changeToBlockedView();
        Destroy(this.gameObject);
    }

    private void accelerationNotShot()
    {
        GameManager.GameManager.getGM().changeToAcceleratedVelocity();
        Destroy(this.gameObject);
    }
}
