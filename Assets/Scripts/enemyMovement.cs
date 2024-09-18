using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float moveSpeed;

	private Transform target;
	private int pathIndex = 0;
	private float baseSpeed;

	private void Awake()
	{
		baseSpeed = moveSpeed;
		rb = GetComponent<Rigidbody2D>();
		
	}
	private void Start()
	{
		
		target = levelManage.Instance.path[pathIndex];
	}

	private void Update()
	{
        if ((Vector2.Distance(target.position,transform.position) <= 0.1f))
        {
			pathIndex++;
			if (pathIndex == levelManage.Instance.path.Length)
			{
				enemySpawn.onEnemyDestroy.Invoke();
				Destroy(gameObject);
				return;
			}
			else
			{
				target = levelManage.Instance.path[pathIndex];
			}
        }
    }
	private void FixedUpdate()
	{
		Vector2 dir = (target.position - transform.position).normalized;
		rb.velocity = dir * moveSpeed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Defender")
		{
			Debug.Log("Va chạm xảy ra!");
		}
	}

	public void changeSpeed(float _speed)
	{
		moveSpeed = _speed;
	}

	public void resetSpeed()
	{
		moveSpeed = baseSpeed;
	}
}
