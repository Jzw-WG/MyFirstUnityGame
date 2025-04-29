using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;
    public EnumWeaponType weaponType;
    // 在unity调试隐藏
    [HideInInspector]
    public bool statsUpdated;
    public Sprite icon;
    public void LevelUp() {
        if (weaponLevel < stats.Count - 1) {
            weaponLevel++;

            statsUpdated = true;
            // 已经升满的武器不再出现在面板中
            if (weaponLevel >= stats.Count - 1) {
                PlayerController.instance.fullyLeveledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class WeaponStats {
    public float speed, 
    damage, 
    range, 
    timeBetweenAttacks, 
    amount, 
    duration;
    public string upgradeText;
}
