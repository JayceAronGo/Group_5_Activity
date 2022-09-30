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
    public VideoClip p1LowPunchVideo;
    public VideoClip p1LowPunchMissedVideo;
    public VideoClip p2LowPunchVideo;
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
    public GameObject p1TriggerBar;
    public GameObject p2TriggerBar;
    public float p1TriggerValue;
    public float p2TriggerValue;
    private bool p1IsSpecialUsed = false;
    private bool p2IsSpecialUsed = false;

    void Awake()
    {
        isP1Ready();
        p1SBtn.interactable = false;
        p2SBtn.interactable = false;
    }

    void Start()
    {
        setHealth();
        healthText();
        getPlayerName();
    }

    void Update()
    {
        Debug.Log(p1TriggerValue);
        Debug.Log(p2TriggerValue);
        Debug.Log(PlayerScript.isTurnOfP1);
        setSpecialBar();
        p1LockSpecialButton();
        p2LockSpecialButton();
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
        // p1SBtn.interactable = p1;
        p1LPBtn.interactable = p1;
        p1HPBtn.interactable = p1;
        p1LKBtn.interactable = p1;
        p1HKBtn.interactable = p1;
        // p2SBtn.interactable = p2;
        p2LPBtn.interactable = p2;
        p2HPBtn.interactable = p2;
        p2LKBtn.interactable = p2;
        p2HKBtn.interactable = p2;
    }

    private void dealDamageToP2(
        int currentHP,
        int damage,
        int accuracy,
        float duration,
        VideoClip attackVideo,
        VideoClip missedAttackVideo
    )
    {
        int randomValue = Random.Range(0, 101);
        if (randomValue <= accuracy)
        {
            PlayerScript.p2Health = currentHP -= damage;
            p1TriggeringSpecial(damage, PlayerScript.gameHealth);
            attack(attackVideo);
            StartCoroutine(delayedDamageToP2(damage, duration));
        }
        else
        {
            damage = 0;
            attack(missedAttackVideo);
            StartCoroutine(delayedDamageToP2(damage, duration));
        }
    }

    private void dealDamageToP1(
        int currentHP,
        int damage,
        int accuracy,
        float duration,
        VideoClip attackVideo,
        VideoClip missedAttackVideo
    )
    {
        int randomValue = Random.Range(0, 101);
        if (randomValue <= accuracy)
        {
            PlayerScript.p1Health = currentHP -= damage;
            p2TriggeringSpecial(damage, PlayerScript.gameHealth);
            attack(attackVideo);
            StartCoroutine(delayedDamageToP1(damage, duration));
        }
        else
        {
            damage = 0;
            attack(missedAttackVideo);
            StartCoroutine(delayedDamageToP1(damage, duration));
        }
    }

    private void p1TriggeringSpecial(float damage, float health)
    {
        if (damage == 3)
        {
            p1TriggerValue += damage * 1f / health;
        }
        else if (damage == 6)
        {
            p1TriggerValue += damage * 1.5f / health;
        }
        else if (damage == 8)
        {
            p1TriggerValue += damage * 1f / health;
        }
        else if (damage == 12)
        {
            p1TriggerValue += damage * 1.5f / health;
        }
    }

    private void p2TriggeringSpecial(float damage, float health)
    {
        if (damage == 3)
        {
            p2TriggerValue += damage * 2f / health;
        }
        else if (damage == 6)
        {
            p2TriggerValue += damage * 1.5f / health;
        }
        else if (damage == 8)
        {
            p2TriggerValue += damage * 2f / health;
        }
        else if (damage == 12)
        {
            p2TriggerValue += damage * 1.5f / health;
        }
    }

    private void p1LockSpecialButton()
    {
        if (PlayerScript.isTurnOfP1)
        {
            if (p1TriggerValue >= 1)
            {
                if (p1IsSpecialUsed)
                {
                    p1SBtn.interactable = false;
                }
                else
                {
                    p1SBtn.interactable = true;
                }
            }
        }
        else
        {
            p1SBtn.interactable = false;
        }
    }

    private void p2LockSpecialButton()
    {
        if (!PlayerScript.isTurnOfP1)
        {
            if (p2TriggerValue >= 1)
            {
                if (p2IsSpecialUsed)
                {
                    p2SBtn.interactable = false;
                }
                else
                {
                    p2SBtn.interactable = true;
                }
            }
        }
        else
        {
            p2SBtn.interactable = false;
        }
    }

    // setting player's special bar

    private void setSpecialBar()
    {
        p1TriggerBar.gameObject.GetComponent<Image>().fillAmount = p1TriggerValue;
        p2TriggerBar.gameObject.GetComponent<Image>().fillAmount = p2TriggerValue;
    }

    // delay when displaying damage/miss
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
        p1IsSpecialUsed = !p1IsSpecialUsed;
        dealDamageToP2(PlayerScript.p2Health, 25, 90, 0.5f, p1LowPunchVideo, p1LowPunchMissedVideo);
    }

    public void p1LowPunch()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP2(PlayerScript.p2Health, 3, 75, 0.5f, p1LowPunchVideo, p1LowPunchMissedVideo);
    }

    public void p1HighPunch()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP2(PlayerScript.p2Health, 8, 55, 0.5f, p1LowPunchVideo, p1LowPunchMissedVideo);
    }

    public void p1LowKick()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP2(PlayerScript.p2Health, 6, 65, 0.5f, p1LowPunchVideo, p1LowPunchMissedVideo);
    }

    public void p1HighKick()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP2(
            PlayerScript.p2Health,
            12,
            100,
            0.5f,
            p1LowPunchVideo,
            p1LowPunchMissedVideo
        );
    }

    public void p2Special()
    {
        whoToAttack();
        disableAllButtons();
        p2IsSpecialUsed = !p2IsSpecialUsed;
        dealDamageToP1(PlayerScript.p1Health, 25, 90, 0.5f, p2LowPunchVideo, p1LowPunchMissedVideo);
    }

    public void p2LowPunch()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP1(PlayerScript.p1Health, 3, 75, 0.5f, p2LowPunchVideo, p1LowPunchMissedVideo);
    }

    public void p2HighPunch()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP1(PlayerScript.p1Health, 8, 55, 0.5f, p2LowPunchVideo, p1LowPunchMissedVideo);
    }

    public void p2LowKick()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP1(PlayerScript.p1Health, 6, 65, 0.5f, p2LowPunchVideo, p1LowPunchMissedVideo);
    }

    public void p2HighKick()
    {
        whoToAttack();
        disableAllButtons();
        dealDamageToP1(PlayerScript.p1Health, 12, 45, 0.5f, p2LowPunchVideo, p1LowPunchMissedVideo);
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
