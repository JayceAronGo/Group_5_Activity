using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneGameHandler : MonoBehaviour
{
    public GameObject p1HealthText;
    public GameObject p2HealthText;
    public GameObject p1Name;
    public GameObject p2Name;

    void Start()
    {
        setHealth();
        getPlayerName();
    }

    void Update()
    {
        changeHealthText();
    }

    void getPlayerName()
    {
        p1Name.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p1Name.ToString();
        p2Name.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p2Name.ToString();
    }

    void setHealth()
    {
        PlayerScript.p1Health = PlayerScript.gameHealth;
        PlayerScript.p2Health = PlayerScript.gameHealth;
    }

    void changeHealthText()
    {
        p1HealthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p2Health.ToString();
        p2HealthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p1Health.ToString();
    }
}
