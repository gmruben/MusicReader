﻿using UnityEngine;
using System.Collections;

public class EditorMenu : UIMenu
{
	public SaveSongData_Overlay saveSongData_Overlay;
	public LoadSongData_Overlay loadSongData_Overlay;

	public UIButton saveButton;
	public UIButton loadButton;

	private SongData songData;

	void Start()
	{
		init ();
	}

	public void init()
	{
		songData = new SongData();

		saveSongData_Overlay.gameObject.SetActive (false);
		loadSongData_Overlay.gameObject.SetActive (false);

		saveButton.onClick += onSaveButtonClick;
		loadButton.onClick += onLoadButtonClick;
	}

	private void onSaveButtonClick()
	{
		saveSongData_Overlay.okButton.onClick += onSaveSongDataOkButtonClick;
		saveSongData_Overlay.cancelButton.onClick += onSaveSongDataCancelButtonClick;

		saveSongData_Overlay.gameObject.SetActive (true);
	}

	private void onLoadButtonClick()
	{
		loadSongData_Overlay.cancelButton.onClick += onLoadSongDataCancelButtonClick;
		
		loadSongData_Overlay.gameObject.SetActive (true);
	}

	private void onSaveSongDataOkButtonClick()
	{
		saveSongData_Overlay.okButton.onClick -= onSaveSongDataOkButtonClick;
		saveSongData_Overlay.cancelButton.onClick -= onSaveSongDataCancelButtonClick;

		UserSongDataStore.storeSongData(songData);
	}

	private void onSaveSongDataCancelButtonClick()
	{
		saveSongData_Overlay.okButton.onClick -= onSaveSongDataOkButtonClick;
		saveSongData_Overlay.cancelButton.onClick -= onSaveSongDataCancelButtonClick;

		saveSongData_Overlay.gameObject.SetActive (false);

	}

	private void onLoadSongDataCancelButtonClick()
	{
		loadSongData_Overlay.cancelButton.onClick -= onLoadSongDataCancelButtonClick;
		
		loadSongData_Overlay.gameObject.SetActive (false);
		
	}
}