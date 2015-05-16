using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EditorCursor : MonoBehaviour
{
	public Transform noteHandler;
	public SpriteRenderer noteSprite;

	public Transform cachedTransform { get; private set; }

	private Bar currentBar;
	public CursorNoteData currentNoteData { get; private set; }

	void Start()
	{
		cachedTransform = transform;
		noteSprite = GetComponentInChildren<SpriteRenderer>();
	}

	void Update()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			updateCursor();
		}
	}

	public void updateNoteData(CursorNoteData noteData)
	{
		currentNoteData = noteData;

		Debug.Log("NOTE ID: " + noteData.noteId);
		noteSprite.sprite = Resources.Load<Sprite>("Sprites/Symbols/" + noteData.noteId);
	}

	private void updateCursor()
	{
		Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).XY();
		transform.position = worldPosition.V3();
		
		bool hasFound = false;
		Collider2D[] colliderList = Physics2D.OverlapPointAll(worldPosition);
		for (int i = 0; i < colliderList.Length; i++)
		{
			currentBar = colliderList[i].GetComponent<Bar>();
			if (currentBar != null)
			{
				noteHandler.gameObject.SetActive(true);

				hasFound = true;
				currentBar.onEnter(this);
				
				break;
			}
		}
		
		if (!hasFound)
		{
			noteHandler.localPosition = Vector3.zero;
			noteHandler.gameObject.SetActive(false);

			if (currentBar != null) currentBar.onExit();
		}
		else
		{
			if (Input.GetMouseButtonDown(0)) currentBar.onClick(this);
		}
	}
}

/// <summary>
/// Data for the cursor's current note
/// </summary>
public struct CursorNoteData
{
	public bool isRest;
	public NoteId noteId;

	public CursorNoteData(NoteId noteId, bool isRest)
	{
		this.isRest = isRest;
		this.noteId = noteId;
	}

	public int duration
	{
		get { return NoteUtil.retrieveDurationById(noteId); }
	}
}