using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarData
{
	public int index;
	public int beatsPerMinute;

	public List<NoteData> noteList;

	public BarData(int index, int beatsPerMinute)
	{
		this.index = index;
		this.beatsPerMinute = beatsPerMinute;

		noteList = new List<NoteData>();
	}
}