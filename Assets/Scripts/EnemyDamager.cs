using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;

    public float lifeTime, growSpeed = 5f;
    private Vector3 targetSize;
    public bool shouldKnockBack;
    public bool destroyParent;
    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;
    private List<EnemyController> enemiesInRange = new List<EnemyController>();
    public bool destroyOnImpact;
    public EnumWeaponType weaponType;
    // Start is called before the first frame update
    void Start()
    {
        // 不再是固定时间突然消失
        // Destroy(gameObject, lifeTime);

        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // 法球生成时逐渐变大
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            // 生命周期结束后逐渐缩小到销毁
            targetSize = Vector3.zero;
            if (transform.localScale.x <= 0.001f) {
                Destroy(gameObject);

                // 销毁法球容器
                if (destroyParent == true) {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
        // 区域持续伤害
        if (damageOverTime == true) {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0) {
                damageCounter = timeBetweenDamage;
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    if (enemiesInRange[i] != null) {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack, weaponType);
                    } else {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (damageOverTime == false) {
            if (collision.tag == "Enemy") {
                collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack, weaponType);
                if (destroyOnImpact == true) {
                    Destroy(gameObject);
                }
            }
        } else {
            if (collision.tag == "Enemy") {
                EnemyController ec = collision.GetComponent<EnemyController>();
                if (!enemiesInRange.Contains(ec)) {
                    enemiesInRange.Add(ec);
                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (damageOverTime == true) {
            if (collision.tag == "Enemy") {
                EnemyController ec = collision.GetComponent<EnemyController>();
                if (ec != null) {
                    enemiesInRange.Remove(ec);
                }
            }
        }
    }
}
