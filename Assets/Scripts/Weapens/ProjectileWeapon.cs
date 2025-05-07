using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public EnemyDamager damager;
    public PlayerDamager playerDamager;
    public Projectile projectile;

    private float shotCounter;
    public float weaponRange;
    public LayerMask whatIsEnemy;
    public int SFXIndex;
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

        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) {
            shotCounter = stats[weaponLevel].timeBetweenAttacks;

            // 发射方向
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            if (enemies.Length > 0) {
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;
                    Vector3 direction = targetPosition - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    // 生成后不跟随玩家
                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                }
                SFXManager.instance.PlaySFXPitched(SFXIndex);
            }
        }
    }

    public void SetStats() {
        shotCounter = 0f;
        projectile.moveSpeed = stats[weaponLevel].speed;
        if (damager != null) {
            damager.damageAmount = stats[weaponLevel].damage;
            damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
            damager.lifeTime = stats[weaponLevel].duration;
            return;
        }
        if (playerDamager != null) {
            playerDamager.damageAmount = stats[weaponLevel].damage;
            playerDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;
            playerDamager.lifeTime = stats[weaponLevel].duration;
            return;
        }
    }
}
