using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyUI;
	[SerializeField] Animator animator;

	private bool isOpen;
	private void Awake()
	{
		animator = GetComponent<Animator>();
		isOpen = true;
	}

	public void toggleMenu()
	{
		isOpen = !isOpen;
		animator.SetBool("menuOpen", isOpen);
	}
	private void OnGUI()
	{
		currencyUI.text = levelManage.Instance.Currency.ToString();
	}

	public void setSelected()
	{

	}
}
