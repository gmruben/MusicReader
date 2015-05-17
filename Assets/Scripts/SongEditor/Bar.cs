using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar : MonoBehaviour
{
	private const float segmentWidth = 75.0f;
	private const float lineHeight = 9.5f;

	private NotePitch centralNotePitch = NotePitch.B4;

	public GameObject barUnitPrefab;
	public GameObject notePrefab;

	public GameObject topLedgerLines;
	public GameObject bottomLedgerLines;

	public EditorBarData barData { get; private set; }

	private BarUnit[] barUnitList;
	private BoxCollider2D collider;

	private List<Note> noteList;

	public void init()
	{
		topLedgerLines.SetActive(false);
		bottomLedgerLines.SetActive(false);

		barData = new EditorBarData();
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

		int noteIndex = calculateNoteIndex(cursor.transform.position);
		NotePitch notePitch = centralNotePitch.Add(noteIndex);

		//Check whether we need to show ledger lines or not
		topLedgerLines.SetActive(notePitch.IntValue >= NotePitch.A5.IntValue);
		bottomLedgerLines.SetActive(notePitch.IntValue <= NotePitch.C3.IntValue);

		topLedgerLines.transform.position = topLedgerLines.transform.position.setX(cursor.transform.position.x);
		bottomLedgerLines.transform.position = bottomLedgerLines.transform.position.setX(cursor.transform.position.x);

		cursor.noteHandler.position = cursor.noteHandler.position.setY(noteIndex * lineHeight);
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
		NotePitch notePitch = cursor.currentNoteData.isRest ? NotePitch.Rest : centralNotePitch.Add(noteIndex);

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
		for (int i = 0; i < noteDataList.Count; i++)
		{
			float posx = (0.5f + start) * segmentWidth;
			float posy = noteDataList[i].isRest ? 0.0f : (noteDataList[i].pitch.IntValue - centralNotePitch.IntValue) * lineHeight;

			GameObject noteGameObject = GameObject.Instantiate(notePrefab) as GameObject;
			
			noteGameObject.transform.parent = transform;
			noteGameObject.transform.localPosition = new Vector3(posx, posy, 0);

			NoteData noteData = noteDataList[i];
			Note note = noteGameObject.GetComponent<Note>();

			note.init(noteData);

			noteList.Add(note);
			start += noteData.duration;
		}
	}

	private int calculateNoteIndex(Vector3 position)
	{
		return Mathf.FloorToInt(position.y / lineHeight);
	}
}