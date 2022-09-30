using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FightSceneGameHandler : MonoBehaviour
{
    public GameObject p1HealthText;
    public GameObject p2HealthText;
    public GameObject p1DamageText;
    public GameObject p2DamageText;
    public GameObject p1HealthBar;
    public GameObject p2HealthBar;
    public GameObject p1Name;
    public GameObject p2Name;
    public GameObject fightSceneVideoPlayer;
    public VideoClip v2;
    public VideoClip stance;
    public VideoClip p1lowPunch;
    private bool isLooping = true;
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
        healthText();
        getPlayerName();
    }

    void Update()
    {
        whoWillwin();
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

    private void healthText()
    {
        p1HealthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p1Health.ToString();
        p2HealthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p2Health.ToString();
    }

    public void backToStance(VideoPlayer vp)
    {
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().isLooping = isLooping;
        isP1Ready();
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().clip = stance;
        p1DamageText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
        p2DamageText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
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

    private void dealDamageToP2(int currentHP, int damage, int accuracy, float duration)
    {
        int randomValue = Random.Range(0, 101);
        if (randomValue <= accuracy)
        {
            PlayerScript.p2Health = currentHP -= damage;
            StartCoroutine(delayedDamageToP2(damage, duration));
        }
        else
        {
            damage = 0;
            StartCoroutine(delayedDamageToP2(damage, duration));
        }
    }

    private void dealDamageToP1(int currentHP, int damage, int accuracy, float duration)
    {
        int randomValue = Random.Range(0, 101);
        if (randomValue <= accuracy)
        {
            PlayerScript.p1Health = currentHP -= damage;
            StartCoroutine(delayedDamageToP1(damage, duration));
        }
        else
        {
            damage = 0;
            StartCoroutine(delayedDamageToP1(damage, duration));
        }
    }

    private IEnumerator delayedDamageToP1(int damageAmount, float duration)
    {
        yield return new WaitForSeconds(duration);
        p1HealthBar.gameObject.GetComponent<Image>().fillAmount -=
            (float)damageAmount / PlayerScript.gameHealth;
        p1HealthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p1Health.ToString();
        if (damageAmount > 0)
        {
            p1DamageText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
                "-" + damageAmount.ToString();
        }
        else
        {
            p1DamageText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Miss";
        }
    }

    private IEnumerator delayedDamageToP2(int damageAmount, float duration)
    {
        yield return new WaitForSeconds(duration);
        p2HealthBar.gameObject.GetComponent<Image>().fillAmount -=
            (float)damageAmount / PlayerScript.gameHealth;
        p2HealthText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
            PlayerScript.p2Health.ToString();
        if (damageAmount > 0)
        {
            p2DamageText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
                "-" + damageAmount.ToString();
        }
        else
        {
            p2DamageText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Miss";
        }
    }

    private void whoToAttack()
    {
        PlayerScript.isTurnOfP1 = !PlayerScript.isTurnOfP1;
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().isLooping = !isLooping;
    }

    private void attack(VideoClip video)
    {
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().clip = video;
        fightSceneVideoPlayer.gameObject.GetComponent<VideoPlayer>().loopPointReached +=
            backToStance;
    }

    public void p1Special()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP2(PlayerScript.p2Health, 25, 90, 2f);
        attack(v2);
    }

    public void p1LowPunch()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP2(PlayerScript.p2Health, 3, 75, 0.5f);
        attack(p1lowPunch);
    }

    public void p2Special()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP1(PlayerScript.p1Health, 25, 90, 2f);
        attack(v2);
    }

    //win
    private void whoWillwin()
    {
        if (PlayerScript.p2Health <= 0)
        {
            Debug.Log("player 1 win");
        }
        else if (PlayerScript.p1Health <= 0)
        {
            Debug.Log("player 2 win");
        }
    }
}
