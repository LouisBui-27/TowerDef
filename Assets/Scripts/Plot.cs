using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
	[Header("References")]
	[SerializeField] SpriteRenderer SpriteRenderer;
	//[SerializeField] Color hoverColor;

	private GameObject tower;
	private Turret turret;
	//private Color startColor;

	private void Start()
	{
		//startColor = SpriteRenderer.color;
	}
	private void OnMouseEnter()
	{
		Debug.Log("enter");
		//SpriteRenderer.color = hoverColor;
	}

	private void OnMouseExit()
	{
		Debug.Log("exit");
		//SpriteRenderer.color = startColor;
	}
	private void OnMouseDown()
	{
		if (UIManage.Instance.IsHoveringUI()) return;
		if (tower != null)
		{
			turret.openUpgradeUI();
			return;
		} 

		Tower gameObject = bulidManage.Instance.getSelectedTower();
		if(gameObject.cost > levelManage.Instance.Currency)
		{
			Debug.Log("u can't bulid this tower");
			return;
		}
		levelManage.Instance.SpendCurrency(gameObject.cost);
		//GameObject gameObject = buildManage.Instance.getSelectedTower();
		tower = Instantiate(gameObject.prefabs, transform.position, Quaternion.identity);
		turret = tower.GetComponent<Turret>();
	}
	
}
