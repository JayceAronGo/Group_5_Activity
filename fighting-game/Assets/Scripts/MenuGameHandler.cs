using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameHandler : MonoBehaviour
{
    void Start() { }

    void Update() { }

    IEnumerator goToPlayerSelection()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        StartCoroutine(goToPlayerSelection());
    }
}
