using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameHandler : MonoBehaviour
{
    public AudioSource audioPlayer;

    void Start() { }

    void Update() { }

    IEnumerator goToPlayerSelection()
    {
        for (float i = 100; i > 0; i--)
        {
            audioPlayer.volume = i / 100;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        StartCoroutine(goToPlayerSelection());
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
