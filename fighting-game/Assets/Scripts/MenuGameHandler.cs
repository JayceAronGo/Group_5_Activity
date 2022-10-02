using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameHandler : MonoBehaviour
{
    public AudioSource audioPlayer;

    void Start() { }

    void Update() { }

    // delay
    IEnumerator goToPlayerSelection()
    {
        for (float i = 100; i > 0; i--)
        {
            audioPlayer.volume = i / 100;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    // change scene
    public void StartGame()
    {
        StartCoroutine(goToPlayerSelection());
    }

    // quit

    public void quitGame()
    {
        Application.Quit();
    }
}
