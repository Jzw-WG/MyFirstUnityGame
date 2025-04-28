using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    // 每次相对下一级的经验值
    public int currentExperience;

    public ExpPickup pickup;

    public List<int> expLevels;
    public int currentlevel= 1, levelCount = 100;
    public List<Weapon> weaponsToUpgrade;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 设置每次升级需要的经验
        while(expLevels.Count < levelCount) {
            expLevels.Add(Mathf.RoundToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int amocuntToGet) {
        currentExperience += amocuntToGet;

        if (currentExperience >= expLevels[currentlevel]) {
            LevelUp();
        }
        UIController.instance.UpdateExperience(currentExperience, expLevels[currentlevel], currentlevel);
        SFXManager.instance.PlaySFXPitched(6);
    }

    public void SpawnExp(Vector3 position, int expValue) {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    void LevelUp() {

        currentExperience -= expLevels[currentlevel];
        currentlevel++;
        // 等级上限之后可以重复升级
        if (currentlevel >= expLevels.Count) {
            currentlevel = expLevels.Count - 1;
        }

        // PlayerController.instance.activeWeapon.LevelUp();
        UIController.instance.levelUpPanel.SetActive(true);
        Time.timeScale = 0;
        // UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.activeWeapon);
        // UIController.instance.levelUpButtons[0].UpdateButtonDisplay(PlayerController.instance.assignedWeapons[0]);
        // UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        // UIController.instance.levelUpButtons[2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[1]);
        weaponsToUpgrade.Clear();
        List<Weapon> availableWeapons = new List<Weapon>();
        // 添加一个已解锁武器到升级面板
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);
        if (availableWeapons.Count > 0) {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }
        // 解锁新的武器
        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLeveledWeapons.Count < PlayerController.instance.maxWeapons) {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0) {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }
        }
        for (int i = 0; i < weaponsToUpgrade.Count; i++) {
            UIController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count) {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            } else {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        PlayerStatController.instance.UpdateDisplay();
    }
}
