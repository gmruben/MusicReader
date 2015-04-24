using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour
{
	private const int offsetx = 75;
	private const int segmentWidth = 50;

	public GameObject barUnitPrefab;
	public GameObject notePrefab;

	private BarUnit[] barUnitList;

	private BarData barData;
	private BoxCollider2D collider;

	void Start()
	{
		init ();
	}

	public void init()
	{
		barData = new BarData();
		collider = GetComponent<BoxCollider2D>();

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

		GameObject note = GameObject.Instantiate(notePrefab) as GameObject;

		note.transform.parent = transform;
		note.transform.localPosition = new Vector3(posx, 0, 0);

		barData.addNote(new NoteData("b", cursor.noteDuration), start);
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
}