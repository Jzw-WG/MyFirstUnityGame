using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    public float damageAmount;
    public float lifeTime, growSpeed = 5f;
    private Vector3 targetSize;
    public bool destroyParent;
    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;
    public bool destroyOnImpact;
    // Start is called before the first frame update
    void Start()
    {
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
                PlayerHealthController.instance.TakeDamage(damageAmount);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (damageOverTime == false) {
            if (collision.tag == "Player") {
                PlayerHealthController.instance.TakeDamage(damageAmount);
                if (destroyOnImpact == true) {
                    Destroy(gameObject);
                }
            }
        } else {
            if (collision.tag == "Player") {
                PlayerHealthController.instance.TakeDamage(damageAmount);
            }
        }
        
    }
}
