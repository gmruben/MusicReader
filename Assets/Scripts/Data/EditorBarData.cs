using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorBarData
{
	private const int numPositions = 16;

	public int index { get; private set; }
	public NoteData[] notes { get; private set; }

	public EditorBarData(int index)
	{
		this.index = index;
		NoteData restNote = new NoteData(NotePitch.Rest, 0, numPositions);

		notes = new NoteData[numPositions];
		for (int i = 0; i < numPositions; i++)
		{
			notes[i] = restNote;
		}
	}

	public void addNote(NoteData newNoteData, int newPosition)
	{
		NoteData prevNote = notes[newPosition];

		int prevNoteEnd = prevNote.start + prevNote.duration - 1;
		int newNoteEnd = newPosition + newNoteData.duration - 1;

		//Add the new note
		for (int i = newPosition; i <= newNoteEnd; i++)
		{
			notes[i] = newNoteData;
		}

		//If the new note overlaps with the previous note
		if (newPosition > prevNote.start && newPosition <= prevNoteEnd)
		{
			//Create the previous note
			int noteDuration = (newPosition - prevNote.start);
			int noteStart = prevNote.start;
			int noteEnd = noteStart + noteDuration - 1;

			NoteData note = new NoteData(prevNote.pitch, noteStart, noteDuration);
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
			int restStart = newNoteEnd + 1;
			int restEnd = restStart + restDuration - 1;

			NoteData restEditorNote = new NoteData(NotePitch.Rest, restStart, restDuration);
			for (int i = restStart; i <= restEnd; i++)
			{
				notes[i] = restEditorNote;
			}
		}

		//Merge the rest notes
		//mergeRestNotes();
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

		do
		{
			NoteData editorNoteData = notes[index];
			noteDataList.Add(editorNoteData);

			index += editorNoteData.duration;
		}
		while (index < numPositions);

		return noteDataList;
	}
}