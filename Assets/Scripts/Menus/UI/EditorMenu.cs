using UnityEngine;
using System.Collections;

public class EditorMenu : UIMenu
{
	public SaveSongData_Overlay saveSongData_Overlay;
	public LoadSongData_Overlay loadSongData_Overlay;

	public EditorBar editorBar;

	public UIButton saveButton;
	public UIButton loadButton;

	public UIButton dottedMinimButton;
	public UIButton minimButton;
	public UIButton dottedCrotchetButton;
	public UIButton crotchedButton;
	public UIButton dottedQuaverButton;
	public UIButton quaverButton;
	public UIButton dottedSemiquaverButton;
	public UIButton semiquaverButton;

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

		editorBar.init ();

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