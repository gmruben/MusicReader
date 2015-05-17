using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EditSongMenu : UIMenu
{
	public Text title;
	public EditorTrackList trackList;

	public UIButton newTrackButton;
	public UIButton backButton;

	private SongData songData;
	private bool comeFromEditorMenu;

	private CreateNewTrackOverlay createNewTrackOverlay;

	public void Init(SongData songData, bool comeFromEditorMenu)
	{
		this.songData = songData;
		this.comeFromEditorMenu = comeFromEditorMenu;

		title.text = "EDIT SONG - " + songData.name;

		trackList.Init(songData);

		newTrackButton.onClick += OnNewTrackButtonClick;
		backButton.onClick += OnBackButtonClick;
	}

	public override void setEnabled(bool isEnabled)
	{
		newTrackButton.gameObject.SetActive(isEnabled);
		backButton.gameObject.SetActive(isEnabled);

		trackList.gameObject.SetActive(isEnabled);
	}

	private void OnNewTrackButtonClick()
	{
		setEnabled(false);

		createNewTrackOverlay = MenuManager.instance.instantiateCreateNewTrackOverlay();
		createNewTrackOverlay.Init();

		createNewTrackOverlay.OnCreate += OnCreateNewTrack;
		createNewTrackOverlay.OnCancel += OnCancelNewTrack;
	}

	private void OnCreateNewTrack(TrackData trackData)
	{
		songData.trackList.Add(trackData);
		UserSongDataStore.storeSongData(songData);

		setEnabled(true);
		GameObject.Destroy(createNewTrackOverlay.gameObject);

		//Update the track list with the new data
		trackList.UpdateData(songData);
	}

	private void OnCancelNewTrack()
	{
		setEnabled(true);
		GameObject.Destroy(createNewTrackOverlay.gameObject);
	}

	private void OnBackButtonClick()
	{
		App.instance.back(comeFromEditorMenu ? 1 : 2);
	}
}