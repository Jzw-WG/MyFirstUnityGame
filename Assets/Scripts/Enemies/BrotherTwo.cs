using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherTwo : MonoBehaviour
{
    public float fireRange = 8f;
    private Transform player;
    public EnemyController enemyController;
    private float originalMoveSpeed;
    public float speedUpRate;
    // Start is called before the first frame update
    void Start()
    {
        originalMoveSpeed = enemyController.moveSpeed;
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= fireRange) {
            enemyController.moveSpeed = speedUpRate * originalMoveSpeed;
        } else {
            enemyController.moveSpeed = originalMoveSpeed; 
        }
    }
}
