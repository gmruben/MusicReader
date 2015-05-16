using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EditSongMenu : UIMenu
{
	public Text title;
	public EditorInstrumentList instrumentList;

	public UIButton newInstrumentButton;
	public UIButton backButton;

	private SongData songData;
	private bool comeFromEditorMenu;

	public void Init(SongData songData, bool comeFromEditorMenu)
	{
		this.songData = songData;
		this.comeFromEditorMenu = comeFromEditorMenu;

		title.text = "EDITOR - EDIT SONG (" + songData.name + ")";

		instrumentList.Init(songData);
		instrumentList.onInstrumentClick += OnInstrumentClick;

		newInstrumentButton.onClick += OnNewInstrumentButtonClick;
		backButton.onClick += OnBackButtonClick;
	}

	private void OnInstrumentClick(string intrumentId)
	{
		Application.LoadLevel("Editor");
	}

	private void OnNewInstrumentButtonClick()
	{
		songData.trackList.Add(new TrackData(InstrumentId.Guitar));
		UserSongDataStore.storeSongData(songData);

		Application.LoadLevel("Editor");
	}

	private void OnBackButtonClick()
	{
		App.instance.back(comeFromEditorMenu ? 1 : 2);
	}
}