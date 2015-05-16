using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EditorSelectSongMenu : UIMenu
{
	public EditorSongList songList;
	public UIButton backButton;

	public void Init()
	{
		songList.Init();

		songList.onSongClick += onSongClick;
		backButton.onClick += OnBackButtonClick;
	}

	private void onSongClick(SongData songData)
	{
		App.instance.ShowEditSongMenu(songData, true);
	}

	private void OnBackButtonClick()
	{
		App.instance.back();
	}
}