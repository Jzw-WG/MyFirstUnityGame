using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    public Slider explvlSilder;
    public TMP_Text explvlText;
    public LevelUpSelectionButton[] levelUpButtons;
    public GameObject levelUpPanel;
    public TMP_Text coinAmountText;
    public PlayerStatUpgradeController moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponUpgradeDisplay;
    public TMP_Text timerText;
    public GameObject levelEndScreen;
    public TMP_Text endTimeText;
    public string mainMenuName;
    public GameObject pauseScreen;
    public TMP_Text speedUpText;
    private float gameSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentlevel)
    {
        explvlSilder.maxValue = levelExp;
        explvlSilder.value = currentExp;
        explvlText.text = "level: " + currentlevel;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        resumeTimeScale();
    }

    public void UpdateCoins()
    {
        coinAmountText.text = "Coins: " + CoinController.instance.currentCoins;
    }


    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            if (levelUpPanel.activeSelf == false)
            {
                resumeTimeScale();
            }
        }
    }

    public void SpeedChange()
    {
        if (gameSpeed == 1f)
        {
            gameSpeed = 2f;
            Time.timeScale = 2f;
            speedUpText.text = ">> X2";
        }
        else if (gameSpeed == 2f)
        {
            gameSpeed = 1f;
            Time.timeScale = 1f;
            speedUpText.text = ">> X1";
        }
    }

    public void resumeTimeScale()
    {
        Time.timeScale = gameSpeed;
    }
}
