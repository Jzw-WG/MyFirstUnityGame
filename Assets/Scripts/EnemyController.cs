using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;

    public float damage;

    public float hitWaitTime = 1f;

    private float hitCounter;

    public float health = 5f;
    public float maxHealth;

    public float knockBackTime = .5f;
    private float knockBackCounter;

    public int expToGive = 1;

    public int coinValue = 1;
    public float coinDropRate = 0.2f;
    public EnumBrotherType brotherType;
    public float knockBackSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        maxHealth = health;
    }
    void Start()
    {
        // target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.gameObject.activeSelf == true) {
            // 受击击退
            if (knockBackCounter > 0) {
                knockBackCounter -= Time.deltaTime;
                if (moveSpeed > 0) {
                    moveSpeed = -moveSpeed * knockBackSpeed;
                }

                if (knockBackCounter <= 0) {
                    moveSpeed = Mathf.Abs(moveSpeed / knockBackSpeed);
                }
            }


            // 这里不需要deltatime也能保持不同帧率很定速度
            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;
            if (hitCounter > 0f) {
                hitCounter -= Time.deltaTime;
            }
        } else {
            // 玩家死亡后怪物静止
            theRB.velocity = Vector2.zero;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" && hitCounter <= 0f) {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake, EnumWeaponType weaponType) {
        // 三娃收到的铁器伤害减半
        if (brotherType.Equals(EnumBrotherType.Yellow) && EnumWeaponType.Iron.Equals(weaponType)) {
            damageToTake /= 2;
        }
        health -= damageToTake;
        if (health <= 0) {
            Destroy(gameObject);

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
            if (Random.value <= coinDropRate) {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
            SFXManager.instance.PlaySFXPitched(2);
        } else {
            if (brotherType.Equals(EnumBrotherType.Yellow) && EnumWeaponType.Iron.Equals(weaponType)) {
                SFXManager.instance.PlaySFXPitched(15);
            } else {
                SFXManager.instance.PlaySFXPitched(16);
            }
        }
        //显示伤害数字
        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack, EnumWeaponType weaponType) {
        TakeDamage(damageToTake, weaponType);
        if (shouldKnockBack == true) {
            knockBackCounter = knockBackTime;
        }
    }
}
