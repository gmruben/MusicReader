using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarData
{
	public int beatsPerMinute;
	public List<NoteData> noteList;

	public BarData(int beatsPerMinute)
	{
		this.beatsPerMinute = beatsPerMinute;
		noteList = new List<NoteData>();
	}
}