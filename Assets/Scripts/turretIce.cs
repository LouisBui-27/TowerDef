using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class turretIce : MonoBehaviour
{
	[SerializeField] private LayerMask enemyMask;
	[SerializeField] private float targetRange;
	[SerializeField] private float bps = 1f;// Bullet Per Second 
	[SerializeField] private float freezeTime = 2f;// Bullet Per Second 
	private float timeUntilFire;
	private void Update()
	{
		
		timeUntilFire += Time.deltaTime;
		if (timeUntilFire >= 1f / bps)
		{
			FreezeInRange();
			timeUntilFire = 0f;
		}
	}

	private void FreezeInRange()
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange, (Vector2)transform.position, 0f, enemyMask);

		if (hits.Length > 0)
		{
			for (int i = 0; i < hits.Length; i++)
			{
				RaycastHit2D hit = hits[i];

				enemyMovement enemyMovement = hit.transform.GetComponent<enemyMovement>();
				enemyMovement.changeSpeed(0.5f);

				StartCoroutine(resetEnemySpeed(enemyMovement));
			}
		}
	}

	private IEnumerator resetEnemySpeed(enemyMovement enemyMovement)
	{
		yield return new WaitForSeconds(freezeTime);

		enemyMovement.resetSpeed();
	}

	private void OnDrawGizmosSelected()
	{
		Handles.color = Color.gray;
		Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
	}
}
