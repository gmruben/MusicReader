using System.Collections;
using System.Collections.Generic;

public class TrackData
{
	public string id;
	public string name;

	public InstrumentId instrumentId;

	public List<BarData> barList;

	public TrackData(string id, string name, InstrumentId instrumentId)
	{
		this.id = id;
		this.name = name;
		this.instrumentId = instrumentId;

		barList = new List<BarData>();
	}

	/// <summary>
	/// Retrieves a list with all the notes of all the bars.
	/// </summary>
	/// <returns>The note list.</returns>
	public List<NoteData> retrieveNoteList()
	{
		List<NoteData> noteList = new List<NoteData>();
		foreach (BarData barData in barList)
		{
			noteList.AddRange(barData.noteList);
		}
		return noteList;
	}
}