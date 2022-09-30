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

    public Button p1SBtn;
    public Button p1LPBtn;
    public Button p1HPBtn;
    public Button p1LKBtn;
    public Button p1HKBtn;
    public Button p2SBtn;
    public Button p2LPBtn;
    public Button p2HPBtn;
    public Button p2LKBtn;
    public Button p2HKBtn;

    void Awake()
    {
        isP1Ready();
    }

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

    public void changeVideo()
    {
        PlayerScript.isTurnOfP1 = !PlayerScript.isTurnOfP1;
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().isLooping = !isLooping;
        disableAllButtons();
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().clip = v2;
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().loopPointReached +=
            backToStance;
    }

    public void backToStance(VideoPlayer vp)
    {
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().isLooping = isLooping;
        isP1Ready();
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().clip = stance;
    }

    private void isP1Ready()
    {
        if (PlayerScript.isTurnOfP1)
        {
            stateOfButtons(true, false);
        }
        else
        {
            stateOfButtons(false, true);
        }
    }

    private void disableAllButtons()
    {
        stateOfButtons(false, false);
    }

    private void stateOfButtons(bool p1, bool p2)
    {
        p1SBtn.interactable = p1;
        p1LPBtn.interactable = p1;
        p1HPBtn.interactable = p1;
        p1LKBtn.interactable = p1;
        p1HKBtn.interactable = p1;
        p2SBtn.interactable = p2;
        p2LPBtn.interactable = p2;
        p2HPBtn.interactable = p2;
        p2LKBtn.interactable = p2;
        p2HKBtn.interactable = p2;
    }
}
