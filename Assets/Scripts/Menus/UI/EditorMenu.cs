using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EditorMenu : UIMenu
{
	private const int maxTempo = 200;

	public UIButton newBarButton;
	public UIButton saveButton;
	public UIButton backButton;

	public InputField tempoInput;

	public Text songTitleLabel;

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

	private SongData songData;
	private TrackData trackData;

	void Start()
	{
		if (GameConfig.songData == null)
		{
			UserSongDataStore.retrieveSongDataList();

			GameConfig.songData = (SongData) UserSongDataStore.retrieveFirstSongData();
			GameConfig.trackData = GameConfig.songData.trackList[0];

			Debug.Log(GameConfig.songData);
		}

		Init ();
	}

	public void Init()
	{
		songData = GameConfig.songData;
		trackData = GameConfig.trackData;

		songTitleLabel.text = songData.name + " - " + trackData.name;

		tempoValue = 120;

		newBarButton.onClick += onNewBarButtonClick;

		saveButton.onClick += onSaveButtonClick;
		backButton.onClick += onBackButtonClick;

		playButton.onClick += onPlayButtonClick;
		stopButton.onClick += onStopButtonClick;

		moveLeftButton.onClick += onMoveLeftButtonClick;
		moveRightButton.onClick += onMoveRightButtonClick;

		tempoInput.text = tempoValue.ToString();
		tempoInput.contentType = InputField.ContentType.IntegerNumber;
		tempoInput.characterLimit = 3;
		tempoInput.onEndEdit.AddListener(onTempoChange);

		createSymbolButtons();

		//Load song data
		editor.Init(trackData);
		editor.LoadData();
	}

	private void createSymbolButtons()
	{
		//HACK: Maybe do the same as with note pitch?
		CursorNoteData crotchetCursorNoteData = new CursorNoteData(NoteId.Crotchet, false);
		cursor.updateNoteData(crotchetCursorNoteData);

		dottedMinimButton.setData(new CursorNoteData(NoteId.DottedMinim, false));
		minimButton.setData(new CursorNoteData(NoteId.Minim, false));
		dottedCrotchetButton.setData(new CursorNoteData(NoteId.DottedCrotchet, false));
		crotchetButton.setData(crotchetCursorNoteData);
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

	private void onNewBarButtonClick()
	{
		editor.AddNewBar();
	}

	private void onSaveButtonClick()
	{
		trackData.barList = editor.retrieveBarDataList();

		UserSongDataStore.storeSongData(songData);
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
}