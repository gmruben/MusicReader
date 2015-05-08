using UnityEngine;
using System.Collections;

public sealed class NotePitch
{
	private readonly string name;
	private readonly int value;
	
	public static readonly NotePitch A = new NotePitch (1, "A");
	public static readonly NotePitch ASharp = new NotePitch (2, "A#");
	public static readonly NotePitch B = new NotePitch (3, "B");
	public static readonly NotePitch C = new NotePitch (4, "C");
	public static readonly NotePitch CSharp = new NotePitch (5, "C#");
	public static readonly NotePitch D = new NotePitch (6, "D");
	public static readonly NotePitch DSharp = new NotePitch (7, "D#");
	public static readonly NotePitch E = new NotePitch (8, "E");
	public static readonly NotePitch F = new NotePitch (9, "F");
	public static readonly NotePitch FSharp = new NotePitch (10, "F#");
	public static readonly NotePitch G = new NotePitch (11, "G");
	public static readonly NotePitch GSharp = new NotePitch (12, "G#");

	public static readonly NotePitch Rest = new NotePitch (12, "R");

	public static NotePitch[] notes = new NotePitch[12] {  A, ASharp, B, C, CSharp, D, DSharp, E, F, FSharp, G, GSharp };
	public static NotePitch[] all = new NotePitch[13] {  A, ASharp, B, C, CSharp, D, DSharp, E, F, FSharp, G, GSharp, Rest };

	private NotePitch (int value, string name)
	{
		this.name = name;
		this.value = value;
	}

	public int IntValue
	{
		get { return value; }
	}

	public string StringValue
	{
		get { return name; }
	}

	public NotePitch Add(int addValue)
	{
		int index = (value + addValue) % (notes.Length - 1);
		return notes[index - 1];
	}

	public static NotePitch ParseString(string stringPitch)
	{
		for (int i = 0; i < all.Length; i++)
		{
			if (all[i].StringValue == stringPitch)
			{
				return all[i];
			}
		}
		return all[0];
	}
}