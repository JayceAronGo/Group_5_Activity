using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameHandler : MonoBehaviour
{
    public static AudioSource audioPlayer;

    void Start() { }

    void Update() { }

    // delay
    IEnumerator goToPlayerSelection()
    {
        yield return new WaitForSeconds(0.5f);
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
