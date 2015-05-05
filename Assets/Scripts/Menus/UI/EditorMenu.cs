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
	public SymbolButton dottedSemiquaverButton;
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
		init ();
	}

	public void init()
	{
		tempoValue = 120;

		saveSongData_Overlay.gameObject.SetActive (false);
		loadSongData_Overlay.gameObject.SetActive (false);

		saveButton.onClick += onSaveButtonClick;
		loadButton.onClick += onLoadButtonClick;

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
		dottedMinimButton.setData(SymbolId.DottedMinim);
		minimButton.setData(SymbolId.Minim);
		dottedCrotchetButton.setData(SymbolId.DottedCrotchet);
		crotchetButton.setData(SymbolId.Crotchet);
		dottedQuaverButton.setData(SymbolId.DottedQuaver);
		quaverButton.setData(SymbolId.Quaver);
		dottedSemiquaverButton.setData(SymbolId.DottedSemiquaver);
		semiquaverButton.setData(SymbolId.Semiquaver);

		semibreveRestButton.setData(SymbolId.Semibreve);
		minimRestButton.setData(SymbolId.Minim);
		crotchetRestButton.setData(SymbolId.Crotchet);
		quaverRestButton.setData(SymbolId.Quaver);
		semiquaverRestButton.setData(SymbolId.Semiquaver);

		dottedMinimButton.onClick += onSymbolButtonClick;
		minimButton.onClick += onSymbolButtonClick;
		dottedCrotchetButton.onClick += onSymbolButtonClick;
		crotchetButton.onClick += onSymbolButtonClick;
		dottedQuaverButton.onClick += onSymbolButtonClick;
		quaverButton.onClick += onSymbolButtonClick;
		dottedSemiquaverButton.onClick += onSymbolButtonClick;
		semiquaverButton.onClick += onSymbolButtonClick;

		semibreveRestButton.onClick += onSymbolButtonClick;
		minimRestButton.onClick += onSymbolButtonClick;
		crotchetRestButton.onClick += onSymbolButtonClick;
		quaverRestButton.onClick += onSymbolButtonClick;
		semiquaverRestButton.onClick += onSymbolButtonClick;
	}

	private void onSymbolButtonClick(SymbolId symbolId)
	{
		cursor.changeSymbol(symbolId);
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