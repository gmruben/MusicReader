using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EditorMenu : UIMenu
{
	private const int maxTempo = 200;

	public SaveSongData_Overlay saveSongData_Overlay;
	public LoadSongData_Overlay loadSongData_Overlay;

	private ChangeInstrumentOverlay changeInstrumentOverlay;

	public UIButton saveButton;
	public UIButton loadButton;
	public UIButton backButton;

	public UIButton changeInstrumentButton;

	public InputField tempoInput;

	public UIButton playButton;
	public UIButton stopButton;

	public UIButton moveLeftButton;
	public UIButton moveRightButton;

	public SymbolButton dottedMinimButton;
	public SymbolButton minimButton;
	public SymbolButton dottedCrotchetButton;
	public SymbolButton crotchetButton;
	public SymbolButton dottedQuaverButton;
	public SymbolButton quaverButton;
	public SymbolButton semiquaverButton;

	public SymbolButton semibreveRestButton;
	public SymbolButton minimRestButton;
	public SymbolButton crotchetRestButton;
	public SymbolButton quaverRestButton;
	public SymbolButton semiquaverRestButton;

	public EditorCursor cursor;
	public Editor editor;

	private int tempoValue;

	void Start()
	{
		Init ();
	}

	public void Init()
	{
		tempoValue = 120;

		saveSongData_Overlay.gameObject.SetActive (false);
		loadSongData_Overlay.gameObject.SetActive (false);

		saveButton.onClick += onSaveButtonClick;
		loadButton.onClick += onLoadButtonClick;
		backButton.onClick += onBackButtonClick;

		changeInstrumentButton.onClick += onChangeInstrumentButtonClick;

		playButton.onClick += onPlayButtonClick;
		stopButton.onClick += onStopButtonClick;

		moveLeftButton.onClick += onMoveLeftButtonClick;
		moveRightButton.onClick += onMoveRightButtonClick;

		tempoInput.text = tempoValue.ToString();
		tempoInput.contentType = InputField.ContentType.IntegerNumber;
		tempoInput.characterLimit = 3;
		tempoInput.onEndEdit.AddListener(onTempoChange);

		createSymbolButtons();
	}

	private void createSymbolButtons()
	{
		dottedMinimButton.setData(new CursorNoteData(NoteId.DottedMinim, false));
		minimButton.setData(new CursorNoteData(NoteId.Minim, false));
		dottedCrotchetButton.setData(new CursorNoteData(NoteId.DottedCrotchet, false));
		crotchetButton.setData(new CursorNoteData(NoteId.Crotchet, false));
		dottedQuaverButton.setData(new CursorNoteData(NoteId.DottedQuaver, false));
		quaverButton.setData(new CursorNoteData(NoteId.Quaver, false));
		semiquaverButton.setData(new CursorNoteData(NoteId.Semiquaver, false));

		semibreveRestButton.setData(new CursorNoteData(NoteId.SemibreveRest, true));
		minimRestButton.setData(new CursorNoteData(NoteId.MinimRest, true));
		crotchetRestButton.setData(new CursorNoteData(NoteId.CrotchetRest, true));
		quaverRestButton.setData(new CursorNoteData(NoteId.QuaverRest, true));
		semiquaverRestButton.setData(new CursorNoteData(NoteId.SemiquaverRest, true));

		dottedMinimButton.onClick += onSymbolButtonClick;
		minimButton.onClick += onSymbolButtonClick;
		dottedCrotchetButton.onClick += onSymbolButtonClick;
		crotchetButton.onClick += onSymbolButtonClick;
		dottedQuaverButton.onClick += onSymbolButtonClick;
		quaverButton.onClick += onSymbolButtonClick;
		semiquaverButton.onClick += onSymbolButtonClick;

		semibreveRestButton.onClick += onSymbolButtonClick;
		minimRestButton.onClick += onSymbolButtonClick;
		crotchetRestButton.onClick += onSymbolButtonClick;
		quaverRestButton.onClick += onSymbolButtonClick;
		semiquaverRestButton.onClick += onSymbolButtonClick;
	}

	private void onSymbolButtonClick(CursorNoteData cursorNoteData)
	{
		cursor.updateNoteData(cursorNoteData);
	}

	private void onSaveButtonClick()
	{
		/*saveSongData_Overlay.okButton.onClick += onSaveSongDataOkButtonClick;
		saveSongData_Overlay.cancelButton.onClick += onSaveSongDataCancelButtonClick;

		saveSongData_Overlay.gameObject.SetActive (true);*/

		SongData songData = new SongData();

		songData.id = "Song1";
		songData.name = "Song 1";

		songData.noteList = editor.retrieveData();

		UserSongDataStore.storeSongData(songData);
	}

	private void onLoadButtonClick()
	{
		/*loadSongData_Overlay.cancelButton.onClick += onLoadSongDataCancelButtonClick;
		
		loadSongData_Overlay.gameObject.SetActive (true);*/

		UserSongDataStore.retrieveSongDataList();
		editor.loadData(UserSongDataStore.retrieveSongData("Song1").noteList);
	}

	private void onChangeInstrumentButtonClick()
	{
		GameObject overlayGameObject = GameObject.Instantiate(MenuManager.instance.changeInstrumentOverlayPrefab) as GameObject;

		changeInstrumentOverlay = overlayGameObject.GetComponent<ChangeInstrumentOverlay>();
		changeInstrumentOverlay.init();
	}

	private void onPlayButtonClick()
	{
		editor.play();
	}

	private void onStopButtonClick()
	{
		editor.pause();
	}

	private void onBackButtonClick()
	{
		Application.LoadLevel("Menu");
	}

	private void onMoveLeftButtonClick()
	{
		editor.move(1);
	}

	private void onMoveRightButtonClick()
	{
		editor.move(-1);
	}

	private void onTempoChange(string value)
	{
		int intValue = int.Parse(value);

		if (intValue > maxTempo)
		{
			intValue = maxTempo;
			tempoInput.text = intValue.ToString();
		}

		tempoValue = intValue;
	}

	private void onSaveSongDataOkButtonClick()
	{
		saveSongData_Overlay.okButton.onClick -= onSaveSongDataOkButtonClick;
		saveSongData_Overlay.cancelButton.onClick -= onSaveSongDataCancelButtonClick;

		//UserSongDataStore.storeSongData(songData);
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