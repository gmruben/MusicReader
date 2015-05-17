using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Editor : MonoBehaviour
{
	private const int barSize = 1200;
	private const float speed = 2.5f;

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

	private TrackData trackData;

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

	public void Init(TrackData trackData)
	{
		this.trackData = trackData;
		editorPlayer = new EditorPlayer(midiPlayer);

		currentIndex = 0;
		cachedTransform = transform;

		inPlay = false;

		inMove = false;
		targetX = 0;

		barList = new List<Bar>();

		//If the song doesn't have any bars yet, add one
		if (trackData.barList.Count == 0)
		{
			AddNewBar();
		}
		else
		{
			//Create the bars
			for (int i = 0; i < trackData.barList.Count; i++)
			{
				InstantiateBar(i);
			}
		}
	}

	public void move(int direction)
	{
		if ((direction < 0 && cachedTransform.position.x > -trackData.barList.Count * barSize) || (direction > 0 && cachedTransform.position.x < 0))
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

		//editorPlayer.play(retrieveData());
	}

	public void pause()
	{
		inPlay = false;
		cachedTransform.position = cachedTransform.position.setX(0);
	}

	public List<BarData> retrieveBarDataList()
	{
		List<BarData> barDataList = new List<BarData>();
		foreach(Bar bar in barList)
		{
			BarData barData = new BarData(bar.barData.index, 120);
			barData.noteList = bar.barData.retrieveNoteDataList();

			barDataList.Add(barData);
		}
		return barDataList;
	}

	public void LoadData()
	{
		for (int i = 0; i < trackData.barList.Count; i++)
		{
			Bar bar = barList[i];
			BarData barData = trackData.barList[i];

			bar.LoadData(barData.noteList);
		}
	}

	public void AddNewBar()
	{
		int newBarIndex = trackData.barList.Count;

		BarData barData = new BarData(newBarIndex, 120);
		trackData.barList.Add(barData);

		InstantiateBar(trackData.barList.Count - 1);
	}

	private void InstantiateBar(int index)
	{
		GameObject barGameObject = GameObject.Instantiate(barPrefab) as GameObject;
		Bar bar = barGameObject.GetComponent<Bar>();
		
		bar.transform.parent = cachedTransform;
		bar.transform.localPosition = new Vector3(index * barSize, 0, 0);
		
		bar.Init(index);
		barList.Add(bar);
	}
}