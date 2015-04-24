using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarData
{
	private const int numPulses = 4;
	private const int numPositions = 16;

	public EditorNoteData[] notes { get; private set; }

	public BarData()
	{
		EditorNoteData restNote = new EditorNoteData(0, new NoteData("rest", numPulses));

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

		if (position == editorNote.start)
		{

		}

		int noteEnd = editorNote.start + editorNote.noteData.intDuration;
		int newNoteEnd = position + noteData.intDuration;

		for (int i = position; i < newNoteEnd; i++)
		{
			notes[i] = newNote;
		}

		if (noteEnd > newNoteEnd)
		{
			//Create new rest note
			int duration = noteEnd - newNoteEnd;
			EditorNoteData restNote = new EditorNoteData(newNoteEnd, new NoteData("rest", duration));
			
			for (int i = newNoteEnd; i < noteEnd; i++)
			{
				notes[i] = restNote;
			}
			mergeRestNote();
		}
	}

	/// <summary>
	/// Merges any consecutive rest notes.
	/// </summary>
	private void mergeRestNote()
	{

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