using UnityEngine;
using System.Collections;

public class NoteData
{
	public string pitch;
	
	public float duration;

	public NoteData(string pitch, float duration)
	{
		this.pitch = pitch;
		this.duration = duration;
	}

	public int intDuration
	{
		get { return Mathf.FloorToInt(duration * 4); }
	}

	public bool isRest
	{
		get { return pitch == "rest"; }
	}
}