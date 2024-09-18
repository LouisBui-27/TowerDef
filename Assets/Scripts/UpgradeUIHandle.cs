using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] bool mouse_over = false;
	private void Update()
	{
		if (mouse_over) Debug.Log("Mouse_over " + name);
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		mouse_over = true;
		UIManage.Instance.SetHoveringState(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouse_over = false;
		UIManage.Instance.SetHoveringState(false);
		gameObject.SetActive(false);
	}
}
