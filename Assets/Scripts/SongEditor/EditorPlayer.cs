using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorPlayer
{
	private float tempo;
	private float elapsedTime;

	private int currentNoteIndex;
	private int currentBarIndex;

	private BarData currentBarData;
	private NoteData currentNoteData;

	private float nextTime;

	private float secPerBeat;

	private MidiPlayer midiPlayer;
	private TrackData trackData;

	public EditorPlayer(MidiPlayer midiPlayer, TrackData trackData)
	{
		this.midiPlayer = midiPlayer;
		this.trackData = trackData;
	}

	public void play()
	{
		elapsedTime = 0;
		nextTime = 0;

		currentNoteIndex = 0;
		currentBarIndex = 0;

		currentBarData = trackData.barList[currentBarIndex];

		tempo = trackData.barList[0].beatsPerMinute;
		secPerBeat = 1.0f / (tempo / 60.0f);

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
			currentBarData = trackData.barList[currentBarIndex];
			currentNoteData = currentBarData.noteList[currentNoteIndex];

			if (!currentNoteData.isRest)
			{
				midiPlayer.noteOn(currentNoteData);
			}

			//Check if it was the last note of the bar
			if (currentNoteIndex == currentBarData.noteList.Count - 1)
			{
				currentNoteIndex = 0;
				currentBarIndex++;
			}
			else
			{
				currentNoteIndex++;
			}

			currentBarData = trackData.barList[currentBarIndex];
			currentNoteData = currentBarData.noteList[currentNoteIndex];

			nextTime = ((currentBarData.index * 16) + currentNoteData.start) * (secPerBeat * 0.25f);
		}
	}
}