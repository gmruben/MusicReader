using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarData
{
	private const float barToNote = 0.25f;

	private const int numPulses = 4;
	private const int numPositions = 16;

	public EditorNoteData[] notes { get; private set; }

	public BarData()
	{
		EditorNoteData restNote = new EditorNoteData(0, new NoteData(NotePitch.Rest, 0, numPulses));

		notes = new EditorNoteData[numPositions];
		for (int i = 0; i < numPositions; i++)
		{
			notes[i] = restNote;
		}
	}

	public void addNote(NoteData noteData, int position)
	{
		EditorNoteData editorNote = notes[position];
		EditorNoteData newNote = new EditorNoteData(position, noteData);

		int noteEnd = editorNote.start + editorNote.noteData.intDuration;
		int newNoteEnd = position + noteData.intDuration;

		//Add the new note
		for (int i = position; i < newNoteEnd; i++)
		{
			notes[i] = newNote;
		}

		//If the new note starts at the same position where the old note starts
		if (position > editorNote.start)
		{
			//Create the previous note
			float prevDuration = (position - editorNote.start) * barToNote;
			EditorNoteData prevNote = new EditorNoteData(editorNote.start, new NoteData(editorNote.noteData.pitch, editorNote.start, prevDuration));

			int prevNoteEnd = prevNote.start + prevNote.noteData.intDuration;
			for (int i = prevNote.start; i < prevNoteEnd; i++)
			{
				notes[i] = prevNote;
			}
		}

		if (noteEnd > newNoteEnd)
		{
			//Create new rest note
			float duration = (noteEnd - newNoteEnd) * barToNote;
			//Calculate rest note list (there are not dotted rest notes, so we have to break those down into different notes)
			//List<EditorNoteData> restNoteList = calculateRestNoteList(Mathf.FloorToInt(duration * 4), newNoteEnd);

			EditorNoteData restNote = new EditorNoteData(newNoteEnd, new NoteData(NotePitch.Rest, newNoteEnd, duration));
			for (int i = newNoteEnd; i < noteEnd; i++)
			{
				notes[i] = restNote;
			}
		}
	}

	public List<NoteData> retrieveNoteDataList()
	{
		int index = 0;
		List<NoteData> noteDataList = new List<NoteData>();

		int iterations = 0;
		do
		{
			EditorNoteData editorNoteData = notes[index];
			noteDataList.Add(editorNoteData.noteData);

			index += editorNoteData.noteData.intDuration;
			iterations++;
		}
		while (index < numPositions && iterations < 20);

		return noteDataList;
	}

	/// <summary>
	/// Since there are not dotted rest notes, this function breaks a rest note into smaller correct rest notes.
	/// </summary>
	/// <returns>The rest note list.</returns>
	private List<EditorNoteData> calculateRestNoteList(int duration, int start)
	{
		List<EditorNoteData> restNoteList = new List<EditorNoteData>();
		return restNoteList;
	}
}

public class EditorNoteData
{
	public int start;
	public NoteData noteData;

	public EditorNoteData(int start, NoteData noteData)
	{
		this.start = start;
		this.noteData = noteData;
	}
}