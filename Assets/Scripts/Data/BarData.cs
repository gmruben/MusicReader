using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarData
{
	private const int numPulses = 4;
	private const int numPositions = 16;

	public NoteData[] notes { get; private set; }

	public BarData()
	{
		NoteData restNote = new NoteData(NotePitch.Rest, 0, numPulses);

		notes = new NoteData[numPositions];
		for (int i = 0; i < numPositions; i++)
		{
			notes[i] = restNote;
		}
	}

	public void addNote(NoteData newNoteData, int position)
	{
		NoteData prevNote = notes[position];

		int prevNoteEnd = prevNote.start + prevNote.duration - 1;
		int newNoteEnd = position + newNoteData.duration - 1;

		//Add the new note
		for (int i = position; i <= newNoteEnd; i++)
		{
			notes[i] = newNoteData;
		}

		//If the new note overlaps with the previous note
		if (position >= prevNote.start && position <= prevNoteEnd)
		{
			//Create the previous note
			int duration = (position - prevNote.start);
			NoteData note = new NoteData(prevNote.pitch, prevNote.start, duration);

			int noteEnd = note.start + note.duration - 1;
			for (int i = note.start; i <= noteEnd; i++)
			{
				notes[i] = note;
			}
		}

		//If the previous note finishes after the new note, create a rest for the remaining positions
		if (prevNoteEnd > newNoteEnd)
		{
			//Create new rest note
			int restDuration = (prevNoteEnd - newNoteEnd);
			NoteData restEditorNote = new NoteData(NotePitch.Rest, newNoteEnd, restDuration);

			for (int i = newNoteEnd; i <= prevNoteEnd; i++)
			{
				notes[i] = restEditorNote;
			}
		}

		//Merge the rest notes
		mergeRestNotes();
	}

	/// <summary>
	/// Merges all the contiguous rest notes
	/// </summary>
	private void mergeRestNotes()
	{
		NoteData editorLastNote = notes[0];
		for (int i = 0; i < numPositions; i++)
		{
			NoteData editorCurrentNote = notes[i];
			if (editorLastNote != editorCurrentNote)
			{
				//If they are both rest, merge them
				if (editorLastNote.isRest && editorCurrentNote.isRest)
				{
					int start = editorLastNote.start;
					int duration = editorLastNote.duration + editorCurrentNote.duration;
					int end = start + duration - 1;

					NoteData newNote = new NoteData(NotePitch.Rest, start, duration);
					for (int j = start; j <= end ; j++)
					{
						notes[j] = newNote;
					}
					editorCurrentNote = newNote;
				}
				editorLastNote = editorCurrentNote;
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
			NoteData editorNoteData = notes[index];
			noteDataList.Add(editorNoteData);

			index += editorNoteData.duration;
			iterations++;
		}
		while (index < numPositions && iterations < 20);

		return noteDataList;
	}
}

/*public class EditorNoteData
{
	public int start;
	public NoteData noteData;

	public EditorNoteData(int start, NoteData noteData)
	{
		this.start = start;
		this.noteData = noteData;
	}
}*/