using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningMessage : MonoBehaviour
{
	public Text messageText;

	void Awake()
	{
		Hide();		
	}
	
	public void Show()
	{
		this.gameObject.SetActive(true);
	}

	public void Hide()
	{
		this.gameObject.SetActive(false);
	}
	
	public void SetMessage(string message)
	{
		messageText.text = message;
	}
}
