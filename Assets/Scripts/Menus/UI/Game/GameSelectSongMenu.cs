using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameSelectSongMenu : UIMenu
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
		GameConfig.songData = songData;
		GameConfig.trackData = songData.trackList[0];

		Application.LoadLevel("Game");
	}

	private void OnBackButtonClick()
	{
		App.instance.back();
	}
}