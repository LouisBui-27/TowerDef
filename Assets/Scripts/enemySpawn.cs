using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int baseEnemy = 8;
    [SerializeField] float enemyPerSecond = 0.5f;
    [SerializeField] float timeBetweenWaves = 5f;

    public static UnityEvent onEnemyDestroy = new UnityEvent() ;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    int enemiesAlive;
    int enemiesLeftToSpawn;
    bool isSpawn = false;

	private void Awake()
	{
		onEnemyDestroy.AddListener(EnemyDestroy);
		StartCoroutine(StartWave());
	}
	//private void Start()
	//{
	//	StartCoroutine(StartWave());
	//}
	private void Update()
	{
        if (!isSpawn) return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / enemyPerSecond) && enemiesLeftToSpawn > 0)
        {
            spawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesLeftToSpawn == 0 && enemiesAlive == 0)
        {
            endWave();
        }
	}

    private void EnemyDestroy()
    {
        enemiesAlive--;
    }
	private IEnumerator StartWave()
    {
        isSpawn = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        yield return new WaitForSeconds(timeBetweenWaves);
        
    }

    private void endWave()
    {
        isSpawn = false;
		timeSinceLastSpawn = 0f;
        currentWave++;
		StartCoroutine(StartWave());
	}
    private void spawnEnemy()
    {
        GameObject prefabsToSpawn = enemyPrefabs[currentWave - 1];
        Instantiate(prefabsToSpawn, levelManage.Instance.startPoint.position, Quaternion.identity);
    }
    private int EnemiesPerWave()
    { 
        return Mathf.RoundToInt(baseEnemy * Mathf.Pow(currentWave, 0.75f));
    }
    
}
