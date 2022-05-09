using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager gm;

        [SerializeField]
        private Transform blueBalloonParent;
        [SerializeField]
        private Transform orangeBalloonParent;
        [SerializeField]
        private Transform mudBalloonParent;
        [SerializeField]
        private Transform acceBalloonParent;
        [SerializeField]
        private Transform deceBalloonParent;

        [SerializeField]
        private Transform blueBalloonPrefab;
        [SerializeField]
        private Transform orangeBalloonPrefab;
        [SerializeField]
        private Transform mudBalloonPrefab;
        [SerializeField]
        private Transform acceBalloonPrefab;
        [SerializeField]
        private Transform deceBalloonPrefab;

        [SerializeField]
        private GameObject longBow;

        [SerializeField]
        private float accelationTime;
        [SerializeField]
        private float decelationTime;
        [SerializeField]
        private float mudBlockedTime;
        [SerializeField]
        private Transform UIManager;
        [SerializeField]
        private Transform spawnerManager;
        [SerializeField]
        private GameObject bounsSpawnerManager;
        [SerializeField]
        private GameObject soundManager;

        [SerializeField]
        private Transform scenePointerHandler;

        [SerializeField]
        private Transform rightHandController;

        [SerializeField]
        private Transform playerRaycastHandler;

        [SerializeField]
        private Transform GoodBalloonSound;
        [SerializeField]
        private Transform BadBalloonSound;
        [SerializeField]
        private Transform NormalBalloonSound;
        [SerializeField]
        private Transform PlayerDiedSound;


        public Transform MudBlockUI;

        public Transform bloodUI;

        private UIManager uiManager;

        private int score;

        [SerializeField]
        private float acceleratedVelY = 10;
        [SerializeField]
        private float deceleratedVelY = 0.1f;
        [SerializeField]
        private float normalVelY = 1;

        private bool isMudBlocked = false;
        private float leftMudBlockedTime;

        private bool isAccelerated = false;
        private float leftAccelerationTime;

        private bool isDecelerated = false;
        private float leftDecelerationTime;

        [SerializeField]
        private int gameTime;
        private bool isGameTimeUsed;

        public bool isGameStart = false;
        public bool isGameEnd = false;

        private GameObject arrowHand;

        private void Awake()
        {
            gm = this;
            score = 0;
            uiManager = UIManager.GetComponent<UIManager>();
            

            isAccelerated = false;
            leftAccelerationTime = accelationTime;

            isDecelerated = false;
            leftDecelerationTime = decelationTime;

            isMudBlocked = false;
            leftMudBlockedTime = mudBlockedTime;

            isGameTimeUsed = false;
            uiManager.EditGameTimeCountdownUI(gameTime);

            blueBalloonPrefab.GetComponent<Balloon>().velY = normalVelY;
            orangeBalloonPrefab.GetComponent<Balloon>().velY = normalVelY;
        }

        private void Update()
        {
            //arrowHand = BowPickUp.GetComponent<ArrowHand>();
            arrowHand = GameObject.FindGameObjectWithTag("arrowHand");
            //StartCoroutine(gameCountdown());
            if (!isGameStart && arrowHand != null)
            {
                ArrowHand ah = arrowHand.GetComponent<ArrowHand>();
                if (ah.firstPickUp)
                {
                    startSpawn();
                    isGameStart = true;
                }
            }

            if (isAccelerated)
            {
                accelerationCountdown();
            }

            if (isMudBlocked)
            {
                mudBlockedCountdown();
            }

            if (isDecelerated)
            {
                decelerationCountdown();
            }

            if (!isGameTimeUsed && gameTime > 0 && isGameStart && !isGameEnd)
            {
                StartCoroutine(gameTimeCountdown());
            }
            else if (!isGameTimeUsed && gameTime <= 0 && !isGameEnd)
            {
                isGameEnd = true;
                destroyAllBalloons();
                stopSpawn();
                uiManager.ShowTimeUp();
                StartCoroutine(gameTimeUpShowMenu());
                //Application.Quit();
            }
        }

        public static GameManager getGM()
        {
            return gm;
        }

        public void addScore(int added)
        {
            score += added;
            uiManager.EditScoreUI(score);
        }

        private void startSpawn()
        {
            foreach (Transform child in spawnerManager)
            {
                child.gameObject.SetActive(true);
            }
            bounsSpawnerManager.SetActive(true);
            longBow.SetActive(false);
        }

        private void stopSpawn()
        {
            foreach (Transform child in spawnerManager)
            {
                //child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
        }

        public void destroyNormalBalloon()
        {
            foreach (Transform child in blueBalloonParent)
            {
                child.GetComponent<Balloon>().normalCollision();
                //Destroy(child.gameObject); 
            }

            foreach (Transform child in orangeBalloonParent)
            {
                child.GetComponent<Balloon>().normalCollision();
                //Destroy(child.gameObject);
            }
        }

        public void destroyAllBalloons()
        {
            foreach (Transform child in blueBalloonParent)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in orangeBalloonParent)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in mudBalloonParent)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in acceBalloonParent)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in deceBalloonParent)
            {
                Destroy(child.gameObject);
            }
        }

        public void changeToAcceleratedVelocity()
        {
            //Debug.Log("acce!!!!");
            isAccelerated = true;
            blueBalloonPrefab.GetComponent<Balloon>().velY = acceleratedVelY;
            orangeBalloonPrefab.GetComponent<Balloon>().velY = acceleratedVelY;

            foreach (Transform child in blueBalloonParent)
            {
                child.GetComponent<Balloon>().changeVelocity(acceleratedVelY);
            }

            foreach (Transform child in orangeBalloonParent)
            {
                child.GetComponent<Balloon>().changeVelocity(acceleratedVelY);
            }
        }


        public void changeToDeceleratedVelocity()
        {
            isDecelerated = true;
            blueBalloonPrefab.GetComponent<Balloon>().velY = deceleratedVelY;
            orangeBalloonPrefab.GetComponent<Balloon>().velY = deceleratedVelY;

            foreach (Transform child in blueBalloonParent)
            {
                child.GetComponent<Balloon>().changeVelocity(deceleratedVelY);
            }

            foreach (Transform child in orangeBalloonParent)
            {
                child.GetComponent<Balloon>().changeVelocity(deceleratedVelY);
            }

        }

        private void changeBackToNormalVelocity()
        {
            blueBalloonPrefab.GetComponent<Balloon>().velY = normalVelY;
            orangeBalloonPrefab.GetComponent<Balloon>().velY = normalVelY;

            foreach (Transform child in blueBalloonParent)
            {
                child.GetComponent<Balloon>().changeVelocity(normalVelY);
            }

            foreach (Transform child in orangeBalloonParent)
            {
                child.GetComponent<Balloon>().changeVelocity(normalVelY);
            }
        }


        public void changeToBlockedView()
        {
            isMudBlocked = true;
            MudBlockUI.gameObject.SetActive(true);
        }

        public void GameLost()
        {
            //uiManager.ShowLostGame();


            StartCoroutine(GameLostWaitTime());
            //Time.timeScale = 0;
        }

        public void ShowGuide()
        {
            uiManager.ShowGuideUI();
        }

        public void HideGuide()
        {
            uiManager.HideGuideUI();
        }

        IEnumerator GameLostWaitTime()
        {
            bounsSpawnerManager.SetActive(false);
            yield return new WaitForSeconds(2.7f);
            playerRaycastHandler.GetComponent<PlayerRaycast>().enabled = false;
            ShowTimeUpMenu();
            stopSpawn();
            destroyAllBalloons();
            this.GetComponent<AudioSource>().Pause();
            bloodUI.gameObject.SetActive(true);
            PlayerDiedSound.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1.0f);
            soundManager.GetComponent<AudioSource>().Play();
            
            //Time.timeScale = 0;
        }

        private void changeBackToNormalView()
        {
            //Debug.Log("=================Normal===============");
            MudBlockUI.gameObject.SetActive(false);
        }

        private void accelerationCountdown()
        {
            leftAccelerationTime -= Time.deltaTime;
            if (leftAccelerationTime <= 0.0f)
            {
                isAccelerated = false;
                changeBackToNormalVelocity();
                leftAccelerationTime = accelationTime;
            }
        }

        private void decelerationCountdown()
        {
            leftDecelerationTime -= Time.deltaTime;
            if (leftDecelerationTime <= 0.0f)
            {
                isDecelerated = false;
                changeBackToNormalVelocity();
                Debug.Log("====Decelerate to Normal======");
                leftDecelerationTime = decelationTime;
            }
        }

        private void mudBlockedCountdown()
        {
            //Debug.Log("MudCountDown");
            leftMudBlockedTime -= Time.deltaTime;
            if (leftMudBlockedTime <= 0.0f)
            {
                isMudBlocked = false;
                changeBackToNormalView();
                leftMudBlockedTime = mudBlockedTime;
            }
        }

        IEnumerator gameTimeCountdown()
        {
            if (gameTime > 0)
            {
                isGameTimeUsed = true;
                yield return new WaitForSeconds(1.0f);
                gameTime--;
                isGameTimeUsed = false;
                uiManager.EditGameTimeCountdownUI(gameTime);
            }

        }

        IEnumerator gameTimeUpShowMenu()
        {
            yield return new WaitForSeconds(5.0f);
            uiManager.HideTimeUp();
            ShowTimeUpMenu();
        }


        private void ShowTimeUpMenu()
        {
            scenePointerHandler.gameObject.SetActive(true);
            rightHandController.GetComponent<SteamVR_LaserPointer>().enabled = true;
            uiManager.ShowTimeUpPanel();
        }

        public void playNormalBalloonSound()
        {
            NormalBalloonSound.GetComponent<AudioSource>().Play();
        }

        public void playBadBalloonSound()
        {
            BadBalloonSound.GetComponent<AudioSource>().Play();
        }

        public void playGoodBalloonSound()
        {
            GoodBalloonSound.GetComponent<AudioSource>().Play();
        }
    }
}

