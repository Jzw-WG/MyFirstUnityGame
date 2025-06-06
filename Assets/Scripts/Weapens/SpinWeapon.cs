using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    public float rotateSpeed;

    public Transform holder, fireballToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;
    public EnemyDamager damager;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();

        // UIController.instance.levelUpButtons[0].UpdateButtonDisplay(this);
    }

    // Update is called once per frame
    void Update()
    {
        // 生成法球
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0) {
            spawnCounter = timeBetweenSpawn;
            // Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);
            
            // 根据武器数量圆周对称生成对应数量的球体
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = i * (360f / stats[weaponLevel].amount);
                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
            SFXManager.instance.PlaySFX(13);
        }

        if (statsUpdated == true) {
            statsUpdated = false;

            SetStats();
        }
    }

    public void SetStats() {
        damager.damageAmount = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        damager.lifeTime = stats[weaponLevel].duration;
        spawnCounter = 0f;
    }
}
