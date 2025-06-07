using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    public static PlayerStatController instance;

    private void Awake()
    {
        instance = this;
    }

    public List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapons;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;
    public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponLevel;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatValue(health[i].cost + (health[i].cost - health[i - 1].cost), health[i].value + (health[i].value - health[i - 1].value)));
        }

        for (int i = moveSpeed.Count - 1; i < moveSpeedLevelCount; i++)
        {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }

        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 防止第一帧是错误数据
        if (UIController.instance.levelUpPanel.activeSelf == true) {
            UpdateDisplay();
        }
    }
    
    public void UpdateDisplay() {
        if (moveSpeedLevel < moveSpeed.Count - 1) {
            UIController.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        } else {
            UIController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }

        if (healthLevel < health.Count - 1) {
            UIController.instance.healthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        } else {
            UIController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }

        if (pickupRangeLevel < pickupRange.Count - 1) {
            UIController.instance.pickupRangeUpgradeDisplay.UpdateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
        } else {
            UIController.instance.pickupRangeUpgradeDisplay.ShowMaxLevel();
        }

        if (maxWeaponLevel < maxWeapons.Count - 1) {
            UIController.instance.maxWeaponUpgradeDisplay.UpdateDisplay(maxWeapons[maxWeaponLevel + 1].cost, maxWeapons[maxWeaponLevel].value, maxWeapons[maxWeaponLevel + 1].value);
        } else {
            UIController.instance.maxWeaponUpgradeDisplay.ShowMaxLevel();
        }

    }

    public void PurchaseMoveSpeed() {
        moveSpeedLevel++;
        CoinController.instance.SpendCoins(moveSpeed[moveSpeedLevel].cost);
        UpdateDisplay();

        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
    }

    public void PurchaseHealth() {
        healthLevel++;
        CoinController.instance.SpendCoins(health[healthLevel].cost);
        UpdateDisplay();

        PlayerHealthController.instance.maxHealth = health[healthLevel].value;
        PlayerHealthController.instance.currentHealth += health[healthLevel].value - health[healthLevel - 1].value;
    }

    public void PurchasePickupRange() {
        pickupRangeLevel++;
        CoinController.instance.SpendCoins(pickupRange[pickupRangeLevel].cost);
        UpdateDisplay();

        PlayerController.instance.pickupRange = pickupRange[pickupRangeLevel].value;
    }

    public void PurchaseMaxWeapons() {
        maxWeaponLevel++;
        CoinController.instance.SpendCoins(maxWeapons[maxWeaponLevel].cost);
        UpdateDisplay();

        PlayerController.instance.maxWeapons = Mathf.RoundToInt(maxWeapons[maxWeaponLevel].value);
    }
}

[System.Serializable]
public class PlayerStatValue {
    public int cost;
    public float value;

    public PlayerStatValue(int cost, float value) {
        this.cost = cost;
        this.value = value;
    }
}
