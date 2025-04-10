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
    // Start is called before the first frame update
    void Start()
    {
        // target = FindObjectOfType<PlayerController>().transform;
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
