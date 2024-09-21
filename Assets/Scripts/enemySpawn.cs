using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class enemySpawn : MonoBehaviour
{
    [SerializeField]EnemyWave[] waves;
    [SerializeField] float enemyPerSecond = 0.5f;
    [SerializeField] float timeBetweenWaves = 5f;

    public static UnityEvent onEnemyDestroy = new UnityEvent() ;

    public int currentWave = 0;
    private float timeSinceLastSpawn;
    int enemiesAlive;
    int enemiesLeftToSpawn;
    bool isSpawn = false;
	private int currentEnemyIndex = 0;
	private void Awake()
	{
		onEnemyDestroy.AddListener(EnemyDestroy);
	}
	private void Start()
	{
		StartCoroutine(StartWave());
	}
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
		if (currentWave > waves.Length)
		{
			yield break;  // Không còn wave để spawn
		}

		var wave = waves[currentWave];
		enemiesLeftToSpawn = 0;
		currentEnemyIndex = 0;
		// Tính tổng số kẻ địch sẽ spawn trong wave này
		for (int i = 0; i < wave.quantities.Length; i++)
		{
			enemiesLeftToSpawn += wave.quantities[i];
		}

		isSpawn = true;
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
		var wave = waves[currentWave];
		if (currentEnemyIndex >= wave.enemies.Length) return;
		var enemyPrefab = wave.enemies[currentEnemyIndex];
		GameObject spawnedEnemy = Instantiate(enemyPrefab, levelManage.Instance.startPoint.position, Quaternion.identity);

		NavMeshAgent agent = spawnedEnemy.GetComponent<NavMeshAgent>();
		if (agent == null)
		{
			agent = spawnedEnemy.AddComponent<NavMeshAgent>();
			agent.updateRotation = false;  // NavMeshAgent trên môi trường 2D
			agent.updateUpAxis = false;
		}

		// Sau khi tạo kẻ địch, thêm component NavMeshAgent nếu chưa có
		wave.quantities[currentEnemyIndex]--;
		if (wave.quantities[currentEnemyIndex] <= 0)
		{
			currentEnemyIndex++; // Chuyển sang loại kẻ địch tiếp theo
		}
		Debug.Log("spawn");
	}
    
}
