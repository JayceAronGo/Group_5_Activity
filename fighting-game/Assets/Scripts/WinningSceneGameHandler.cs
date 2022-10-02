using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class WinningSceneGameHandler : MonoBehaviour
{
    public VideoPlayer vp;
    public VideoClip vpClipP1;
    public VideoClip vpClipP2;
    public AudioSource audioPlayer;
    public AudioClip apP1;
    public AudioClip apP2;

    // Display winner
    public GameObject winnerText;

    void Awake()
    {
        if (PlayerScript.p1Health <= 0)
        {
            winnerText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "player 2";
            vp.clip = vpClipP2;
            audioPlayer.PlayOneShot(apP2);
        }
        else if (PlayerScript.p2Health <= 0)
        {
            winnerText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "player 1";
            vp.clip = vpClipP1;
            audioPlayer.PlayOneShot(apP1);
        }
    }

    void Start() { }

    void Update() { }

    // change scene
    public void getBackToPlayerSelectionScene()
    {
        StartCoroutine(goToPlayerSelectionScene());
    }

    IEnumerator goToPlayerSelectionScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

    // quit
    public void quitGame()
    {
        Application.Quit();
    }
}
