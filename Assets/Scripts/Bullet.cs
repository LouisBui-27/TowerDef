using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D Rigidbody2D;
    [SerializeField] float bulletSpeed = 5f;
	[SerializeField] float bulletDamage = 1f;
    private Transform target;


	public void setTarget(Transform _target)
	{
		target = _target; 
	}
	private void FixedUpdate()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}
		
		Vector2 dir = (target.position - transform.position).normalized;
		Rigidbody2D.velocity = dir * bulletSpeed;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		collision.gameObject.GetComponent<Health>().takeDamage(bulletDamage);
		Destroy(gameObject);
	}
}
