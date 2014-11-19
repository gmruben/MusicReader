using UnityEngine;
using System.Collections;

public class EditorMenu : UIMenu
{
	public UIButton loadButton;

	void Start()
	{
		init ();
	}

	public void init()
	{
		loadButton.onClick += onLoadButtonClick;
	}

	private void onLoadButtonClick()
	{
		Debug.Log ("LOAD");
	}
}