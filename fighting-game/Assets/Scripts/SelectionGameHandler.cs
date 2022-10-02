using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectionGameHandler : MonoBehaviour
{
    public TMP_InputField p1InputField;
    public TMP_InputField p2InputField;
    public GameObject healthText;
    public AudioSource audioPlayer;

    void Start() { }

    void Update()
    {
        healthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.gameHealth.ToString();
    }

    // setting health buttons
    public void setHealthTo50()
    {
        setHealth(50);
    }

    public void setHealthTo100()
    {
        setHealth(100);
    }

    public void setHealthTo150()
    {
        setHealth(150);
    }

    public void setHealthTo200()
    {
        setHealth(200);
    }

    private void setHealth(int health)
    {
        PlayerScript.gameHealth = health;
    }

    // fight

    public void fight()
    {
        getP1Name();
        getP2Name();
        FightScene();
    }

    // changing scene
    IEnumerator goToFightScene()
    {
        yield return new WaitForSeconds(0.5f);
        for (float i = 100; i > 0; i--)
        {
            audioPlayer.volume = i / 100;
        }
        SceneManager.LoadScene(2);
    }

    public void FightScene()
    {
        StartCoroutine(goToFightScene());
    }

    // get names
    private void getP1Name()
    {
        string inputName = p1InputField.GetComponent<TMP_InputField>().text.ToString();

        if (inputName == "")
        {
            PlayerScript.p1Name = "Player 1";
        }
        else
        {
            PlayerScript.p1Name = inputName;
        }
    }

    private void getP2Name()
    {
        string inputName = p2InputField.GetComponent<TMP_InputField>().text.ToString();

        if (inputName == "")
        {
            PlayerScript.p2Name = "Player 2";
        }
        else
        {
            PlayerScript.p2Name = inputName;
        }
    }
}
