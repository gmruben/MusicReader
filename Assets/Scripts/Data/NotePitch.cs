using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class NotePitch
{
	private const int numOctaves = 4;
	private const int numNotes = 8;
	private const int numAllNotes = 13;

	private readonly string name;
	private readonly Pitch pitch;
	private readonly int octave;
	private readonly int midiNote;

	public enum Pitch { A, ASharp, B, C, CSharp, D, DSharp, E, F, FSharp, G, GSharp, Rest };
	
	public static readonly NotePitch A2 = new NotePitch 		("A2", 	Pitch.A, 		2, 48);
	public static readonly NotePitch A2Sharp = new NotePitch 	("A2#", Pitch.ASharp, 	2, 49);
	public static readonly NotePitch B2 = new NotePitch 		("B2", 	Pitch.B, 		2, 50);
	public static readonly NotePitch C2 = new NotePitch 		("C2", 	Pitch.C, 		2, 51);
	public static readonly NotePitch C2Sharp = new NotePitch 	("C2#", Pitch.CSharp, 	2, 52);
	public static readonly NotePitch D2 = new NotePitch 		("D2", 	Pitch.D, 		2, 53);
	public static readonly NotePitch D2Sharp = new NotePitch 	("D2#", Pitch.DSharp, 	2, 54);
	public static readonly NotePitch E2 = new NotePitch 		("E2", 	Pitch.E, 		2, 55);
	public static readonly NotePitch F2 = new NotePitch 		("F2", 	Pitch.F, 		2, 56);
	public static readonly NotePitch F2Sharp = new NotePitch 	("F2#", Pitch.FSharp, 	2, 57);
	public static readonly NotePitch G2 = new NotePitch 		("G2", 	Pitch.G, 		2, 58);
	public static readonly NotePitch G2Sharp = new NotePitch 	("G2#", Pitch.GSharp, 	2, 59);

	public static readonly NotePitch A3 = new NotePitch 		("A3", 	Pitch.A, 		3, 60);
	public static readonly NotePitch A3Sharp = new NotePitch 	("A3#", Pitch.ASharp, 	3, 61);
	public static readonly NotePitch B3 = new NotePitch 		("B3", 	Pitch.B, 		3, 62);
	public static readonly NotePitch C3 = new NotePitch 		("C3", 	Pitch.C, 		3, 63);
	public static readonly NotePitch C3Sharp = new NotePitch 	("C3#", Pitch.CSharp, 	3, 64);
	public static readonly NotePitch D3 = new NotePitch 		("D3",	Pitch.D, 		3, 65);
	public static readonly NotePitch D3Sharp = new NotePitch 	("D3#", Pitch.DSharp, 	3, 66);
	public static readonly NotePitch E3 = new NotePitch 		("E3", 	Pitch.E, 		3, 67);
	public static readonly NotePitch F3 = new NotePitch 		("F3", 	Pitch.F, 		3, 68);
	public static readonly NotePitch F3Sharp = new NotePitch 	("F3#", Pitch.FSharp, 	3, 69);
	public static readonly NotePitch G3 = new NotePitch 		("G3", 	Pitch.G, 		3, 70);
	public static readonly NotePitch G3Sharp = new NotePitch 	("G3#", Pitch.GSharp, 	3, 71);

	public static readonly NotePitch A4 = new NotePitch 		("A4", 	Pitch.A, 		4, 72);
	public static readonly NotePitch A4Sharp = new NotePitch 	("A4#", Pitch.ASharp, 	4, 73);
	public static readonly NotePitch B4 = new NotePitch 		("B4", 	Pitch.B, 		4, 74);
	public static readonly NotePitch C4 = new NotePitch 		("C4", 	Pitch.C, 		4, 75);
	public static readonly NotePitch C4Sharp = new NotePitch 	("C4#", Pitch.CSharp, 	4, 76);
	public static readonly NotePitch D4 = new NotePitch 		("D4", 	Pitch.D, 		4, 77);
	public static readonly NotePitch D4Sharp = new NotePitch 	("D4#", Pitch.DSharp, 	4, 78);
	public static readonly NotePitch E4 = new NotePitch 		("E4", 	Pitch.E, 		4, 79);
	public static readonly NotePitch F4 = new NotePitch 		("F4", 	Pitch.F, 		4, 80);
	public static readonly NotePitch F4Sharp = new NotePitch 	("F4#", Pitch.FSharp, 	4, 81);
	public static readonly NotePitch G4 = new NotePitch 		("G4", 	Pitch.G, 		4, 82);
	public static readonly NotePitch G4Sharp = new NotePitch 	("G4#", Pitch.GSharp, 	4, 83);

	public static readonly NotePitch A5 = new NotePitch 		("A5", 	Pitch.A, 		5, 84);
	public static readonly NotePitch A5Sharp = new NotePitch 	("A5#", Pitch.ASharp, 	5, 85);
	public static readonly NotePitch B5 = new NotePitch 		("B5", 	Pitch.B, 		5, 86);
	public static readonly NotePitch C5 = new NotePitch 		("C5", 	Pitch.C, 		5, 87);
	public static readonly NotePitch C5Sharp = new NotePitch 	("C5#", Pitch.CSharp, 	5, 88);
	public static readonly NotePitch D5 = new NotePitch 		("D5", 	Pitch.D, 		5, 89);
	public static readonly NotePitch D5Sharp = new NotePitch 	("D5#", Pitch.DSharp, 	5, 90);
	public static readonly NotePitch E5 = new NotePitch 		("E5", 	Pitch.E, 		5, 91);
	public static readonly NotePitch F5 = new NotePitch 		("F5", 	Pitch.F, 		5, 92);
	public static readonly NotePitch F5Sharp = new NotePitch 	("F5#", Pitch.FSharp, 	5, 93);
	public static readonly NotePitch G5 = new NotePitch 		("G5", 	Pitch.G, 		5, 94);
	public static readonly NotePitch G5Sharp = new NotePitch 	("G5#", Pitch.GSharp, 	5, 95);

	public static readonly NotePitch Rest = new NotePitch 		("R", 	Pitch.Rest, 	0, 00);

	/*public static NotePitch[] notes = new NotePitch[numNotes * numOctaves]
	{
		A2, A2Sharp, B2, C2, C2Sharp, D2, D2Sharp, E2, F2, F2Sharp, G2, G2Sharp,
		A3, A3Sharp, B3, C3, C3Sharp, D3, D3Sharp, E3, F3, F3Sharp, G3, G3Sharp,
		A4, A4Sharp, B4, C4, C4Sharp, D4, D4Sharp, E4, F4, F4Sharp, G4, G4Sharp,
		A5, A5Sharp, B5, C5, C5Sharp, D5, D5Sharp, E5, F5, F5Sharp, G5, G5Sharp,
	};*/

	public static Dictionary<string, NotePitch> noteDictionary;
	public static List<NotePitch> CNoteList = new List<NotePitch>()
	{
		A2, B2, C2, D2, E2, F2, G2,
		A3, B3, C3, D3, E3, F3, G3,
		A4, B4, C4, D4, E4, F4, G4,
		A5, B5, C5, D5, E5, F5, G5,
	};

	public static List<NotePitch> noteList = new List<NotePitch>()
	{
		A2, A2Sharp, B2, C2, C2Sharp, D2, D2Sharp, E2, F2, F2Sharp, G2, G2Sharp,
		A3, A3Sharp, B3, C3, C3Sharp, D3, D3Sharp, E3, F3, F3Sharp, G3, G3Sharp,
		A4, A4Sharp, B4, C4, C4Sharp, D4, D4Sharp, E4, F4, F4Sharp, G4, G4Sharp,
		A5, A5Sharp, B5, C5, C5Sharp, D5, D5Sharp, E5, F5, F5Sharp, G5, G5Sharp,
	};

	LinkedList<NotePitch> list;

	//public static NotePitch[] allNotes = new NotePitch[numAllNotes] {  A, ASharp, B, C, CSharp, D, DSharp, E, F, FSharp, G, GSharp, Rest };

	private NotePitch (string name, Pitch pitch, int octave, int midiNote)
	{
		this.name = name;
		this.pitch = pitch;
		this.octave = octave;
		this.midiNote = midiNote;
	}

	public int IntValue
	{
		get { return CNoteList.IndexOf(this); }
	}

	public string StringValue
	{
		get { return name; }
	}

	public NotePitch Add(int addValue)
	{
		return CNoteList[IntValue + addValue];
	}

	public static NotePitch ParseString(string stringPitch)
	{
		//If the dictionary is null, create it
		if (noteDictionary == null) CreateNoteDictionary();

		if (stringPitch == "R") return Rest;
		else return noteDictionary[stringPitch];
	}

	public bool isPitch(Pitch pitch)
	{
		return this.pitch == pitch;
	}

	private static void CreateNoteDictionary()
	{
		noteDictionary = new Dictionary<string, NotePitch>();
		foreach (NotePitch notePitch in noteList)
		{
			noteDictionary.Add(notePitch.StringValue, notePitch);
		}
	}
}