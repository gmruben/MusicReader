using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CreateNewSongMenu : UIMenu
{
	public InputField songNameInput;

	public UIButton okButton;
	public UIButton backButton;

	public void Init()
	{
		okButton.onClick += OnOkButtonClick;
		backButton.onClick += OnBackButtonClick;
	}

	private void OnOkButtonClick()
	{
		//Create the new song
		SongData songData = createSongData(songNameInput.text); 

		UserSongDataStore.storeSongData(songData);
		App.instance.ShowEditSongMenu(songData, false);
	}

	private void OnBackButtonClick()
	{
		App.instance.back();
	}

	private SongData createSongData(string songName)
	{
		SongData songData = new SongData();
		
		songData.id = songName.Trim();
		songData.name = songName;

		return songData;
	}
}