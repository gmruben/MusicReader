using UnityEngine;
using System.Collections;

public class NoteData
{
	public NotePitch pitch;

	public int start;
	public int duration;

	public NoteData(NotePitch pitch, int start, int duration)
	{
		this.pitch = pitch;
		this.start = start;
		this.duration = duration;
	}

	public bool isRest
	{
		get { return pitch == NotePitch.Rest; }
	}

	public bool isHold
	{
		get { return duration > 1; }
	}
}