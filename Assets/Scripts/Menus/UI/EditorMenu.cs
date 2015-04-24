using UnityEngine;
using System.Collections;

public class EditorMenu : UIMenu
{
	public SaveSongData_Overlay saveSongData_Overlay;
	public LoadSongData_Overlay loadSongData_Overlay;

	public UIButton saveButton;
	public UIButton loadButton;

	public SymbolButton dottedMinimButton;
	public SymbolButton minimButton;
	public SymbolButton dottedCrotchetButton;
	public SymbolButton crotchedButton;
	public SymbolButton dottedQuaverButton;
	public SymbolButton quaverButton;
	public SymbolButton dottedSemiquaverButton;
	public SymbolButton semiquaverButton;

	public EditorCursor cursor;

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

		createSymbolButtons();
	}

	private void createSymbolButtons()
	{
		dottedMinimButton.setData(SymbolId.DottedMinim);
		minimButton.setData(SymbolId.Minim);
		dottedCrotchetButton.setData(SymbolId.DottedCrotchet);
		crotchedButton.setData(SymbolId.Crotchet);
		dottedQuaverButton.setData(SymbolId.DottedQuaver);
		quaverButton.setData(SymbolId.Quaver);
		dottedSemiquaverButton.setData(SymbolId.DottedSemiquaver);
		semiquaverButton.setData(SymbolId.Semiquaver);
		
		dottedMinimButton.onClick += onSymbolButtonClick;
		minimButton.onClick += onSymbolButtonClick;
		dottedCrotchetButton.onClick += onSymbolButtonClick;
		crotchedButton.onClick += onSymbolButtonClick;
		dottedQuaverButton.onClick += onSymbolButtonClick;
		quaverButton.onClick += onSymbolButtonClick;
		dottedSemiquaverButton.onClick += onSymbolButtonClick;
		semiquaverButton.onClick += onSymbolButtonClick;
	}

	private void onSymbolButtonClick(SymbolId symbolId)
	{
		cursor.changeSymbol(symbolId);
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