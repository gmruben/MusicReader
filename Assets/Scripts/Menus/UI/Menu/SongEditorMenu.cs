using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SongEditorMenu : UIMenu
{
	public UIButton newSongButton;
	public UIButton existingSongButton;

	public UIButton backButton;

	private bool comeFromEditorMenu;

	public void Init()
	{
		this.comeFromEditorMenu = comeFromEditorMenu;

		newSongButton.onClick += OnNewSongButtonClick;
		existingSongButton.onClick += OnExistingSongButtonClick;

		backButton.onClick += OnBackButtonClick;
	}

	private void OnNewSongButtonClick()
	{
		App.instance.ShowCreateSongMenu();
	}

	private void OnExistingSongButtonClick()
	{
		App.instance.ShowEditorSelectSongMenu();
	}

	private void OnBackButtonClick()
	{
		App.instance.back();
	}
}