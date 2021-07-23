using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public TextMeshProUGUI team1ScoreText;
    public TextMeshProUGUI team2ScoreText;
    public GameObject panelConfirmReset;
    public GameObject btnTeam1Up;
    public GameObject btnTeam2Up;
    public GameObject btnTeam1Down;
    public GameObject btnTeam2Down;
    public GameObject btnReset;
    public GameObject imgSchnappi;

    private int team1Score = 0;
    private int team2Score = 0;
    private int layout = 0;
    private RectTransform rt1Up;
    private RectTransform rt1Down;
    private RectTransform rt2Up;
    private RectTransform rt2Down;
    private RectTransform rtReset;
    private RectTransform rtSchnappi;

    // Start is called before the first frame update
    void Start()
    {
        rt1Up      = btnTeam1Up.GetComponent<RectTransform>();
        rt1Down    = btnTeam1Down.GetComponent<RectTransform>();
        rt2Up      = btnTeam2Up.GetComponent<RectTransform>();
        rt2Down    = btnTeam2Down.GetComponent<RectTransform>();
        rtReset    = btnReset.GetComponent<RectTransform>();
        rtSchnappi = imgSchnappi.GetComponent<RectTransform>();

        team1Score = PlayerPrefs.GetInt("Team1Score");
        team2Score = PlayerPrefs.GetInt("Team2Score");

        layout = PlayerPrefs.GetInt("Layout");
        ArrangeToLayout(layout);

        panelConfirmReset.SetActive(false);

        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaObject act = new AndroidJavaClass("com.unity3d.player.UnityPlayer") // Get the Unity Player.
                .GetStatic<AndroidJavaObject>("currentActivity"); // Get the Current Activity from the Unity Player.
                act.Call("setShowWhenLocked", true);
        }
    }

    void Update()
    {
        team1ScoreText.text = team1Score.ToString();
        team2ScoreText.text = team2Score.ToString();
    }

    public void OnTeam1Up()
    {
        team1Score++;
        SaveScores();
    }

    public void OnTeam2Up()
    {
        team2Score++;
        SaveScores();
    }

    public void OnTeam1Down()
    {
        if (team1Score > 0)
        {
            team1Score--;
            SaveScores();
        }
    }

    public void OnTeam2Down()
    {
        if (team2Score > 0)
        {
            team2Score--;
            SaveScores();
        }
    }

    public void OnReset()
    {
        panelConfirmReset.SetActive(true);
    }

    public void OnCancelReset()
    {
        panelConfirmReset.SetActive(false);
    }

    public void OnReallyReset()
    {
        team1Score = 0;
        team2Score = 0;
        SaveScores();
        panelConfirmReset.SetActive(false);
    }

    public void OnLayout()
    {
        layout++;
        if (layout > 2)
        {
            layout = 0;
        }
        ArrangeToLayout(layout);
        PlayerPrefs.SetInt("Layout", layout);
        PlayerPrefs.Save();
    }

    private void ArrangeToLayout(int layoutNumber)
    {
        switch (layoutNumber)
        {
            case 0:
                {
                    // symetric
                    rt1Down.anchoredPosition = new Vector2(-320, -140);
                    rt1Up.anchoredPosition = new Vector2(-130, -140);
                    rt2Down.anchoredPosition = new Vector2(130, -140);
                    rt2Up.anchoredPosition = new Vector2(320, -140);
                    rtReset.anchorMin = new Vector2(0, 0);
                    rtReset.anchorMax = new Vector2(0, 0);
                    rtReset.anchoredPosition = new Vector2(130, 130);
                    rtSchnappi.anchorMin = new Vector2(1, 0);
                    rtSchnappi.anchorMax = new Vector2(1, 0);
                    rtSchnappi.anchoredPosition = new Vector2(-130, 130);
                    break;
                }
            case 1:
                {
                    // one hand right
                    rt1Down.anchoredPosition = new Vector2(130, -330);
                    rt1Up.anchoredPosition = new Vector2(320, -330);
                    rt2Down.anchoredPosition = new Vector2(130, -140);
                    rt2Up.anchoredPosition = new Vector2(320, -140);
                    rtReset.anchorMin = new Vector2(0, 0);
                    rtReset.anchorMax = new Vector2(0, 0);
                    rtReset.anchoredPosition = new Vector2(130, 130);
                    rtSchnappi.anchorMin = new Vector2(1, 0);
                    rtSchnappi.anchorMax = new Vector2(1, 0);
                    rtSchnappi.anchoredPosition = new Vector2(-130, 130);
                    break;
                }
            case 2:
                {
                    // one hand left
                    rt1Down.anchoredPosition = new Vector2(-320, -140);
                    rt1Up.anchoredPosition = new Vector2(-130, -140);
                    rt2Down.anchoredPosition = new Vector2(-320, -330);
                    rt2Up.anchoredPosition = new Vector2(-130, -330);
                    rtReset.anchorMin = new Vector2(1, 0);
                    rtReset.anchorMax = new Vector2(1, 0);
                    rtReset.anchoredPosition = new Vector2(-130, 130);
                    rtSchnappi.anchorMin = new Vector2(0, 0);
                    rtSchnappi.anchorMax = new Vector2(0, 0);
                    rtSchnappi.anchoredPosition = new Vector2(130, 130);
                    break;
                }
        }
    }

    private void PlaceRectTransform(RectTransform rectTransform, float left, float top)
    {
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left, rectTransform.rect.width);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, top, rectTransform.rect.height);
    }

    private void SaveScores() { 
        PlayerPrefs.SetInt("Team1Score", team1Score);
        PlayerPrefs.SetInt("Team2Score", team2Score);
        PlayerPrefs.Save();
    }
}
