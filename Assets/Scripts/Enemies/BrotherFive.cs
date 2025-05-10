using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherFive : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float fireInterval = 4f;
    public float fireRange = 8f;
    private float fireCounter;
    private Transform player;

    void Start()
    {
        fireCounter = 0;
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= fireRange) {
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireInterval;
                Instantiate(bubblePrefab, transform.position, Quaternion.identity).SetActive(true);
            }
        } else {
            // 如果距离太远，也可以重置冷却，或者让五娃移动靠近
            fireCounter = 0;
        }
    }
}
