using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;
    private Weapon assignedWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateButtonDisplay(Weapon weapon) {
        if (weapon.gameObject.activeSelf == true) {
            upgradeDescText.text = weapon.stats[weapon.weaponLevel].upgradeText;
            weaponIcon.sprite = weapon.icon;
            nameLevelText.text = weapon.name + " - Level " + weapon.weaponLevel;
        } else {
            upgradeDescText.text = "Unlock " + weapon.name;
            weaponIcon.sprite = weapon.icon;
            nameLevelText.text = weapon.name;
        }
        
        assignedWeapon = weapon;
    }

    public void SelectUpgrade() {
        if (assignedWeapon != null) {
            if (assignedWeapon.gameObject.activeSelf == true) {
                assignedWeapon.LevelUp();
            } else {
                PlayerController.instance.AddWeapon(assignedWeapon);
            }
            UIController.instance.levelUpPanel.SetActive(false);
            UIController.instance.resumeTimeScale();
        }
    }
}
