using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawn, maxSpawn;
    private Transform target;
    private float despawnDistance;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public int checkPerFrame;
    private int enemyToCheck;

    public List<WaveInfo> waves;
    public List<WaveInfo> infinateWaves;
    private int currentWave;
    private float waveCounter;
    private int enemiesSpawnedInWave; 
    private infinateWaveInfo preWaveEnemyInfo = new infinateWaveInfo();
    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn;

        target = PlayerHealthController.instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;
    
        currentWave = -1;

        enemiesSpawnedInWave = 0;
        GoToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        // spawnCounter -= Time.deltaTime;
        // if (spawnCounter <= 0) {
        //     spawnCounter = timeToSpawn;

        //     // Instantiate(enemyToSpawn, transform.position, transform.rotation);
        //     GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
        //     spawnedEnemies.Add(newEnemy);
        // }

        // 按波次生成敌人
        if (PlayerHealthController.instance.gameObject.activeSelf) {
            if (currentWave < waves.Count) {
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0) {
                    GoToNextWave();
                }
                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0) {
                    spawnCounter = waves[currentWave].timeBetweenSpawn;

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                    EnemyController curEnemy = newEnemy.GetComponent<EnemyController>();

                    // 进入无限
                    if (infinateWaves.Count > 0 && preWaveEnemyInfo != null)
                    {
                        // 每波次都比上一波强化
                        curEnemy.damage = preWaveEnemyInfo.damage * 1.1f;
                        curEnemy.expToGive = preWaveEnemyInfo.expToGive + 5;
                        curEnemy.coinValue = preWaveEnemyInfo.coinValue + 5;
                        curEnemy.health = preWaveEnemyInfo.maxHealth * 1.1f;
                        curEnemy.maxHealth = preWaveEnemyInfo.maxHealth * 1.1f;
                    }
                    enemiesSpawnedInWave++;
                    // 每波次最后一次生成时记录为上一波的敌人属性
                    if (waveCounter <= waves[currentWave].timeBetweenSpawn)
                    {
                        preWaveEnemyInfo.damage = curEnemy.damage;
                        preWaveEnemyInfo.health = curEnemy.health;
                        preWaveEnemyInfo.maxHealth = curEnemy.maxHealth;
                        preWaveEnemyInfo.expToGive = curEnemy.expToGive;
                        preWaveEnemyInfo.coinValue = curEnemy.coinValue;
                    }
                    spawnedEnemies.Add(newEnemy);
                }
            }
        }

        transform.position = target.position;

        // 销毁离玩家太远的敌人
        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget) {
            if (enemyToCheck < spawnedEnemies.Count) {
                if (spawnedEnemies[enemyToCheck] != null) {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance) {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    } else {
                        enemyToCheck++;
                    }
                } else {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            } else {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 SelectSpawnPoint() {
        // 视野外的边界处，随机生成敌人
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;
        if (spawnVerticalEdge) {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);
            if (Random.Range(0f, 1f) > .5f) {
                spawnPoint.x = maxSpawn.position.x;
            } else {
                spawnPoint.x = minSpawn.position.x;
            }
        } else {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            if (Random.Range(0f, 1f) > .5f) {
                spawnPoint.y = maxSpawn.position.y;
            } else {
                spawnPoint.y = minSpawn.position.y;
            }
        }
        return spawnPoint;
    }

    public void GoToNextWave() {
        currentWave++;
        enemiesSpawnedInWave = 0;
        if (currentWave >= waves.Count)
        {
            if (infinateWaves.Count == 0)
            {
                // 确定最无限的怪物列表
                for (int i = 0; i < waves.Count; i++)
                {
                    EnemyController controller = waves[i].enemyToSpawn.GetComponent<EnemyController>();
                    if (controller.brotherType == EnumBrotherType.Unknown)
                    {
                        continue;
                    }
                    WaveInfo info = new WaveInfo();
                    info.enemyToSpawn = waves[i].enemyToSpawn;
                    info.waveLength = waves[i].waveLength;
                    info.timeBetweenSpawn = waves[i].timeBetweenSpawn;
                    infinateWaves.Add(info);
                }
                waves = infinateWaves;
            }
            currentWave = 0;
        }
        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawn;
    }
}

// 可以在unity中展示
[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawn = 1f;
}

public class infinateWaveInfo {
    public float damage;
    public float health;
    public float maxHealth;
    public int expToGive;
    public int coinValue;
}
