using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Editor : MonoBehaviour
{
	private const int barSize = 1200;
	private const float speed = 2.5f;
	private const int numBars = 10;

	public GameObject barPrefab;

	public MidiPlayer midiPlayer;

	private List<Bar> barList;

	private int currentIndex;
	private Transform cachedTransform;

	private bool inPlay;

	private bool inMove;
	private float targetX;

	private Tween tween;

	private EditorPlayer editorPlayer;

	void Start()
	{
		init ();
	}

	void Update()
	{
		if (inMove)
		{
			if (!tween.hasEnded)
			{
				float posx = tween.update(Time.deltaTime);
				cachedTransform.position = cachedTransform.position.setX(posx);
			}
		}

		if (inPlay)
		{
			float posx = cachedTransform.position.x - (500 * Time.deltaTime);
			cachedTransform.position = cachedTransform.position.setX(posx);

			editorPlayer.update(Time.deltaTime);
		}
	}

	public void init()
	{
		editorPlayer = new EditorPlayer(midiPlayer);

		currentIndex = 0;
		cachedTransform = transform;

		inPlay = false;

		inMove = false;
		targetX = 0;

		barList = new List<Bar>();
		for (int i = 0; i < numBars; i++)
		{
			GameObject barGameObject = GameObject.Instantiate(barPrefab) as GameObject;
			Bar bar = barGameObject.GetComponent<Bar>();

			bar.transform.parent = cachedTransform;
			bar.transform.localPosition = new Vector3(i * barSize, 0, 0);

			barList.Add(bar);
		}
	}

	public void move(int direction)
	{
		if ((direction < 0 && cachedTransform.position.x > -numBars * barSize) || (direction > 0 && cachedTransform.position.x < 0))
		{
			inMove = true;
			targetX = cachedTransform.position.x + (barSize * direction * 0.5f);

			tween = new Tween(cachedTransform.position.x, targetX, speed);
		}
	}

	public void play()
	{
		inPlay = true;
		cachedTransform.position = cachedTransform.position.setX(0);

		editorPlayer.play(retrieveData());
	}

	public void pause()
	{
		inPlay = false;
		cachedTransform.position = cachedTransform.position.setX(0);
	}

	public List<NoteData> retrieveData()
	{
		List<NoteData> noteDataList = new List<NoteData>();
		foreach(Bar bar in barList)
		{
			noteDataList.AddRange(bar.barData.retrieveNoteDataList());
		}
		return noteDataList;
	}

	public void loadData(List<NoteData> noteList)
	{
		for (int i = 0; i < barList.Count; i++)
		{
			barList[i].drawNotes(noteList);
		}
	}
}