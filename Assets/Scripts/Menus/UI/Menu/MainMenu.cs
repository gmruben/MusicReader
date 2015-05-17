using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : UIMenu
{
	public UIButton gameButton;
	public UIButton editorButton;

	public void Init()
	{
		gameButton.onClick += OnGameButtonClick;
		editorButton.onClick += OnEditorButtonClick;
	}

	private void OnGameButtonClick()
	{
		App.instance.ShowGameSelectSongMenu();
	}

	private void OnEditorButtonClick()
	{
		App.instance.ShowSongEditorMenu();
	}
}