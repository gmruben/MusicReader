using UnityEngine;
using System.Collections;

public class UIManager
{
	private static UIManager _instance;
	
	public Camera camera;

	public static UIManager instance
	{
		get
		{
			if (_instance == null) _instance = new UIManager();
			return _instance;
		}
	}
	
	private UIManager()
	{
		camera = GameObject.Find("UICamera").camera;
	}
}