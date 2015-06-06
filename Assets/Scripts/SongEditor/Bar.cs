using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar : MonoBehaviour
{
	private const float segmentWidth = 75.0f;
	private const float lineHeight = 9.5f;

	private NotePitch centralNotePitch = NotePitch.B4;

	public TextMesh indexLabel;

	public GameObject barUnitPrefab;
	public GameObject notePrefab;

	public GameObject topLedgerLines;
	public GameObject bottomLedgerLines;

	public EditorBarData barData { get; private set; }

	private BarUnit[] barUnitList;
	private BoxCollider2D collider;

	private List<Note> noteList;
	private Key currentKey;

	public void Init(int index, Key key)
	{
		currentKey = key;

		//Set index label
		indexLabel.text = (index + 1).ToString();

		topLedgerLines.SetActive(false);
		bottomLedgerLines.SetActive(false);

		barData = new EditorBarData(index);
		collider = GetComponent<BoxCollider2D>();

		noteList = new List<Note>();

		barUnitList = new BarUnit[16];
		for (int i = 0; i < 16; i++)
		{
			GameObject barUnitObject = GameObject.Instantiate(barUnitPrefab) as GameObject;

			barUnitObject.transform.parent = transform;
			barUnitObject.transform.localPosition = new Vector3((0.5f + i) * segmentWidth, 0, 0);

			barUnitList[i] = barUnitObject.GetComponent<BarUnit>();
			barUnitList[i].gameObject.SetActive(false);
		}
	}

	public void onEnter(EditorCursor cursor)
	{
		int start = calculateStartIndex(cursor.cachedTransform.position);
		if (start >= 0 && start <= 15)
		{
			showDuration(start, cursor.currentNoteData.duration);
		}

		int noteBarIndex = calculateNoteIndex(cursor.transform.position);

		NotePitch notePitch = centralNotePitch.Add(currentKey, noteBarIndex);
		int noteIndex = notePitch.RetrieveNoteIndex(currentKey);
		int centralNoteIndex = centralNotePitch.RetrieveNoteIndex(currentKey);

		int topLedgerLineIndex = centralNoteIndex + 5;
		int bottomLedgerLineIndex = centralNoteIndex - 5;

		//Check whether we need to show ledger lines or not
		topLedgerLines.SetActive(noteIndex >= topLedgerLineIndex);
		bottomLedgerLines.SetActive(noteIndex <= bottomLedgerLineIndex);

		topLedgerLines.transform.position = topLedgerLines.transform.position.setX(cursor.transform.position.x);
		bottomLedgerLines.transform.position = bottomLedgerLines.transform.position.setX(cursor.transform.position.x);

		cursor.noteHandler.position = cursor.noteHandler.position.setY(noteBarIndex * lineHeight);
	}

	public void onExit()
	{
		hideDuration();
	}

	public void onClick(EditorCursor cursor)
	{
		int start = calculateStartIndex(cursor.cachedTransform.position);
		float posx = (0.5f + start) * segmentWidth;

		int noteIndex = calculateNoteIndex(cursor.noteHandler.position);
		NotePitch notePitch = cursor.currentNoteData.isRest ? NotePitch.Rest : centralNotePitch.Add(currentKey, noteIndex);

		NoteData noteData = new NoteData(notePitch, start, cursor.currentNoteData.duration);

		barData.addNote(noteData, start);
		drawNotes(barData.retrieveNoteDataList());
	}

	private void showDuration(int start, int duration)
	{
		hideDuration();

		int end = start + duration;
		end = Mathf.Clamp(end, 0, barUnitList.Length);

		for (int i = start; i < end; i++)
		{
			barUnitList[i].gameObject.SetActive(true);

			if (barData.notes[i].isRest) barUnitList[i].spriteRenderer.color = new Color(0.0f, 0.0f, 1.0f, 0.25f);
			else barUnitList[i].spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
		}
	}

	private void hideDuration()
	{
		for (int i = 0; i < barUnitList.Length; i++)
		{
			barUnitList[i].gameObject.SetActive(false);
		}
	}

	private int calculateStartIndex(Vector3 position)
	{
		Vector3 localPosition = transform.InverseTransformPoint(position);
		int startIndex = Mathf.FloorToInt((localPosition.x - 0) / segmentWidth);

		return startIndex;
	}

	public void drawNotes(List<NoteData> noteDataList)
	{
		for (int i = 0; i < noteList.Count; i++)
		{
			GameObject.Destroy(noteList[i].gameObject);
		}
		noteList.Clear();

		int start = 0;
		int centralNoteIndex = centralNotePitch.RetrieveNoteIndex(currentKey);

		for (int i = 0; i < noteDataList.Count; i++)
		{
			int noteIndex = noteDataList[i].pitch.RetrieveNoteIndex(currentKey);

			float posx = (0.5f + start) * segmentWidth;
			float posy = noteDataList[i].isRest ? 0.0f : (noteIndex - centralNoteIndex) * lineHeight;

			GameObject noteGameObject = GameObject.Instantiate(notePrefab) as GameObject;
			
			noteGameObject.transform.parent = transform;
			noteGameObject.transform.localPosition = new Vector3(posx, posy, 0);

			NoteData noteData = noteDataList[i];
			Note note = noteGameObject.GetComponent<Note>();

			note.init(currentKey, noteData);

			noteList.Add(note);
			start += noteData.duration;
		}

		//PrintNotes(noteDataList);
	}

	private int calculateNoteIndex(Vector3 position)
	{
		return Mathf.FloorToInt(position.y / lineHeight);
	}

	public void LoadData(List<NoteData> noteList)
	{
		foreach (NoteData noteData in noteList)
		{
			int noteEnd = noteData.start + noteData.duration;
			for (int i = noteData.start; i < noteEnd; i++)
			{
				barData.notes[i] = noteData;
			}
		}

		drawNotes(barData.retrieveNoteDataList());
	}

	public void PrintNotes(List<NoteData> noteList)
	{
		Debug.Log("------");
		foreach (NoteData noteData in noteList)
		{
			Debug.Log("NOTE: " + noteData.duration + " - " + noteData.pitch.StringValue);
		}
		
		for (int i = 0; i < barData.notes.Length; i++)
		{
			Debug.Log("NOTES[" + i + "]: " + barData.notes[i].duration + " - " + barData.notes[i].pitch.StringValue);
		}
		Debug.Log("------");
	}
}