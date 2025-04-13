using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentlevel) {
        explvlSilder.maxValue = levelExp;
        explvlSilder.value = currentExp;
        explvlText.text = "level: " + currentlevel;
    }

    public void SkipLevelUp() {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
