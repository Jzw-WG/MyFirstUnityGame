using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : Weapon
{
    public EnemyDamager damager;
    private float throwCounter;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (statsUpdated == true) {
            statsUpdated = false;

            SetStats();
        }

        throwCounter -= Time.deltaTime;
        if (throwCounter <= 0) {
            throwCounter = stats[weaponLevel].timeBetweenAttacks;

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                Instantiate(damager, damager.transform.position, damager.transform.rotation).gameObject.SetActive(true);
            }

            SFXManager.instance.PlaySFXPitched(1);
        }
    }

    public void SetStats() {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        damager.lifeTime = stats[weaponLevel].duration;
        throwCounter = 0f;
    }
}
