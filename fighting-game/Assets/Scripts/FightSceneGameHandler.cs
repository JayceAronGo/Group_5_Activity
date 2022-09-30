using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FightSceneGameHandler : MonoBehaviour
{
    public GameObject p1HealthText;
    public GameObject p2HealthText;
    public GameObject p1Name;
    public GameObject p2Name;

    public GameObject fightSceneVideoPlayer;
    public VideoClip v2;
    public VideoClip stance;

    bool isLooping = true;
    public Button p1LP;

    void Start()
    {
        setHealth();
        getPlayerName();
    }

    void Update()
    {
        changeHealthText();
    }

    private void getPlayerName()
    {
        p1Name.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p1Name.ToString();
        p2Name.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p2Name.ToString();
    }

    private void setHealth()
    {
        PlayerScript.p1Health = PlayerScript.gameHealth;
        PlayerScript.p2Health = PlayerScript.gameHealth;
    }

    private void changeHealthText()
    {
        p1HealthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p2Health.ToString();
        p2HealthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p1Health.ToString();
    }

    private void isP1Ready()
    {
        if (PlayerScript.isTurnOfP1)
        {
            p1LP.interactable = true;
            PlayerScript.isTurnOfP1 = false;
        }
        else
        {
            p1LP.interactable = false;
            PlayerScript.isTurnOfP1 = true;
        }
    }

    public void changeVideo()
    {
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().isLooping = !isLooping;
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().clip = v2;

        PlayerScript.isTurnOfP1 = !PlayerScript.isTurnOfP1;
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().loopPointReached +=
            backToStance;
    }

    public void backToStance(VideoPlayer vp)
    {
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().isLooping = isLooping;
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().clip = stance;
        //ditooo  isP1Ready();
    }
}
