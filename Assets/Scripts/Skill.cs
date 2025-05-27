using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int maxLevel;
    public float value;
    public int weaponLevel;
    // 在unity调试隐藏
    [HideInInspector]
    public bool statsUpdated;
    public Sprite icon;
    public void LevelUp() {
        if (weaponLevel < maxLevel) {
            weaponLevel++;

            statsUpdated = true;
            // 已经升满的武器不再出现在面板中
            if (weaponLevel >= maxLevel) {
                // PlayerController.instance.fullyLeveledWeapons.Add(this);
                // PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
}