using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    public float knockBackTime = .5f;
    private float knockBackCounter;
    // Start is called before the first frame update
    void Start()
    {
        // target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 受击击退
        if (knockBackCounter > 0) {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0) {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter <= 0) {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }


        // 这里不需要deltatime也能保持不同帧率很定速度
        theRB.velocity = (target.position - transform.position).normalized * moveSpeed;
        if (hitCounter > 0f) {
            hitCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" && hitCounter <= 0f) {
            PlayerHealthController.instance.TakeDamage(damage);
            hitCounter = hitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake) {
        health -= damageToTake;
        if (health <= 0) {
            Destroy(gameObject);
        }
        //显示伤害数字
        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack) {
        TakeDamage(damageToTake);
        if (shouldKnockBack == true) {
            knockBackCounter = knockBackTime;
        }
    }
}
