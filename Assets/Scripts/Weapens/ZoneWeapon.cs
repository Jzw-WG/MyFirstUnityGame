using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    public EnemyDamager damager;
    private float spawnTime, spawnCounter;

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

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0) {
            spawnCounter = spawnTime;

            Instantiate(damager, damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);
        }
    }

    void SetStats() {
        damager.damageAmount = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        damager.timeBetweenDamage = stats[weaponLevel].speed;
        damager.lifeTime = stats[weaponLevel].duration;
        spawnTime = stats[weaponLevel].timeBetweenAttacks;
        spawnCounter = 0f;
    }
}
