using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar : MonoBehaviour
{
	private const float segmentWidth = 75.0f;
	private const float lineHeight = 9.5f;

	private NotePitch centralNotePitch = NotePitch.B;

	public GameObject barUnitPrefab;
	public GameObject notePrefab;

	public BarData barData { get; private set; }

	private BarUnit[] barUnitList;
	private BoxCollider2D collider;

	private List<Note> noteList;

	void Start()
	{
		init ();
	}

	public void init()
	{
		barData = new BarData();
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
		int duration = Mathf.FloorToInt(cursor.noteDuration * 4);

		if (start >= 0 && start <= 15)
		{
			showDuration(start, duration);
		}

		float posy = calculateNoteIndex(cursor.transform.position) * lineHeight;
		cursor.noteHandler.position = cursor.noteHandler.position.setY(posy);
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
		NotePitch notePitch = centralNotePitch.Add(noteIndex);

		NoteData noteData = new NoteData(notePitch, start, cursor.noteDuration);

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

			if (barData.notes[i].noteData.isRest) barUnitList[i].spriteRenderer.color = new Color(0.0f, 0.0f, 1.0f, 0.25f);
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
			start += noteData.intDuration;
		}
	}

	private int calculateNoteIndex(Vector3 position)
	{
		return Mathf.FloorToInt(position.y / lineHeight);
	}
}