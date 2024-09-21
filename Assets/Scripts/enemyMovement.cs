using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float moveSpeed;
	private NavMeshAgent agent;
	private int waypointIndex = 0;  // Vị trí hiện tại trong danh sách các điểm
	private Transform[] waypoints;

	//private Transform target;
	//private int pathIndex = 0;
	private float baseSpeed;

	//private void Awake()
	//{
	//	agent = GetComponent<NavMeshAgent>();
	//	agent.updateRotation = false; // Tắt việc tự động xoay trong không gian 3D
	//	agent.updateUpAxis = false;   // Chuyển sang hệ tọa độ 2D (XZ)

	//}

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.speed = moveSpeed;
		waypoints = levelManage.Instance.path;
		if (waypoints.Length > 0)
		{
			agent.SetDestination(waypoints[waypointIndex].position);  // Di chuyển đến điểm đầu tiên
		}
	}

	private void Update()
	{
		if (!agent.pathPending && agent.remainingDistance < 0.5f)
		{
			GoToNextWaypoint();
		}
	}

	//private void FixedUpdate()
	//{
	//	Vector2 dir = (target.position - transform.position).normalized;
	//	rb.velocity = dir * moveSpeed;
	//}

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


	private void GoToNextWaypoint()
	{
		waypointIndex++;
		if (waypointIndex < waypoints.Length)
		{
			agent.SetDestination(waypoints[waypointIndex].position);  // Cập nhật điểm đến tiếp theo
		}
		else
		{
			// Đã đến điểm cuối cùng (có thể phá hủy đối tượng hoặc kích hoạt hành động khác)
			enemySpawn.onEnemyDestroy.Invoke();
			Destroy(gameObject);
		}
	}
	public void resetSpeed()
	{
		moveSpeed = baseSpeed;
	}
}
