using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulidManage : MonoBehaviour
{
	public static bulidManage Instance;

	[Header("References")]
	//[SerializeField] private GameObject[] towerPrefabs;
	[SerializeField] private Tower[] towerPrefabs;

	public int selectedTower = 0;
	private void Awake()
	{
		Instance =GetComponent<bulidManage>();
		//Instance = this;
	}

	public Tower getSelectedTower()
	{
		return towerPrefabs[selectedTower];
	}

	public void setSelectedTower(int _selectedTower)
	{
		selectedTower = _selectedTower;
	}
}
