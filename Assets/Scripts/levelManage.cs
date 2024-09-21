using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManage : MonoBehaviour
{
	public static levelManage Instance { get; private set; }

	[SerializeField] public Transform startPoint;
	[SerializeField] public Transform endPoint;

	[SerializeField] public Transform[] path;

	private int currency;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			//DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		currency = 100;
	}

	public void IncreaseCurrency(int amount)
	{
		currency += amount;
	}

	public bool SpendCurrency(int amount)
	{
		if (amount <= currency)
		{
			currency -= amount;
			return true;
		}
		else
		{
			Debug.Log("Not enough currency.");
			return false;
		}
	}

	public int Currency
	{
		get { return currency; }
	}
}
