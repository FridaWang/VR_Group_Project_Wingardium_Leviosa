using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform editScoreUI;

    [SerializeField]
    private Transform gameTimeCountdownUI;

    [SerializeField]
    private Transform timeupUI;

    [SerializeField]
    private Transform lostGameUI;

    [SerializeField]
    private Transform guideUI;

    [SerializeField]
    private Transform otherThanGuideUI;

    [SerializeField]
    private Transform timeUpPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EditScoreUI(int score)
    {
        editScoreUI.GetComponent<Text>().text = score.ToString();
    }

    public void EditGameTimeCountdownUI(int time)
    {
        gameTimeCountdownUI.GetComponent<Text>().text = time.ToString();
    }

    public void ShowTimeUp()
    {
        timeupUI.gameObject.SetActive(true);
    }

    public void HideTimeUp()
    {
        timeupUI.gameObject.SetActive(false);
    }

    public void ShowLostGame()
    {
        lostGameUI.gameObject.SetActive(true);
    }

    public void ShowGuideUI()
    {
        guideUI.gameObject.SetActive(true);
        otherThanGuideUI.gameObject.SetActive(false);
    }

    public void HideGuideUI()
    {
        guideUI.gameObject.SetActive(false);
        otherThanGuideUI.gameObject.SetActive(true);
    }

    public void ShowTimeUpPanel()
    {
        timeUpPanel.gameObject.SetActive(true);
    }
}
