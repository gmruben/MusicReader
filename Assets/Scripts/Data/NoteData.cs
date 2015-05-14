using UnityEngine;
using System.Collections;

public class NoteData
{
	public NotePitch pitch;

	public int start;
	public float duration;

	public NoteData(NotePitch pitch, int start, float duration)
	{
		this.pitch = pitch;
		this.start = start;
		this.duration = duration;
	}

	public int intDuration
	{
		get { return Mathf.FloorToInt(duration * 4); }
	}

	public bool isRest
	{
		get { return pitch == NotePitch.Rest; }
	}

	public bool isHold
	{
		get { return intDuration > 1; }
	}
}