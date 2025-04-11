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
            if (transform.localScale.x == 0f) {
                Destroy(gameObject);

                // 销毁法球容器
                if (destroyParent == true) {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            other.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack);
        }
    }
}
