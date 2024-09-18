using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
	[SerializeField] Transform turretRotatePoint;
	[SerializeField] private float targetRange;
	[SerializeField] private float rotationSpeed = 5f;
	[SerializeField] private LayerMask enemyMask;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Transform firePoint;
	[SerializeField] GameObject upgradeUI;
	[SerializeField] Button upgradeButton;
	[SerializeField] int baseUpdateCost = 100;


	[SerializeField] private float bps = 1f;// Bullet Per Second 
	private Transform target;
	private float timeUntilFire;
	private int levelTower = 1;


	private void Update()
	{
		if(target == null)
		{
			findTarget();
			return;
		}
		RotateTowardsTarget();

		if (!checkTargetIsInRange())
		{
			target = null;
		}
		else
		{
			timeUntilFire += Time.deltaTime;
            if (timeUntilFire>= 1f/bps)
            {
				Shoot();
				timeUntilFire = 0f;
			}

		}
	}
	private void Start()
	{
		upgradeButton.onClick.AddListener(upGrade);
	}
	private bool checkTargetIsInRange()
	{
		return Vector2.Distance(target.position,transform.position) <= targetRange;
	}
	private void findTarget()
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange, (Vector2)transform.position, 0f, enemyMask);
		if (hits.Length > 0)
		{
			target = hits[0].transform;
		}
	}

	private void RotateTowardsTarget()
	{
		float angle = Mathf.Atan2(target.position.y - transform.position.y,target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f,0f,angle));
		turretRotatePoint.rotation = Quaternion.RotateTowards(turretRotatePoint.rotation,targetRotation, rotationSpeed * Time.deltaTime);
	}

	private void Shoot()
	{
		GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
		Bullet bullet = bulletObj.GetComponent<Bullet>();
		bullet.setTarget(target);
	}
	public void openUpgradeUI()
	{
		upgradeUI.SetActive(true);
	}
	public void closeUpgradeUI()
	{
		upgradeUI.SetActive(false);
		UIManage.Instance.SetHoveringState(false);
	}
	public void upGrade()
	{
		if (baseUpdateCost > levelManage.Instance.Currency) return;
		if (levelTower <=3)
		{
			levelManage.Instance.SpendCurrency(baseUpdateCost);
			bps = (float)(bps * 1.5);
			targetRange = (float)(targetRange * 1.5);
			closeUpgradeUI();
			levelTower++;
		}
		
	}
	private void OnDrawGizmosSelected()
	{
		Handles.color = Color.gray;
		Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
	}


}
