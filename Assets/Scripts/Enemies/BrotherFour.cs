using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherFour : MonoBehaviour
{
    public float fireRange = 8f;
    private Transform player;
    public GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
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
            fire.SetActive(true);
        } else {
            fire.SetActive(false);
        }
    }
}
