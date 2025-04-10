using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float rotateSpeed;

    public Transform holder, fireballToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 生成法球
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0) {
            spawnCounter = timeBetweenSpawn;
            Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);
        }
    }
}
