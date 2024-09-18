using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float hp ; 
    [SerializeField] int currency = 15 ;

    private bool isDestroyed = false;
    public void takeDamage(float damage)
    {
        hp-= damage;
		hp = Mathf.Max(hp, 0); // Không cho hp giảm xuống dưới 0
		if ( hp <= 0 && !isDestroyed)
        {
            enemySpawn.onEnemyDestroy.Invoke();
            levelManage.Instance.IncreaseCurrency(currency);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
