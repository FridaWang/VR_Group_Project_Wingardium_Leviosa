using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class ScenePointerHandler : MonoBehaviour
{
    [SerializeField]
    private Transform StartButtonUI;

    [SerializeField]
    private Transform GuideButtonUI;

    [SerializeField]
    private Transform GuideBackButtonUI;

    [SerializeField]
    private Transform RestartButtonUI;

    [SerializeField]
    private Transform QuitButtonUI;



    private Button startButton;
    public SteamVR_LaserPointer laserPointer;
    public Color highLightedColor;
    public Color normalColor;

    void Start()
    {
        laserPointer.PointerIn += ButtonHighlight;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += ButtonClicked;
    }

    public void ButtonClicked(object sender, PointerEventArgs e)
    {
        if (e.target.name == "StartButton")
        {
            StartButtonUI.GetComponent<Button>().Select();
            SceneManager.LoadScene("MainScene");
        }
        else if(e.target.name == "GuideButton")
        {
            GameManager.GameManager.getGM().ShowGuide();
        }
        else if (e.target.name == "GuideBackButton")
        {
            GameManager.GameManager.getGM().HideGuide();
        }
        else if (e.target.name == "RestartButton")
        {
            Debug.Log("----------------");
            SceneManager.LoadScene("MainScene");          
        }
        else if (e.target.name == "QuitButton")
        {
            Application.Quit();
        }
    }

    public void ButtonHighlight(object sender, PointerEventArgs e)
    {
        if (e.target.name == "StartButton")
        {
            var colors = StartButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(highLightedColor.r * 255, highLightedColor.g * 255, highLightedColor.b * 255);
            StartButtonUI.GetComponent<Button>().colors = colors;
        }
        else if (e.target.name == "GuideButton")
        {
            var colors = GuideButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(highLightedColor.r * 255, highLightedColor.g * 255, highLightedColor.b * 255);
            GuideButtonUI.GetComponent<Button>().colors = colors;
        }
        else if (e.target.name == "GuideBackButton")
        {
            var colors = GuideBackButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(highLightedColor.r * 255, highLightedColor.g * 255, highLightedColor.b * 255);
            GuideBackButtonUI.GetComponent<Button>().colors = colors;
        }
        else if (e.target.name == "RestartButton")
        {
            var colors = RestartButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(highLightedColor.r * 255, highLightedColor.g * 255, highLightedColor.b * 255);
            RestartButtonUI.GetComponent<Button>().colors = colors;
        }
        else if (e.target.name == "QuitButton")
        {
            var colors = QuitButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(highLightedColor.r * 255, highLightedColor.g * 255, highLightedColor.b * 255);
            QuitButtonUI.GetComponent<Button>().colors = colors;
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if(SceneManager.GetActiveScene().name == "StartScene")
        {
            var colors = StartButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(normalColor.r * 255, normalColor.g * 255, normalColor.b * 255);
            StartButtonUI.GetComponent<Button>().colors = colors;

            colors = GuideButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(normalColor.r * 255, normalColor.g * 255, normalColor.b * 255);
            GuideButtonUI.GetComponent<Button>().colors = colors;

            colors = GuideBackButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(normalColor.r * 255, normalColor.g * 255, normalColor.b * 255);
            GuideBackButtonUI.GetComponent<Button>().colors = colors;
        }
        else if(SceneManager.GetActiveScene().name == "MainScene")
        {
            var colors = RestartButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(normalColor.r * 255, normalColor.g * 255, normalColor.b * 255);
            RestartButtonUI.GetComponent<Button>().colors = colors;

            colors = QuitButtonUI.GetComponent<Button>().colors;
            colors.normalColor = new Color(normalColor.r * 255, normalColor.g * 255, normalColor.b * 255);
            QuitButtonUI.GetComponent<Button>().colors = colors;
        }
    }
}
