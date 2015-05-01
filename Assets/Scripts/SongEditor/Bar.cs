using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar : MonoBehaviour
{
	private const int offsetx = 75;
	private const int segmentWidth = 50;

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
			barUnitObject.transform.localPosition = new Vector3(offsetx + i * segmentWidth, 0, 0);

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
	}

	public void onExit()
	{
		hideDuration();
	}

	public void onClick(EditorCursor cursor)
	{
		int start = calculateStartIndex(cursor.cachedTransform.position);
		float posx = offsetx + start * segmentWidth;

		/*GameObject note = GameObject.Instantiate(notePrefab) as GameObject;

		note.transform.parent = transform;
		note.transform.localPosition = new Vector3(posx, 0, 0);*/

		NoteData noteData = new NoteData("b", cursor.noteDuration);
		//note.GetComponent<Note>().init(noteData);

		barData.addNote(noteData, start);
		drawNotes();
	}

	private void showDuration(int start, int duration)
	{
		hideDuration();

		int end = start + duration;
		end = Mathf.Clamp(end, 0, barUnitList.Length);

		for (int i = start; i < end; i++)
		{
			barUnitList[i].gameObject.SetActive(true);

			if (barData.notes[i].noteData.isRest) barUnitList[i].spriteRenderer.color = Color.blue;
			else barUnitList[i].spriteRenderer.color = Color.red;
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
		int startIndex = Mathf.FloorToInt((localPosition.x - segmentWidth) / segmentWidth);

		return startIndex;
	}

	private void drawNotes()
	{
		for (int i = 0; i < noteList.Count; i++)
		{
			GameObject.Destroy(noteList[i].gameObject);
		}
		noteList.Clear();

		int start = 0;

		List<NoteData> noteDataList = barData.retrieveNoteDataList();
		for (int i = 0; i < noteDataList.Count; i++)
		{
			float posx = offsetx + start * segmentWidth;

			GameObject noteGameObject = GameObject.Instantiate(notePrefab) as GameObject;
			
			noteGameObject.transform.parent = transform;
			noteGameObject.transform.localPosition = new Vector3(posx, 0, 0);

			NoteData noteData = noteDataList[i];
			Note note = noteGameObject.GetComponent<Note>();

			Debug.Log("NOTE: " + noteData.intDuration + "(" + noteData.isRest + ")");
			note.init(noteData);

			noteList.Add(note);
			start += noteData.intDuration;
		}
		Debug.Log("-----------------");
	}
}