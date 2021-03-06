﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameBar : MonoBehaviour
{
	private const float segmentWidth = 75.0f;
	private const float offset = segmentWidth * 0.5f;
	private const float lineHeight = 9.5f;

	public event Action onHitNote;
	public event Action onMissNote;

	private NotePitch centralNotePitch = NotePitch.B4;

	private float speed;
	private Transform cachedTransform;

	private float currentTime;
	private GameNote currentNote;
	private List<GameNote> noteList;

	private float startTime;

	private int beatsPerMinute = 60;
	private float beatsPerSecond;
	private float secondsPerBeat;
	private float secondsPerSemiquaver;

	//Max time offset to hit a note
	private const float timeOffset = 0.10f;
	private const int initialRestBeatCount = 1;

	//Whether the player has hit the current note
	private bool hasHitNote = false;

	void Update()
	{
		cachedTransform.position += Vector3.left * speed * Time.deltaTime;

		//Return if there is no current note
		if (currentNote == null) return;

		float diff = Time.time - currentTime;
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (Mathf.Abs(diff) < timeOffset)
			{
				currentNote.Hit();
				currentNote.OnEnd += onNoteEnd;

				hasHitNote = true;
				if (onHitNote != null) onHitNote();
			}
		}
		else if (Input.GetKey(KeyCode.A))
		{
			if (hasHitNote)
			{
				float currentDuration = diff / secondsPerSemiquaver;
				currentNote.UpdateHold(currentDuration);
			}
		}
		else if (Input.GetKeyUp(KeyCode.A))
		{
			if (hasHitNote)
			{
				checkNextNote();
			}
		}
		else
		{
			if (diff > timeOffset)
			{
				currentNote.Miss();
				checkNextNote();

				if (onMissNote != null) onMissNote();
			}
		}
	}

	private void onNoteEnd()
	{
		checkNextNote();
	}

	/// <summary>
	/// Calculates what is the next note
	/// </summary>
	private void checkNextNote()
	{
		hasHitNote = false;
		if (noteList.Count > 0)
		{
			currentNote = noteList[0];
			currentTime = startTime + currentNote.noteData.start * secondsPerSemiquaver;

			noteList.RemoveAt(0);
		}
	}
	
	public void init(int index, Key key, List<NoteData> noteDataList)
	{
		beatsPerSecond = (float) (beatsPerMinute / 60.0f);
		secondsPerBeat = 1.0f / beatsPerSecond;
		secondsPerSemiquaver = secondsPerBeat / 4.0f;

		speed = segmentWidth * 4.0f / secondsPerBeat;
		cachedTransform = transform;

		noteList = new List<GameNote>();

		for (int i = 0; i < noteDataList.Count; i++)
		{
			NoteData noteData = noteDataList[i];
			if (!noteData.isRest)
			{
				int noteIndex = noteData.pitch.RetrieveNoteIndex(key);
				int centralNoteIndex = centralNotePitch.RetrieveNoteIndex(key);

				float posx = offset + (noteData.start * segmentWidth);
				float posy = noteData.isRest ? 0.0f : (noteIndex - centralNoteIndex) * lineHeight;

				GameObject noteObject = GameObject.Instantiate(EntityManager.instance.gameNotePrefab) as GameObject;
				noteObject.transform.SetParent(transform);
				noteObject.transform.localPosition = new Vector3(posx, posy, 0);

				GameNote gameNote = noteObject.GetComponent<GameNote>();
				gameNote.Init(noteDataList[i]);
				noteList.Add(gameNote);
			}
		}

		//Where the actual bar starts (there is a 1 beat rest at the beginning)
		startTime = (initialRestBeatCount + (index * 16)) * secondsPerBeat;

		checkNextNote();
	}
}