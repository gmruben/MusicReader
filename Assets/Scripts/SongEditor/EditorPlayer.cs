using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorPlayer
{
	private float tempo = 120.0f;
	private float elapsedTime;
	private List<NoteData> noteDataList;

	private NoteData currentNoteData;
	private float nextTime;

	private float secPerBeat;

	private MidiPlayer midiPlayer;

	public EditorPlayer(MidiPlayer midiPlayer)
	{
		this.midiPlayer = midiPlayer;
	}

	public void play(List<NoteData> noteDataList)
	{
		elapsedTime = 0;
		nextTime = 0;

		secPerBeat = 1.0f / (tempo / 60.0f);

		this.noteDataList = noteDataList;

		checkNextNote();
	}

	public void update(float time)
	{
		elapsedTime += time;
		checkNextNote();
	}

	private void checkNextNote()
	{
		if (elapsedTime >= nextTime)
		{
			currentNoteData = noteDataList[0];
			noteDataList.RemoveAt(0);

			Debug.Log("PLAY NOTE: " + currentNoteData.intDuration);

			midiPlayer.noteOff();
			if (!currentNoteData.isRest)
			{
				midiPlayer.noteOn();
			}
			nextTime += currentNoteData.duration * secPerBeat;
		}
	}
}