using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
	public GameObject[] enemies;  // Danh sách các kẻ địch cho wave này
	public int[] quantities;       // Số lượng của từng loại kẻ địch
}
