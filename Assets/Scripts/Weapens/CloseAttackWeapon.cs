using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackWeapon : Weapon
{
    public EnemyDamager damager;
    private float attackCounter, direction;
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
        attackCounter -= Time.deltaTime;
        if (attackCounter <= 0) {
            attackCounter = stats[weaponLevel].timeBetweenAttacks;
            // 根据移动方向改变武器朝向
            direction = Input.GetAxisRaw("Horizontal");
            if (direction != 0) {
                if (direction > 0) {
                    damager.transform.rotation = Quaternion.identity;
                } else {
                    damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }
            }
            Instantiate(damager, damager.transform.position, damager.transform.rotation, transform).gameObject.SetActive(true);
        
            // 根据武器数量圆周对称生成对应数量的武器
            for (int i = 1; i < stats[weaponLevel].amount; i++)
            {
                float rot = i * (360f / stats[weaponLevel].amount);
                Instantiate(damager, damager.transform.position, Quaternion.Euler(0f, 0f, damager.transform.rotation.eulerAngles.z + rot), transform).gameObject.SetActive(true);
            }

            SFXManager.instance.PlaySFXPitched(9);
        }

    }

    void SetStats() {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;
        damager.lifeTime = stats[weaponLevel].duration;
        attackCounter = 0f;
    }
}
