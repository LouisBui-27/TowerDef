using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManage : MonoBehaviour
{
    public static UIManage Instance;
	private bool isHoveringUI;
	private void Awake()
	{
		Instance = this;
	}
	public void SetHoveringState(bool state)
	{
		isHoveringUI = state;
	}
	public bool IsHoveringUI()
	{
		return isHoveringUI; 
	}
}
