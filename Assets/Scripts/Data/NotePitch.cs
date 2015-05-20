using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class NotePitch
{
	private readonly string name;
	private readonly Note note;
	private readonly int octave;
	public readonly int midiNote;

	public enum Note { A, B, C, D, E, F, G, Rest };
	
	public static readonly NotePitch A2 = new NotePitch 		("A2", 	Note.A, 2, 48);
	public static readonly NotePitch A2Sharp = new NotePitch 	("A2#", Note.A, 2, 49);
	public static readonly NotePitch B2 = new NotePitch 		("B2", 	Note.B, 2, 50);
	public static readonly NotePitch C2 = new NotePitch 		("C2", 	Note.C, 2, 51);
	public static readonly NotePitch C2Sharp = new NotePitch 	("C2#", Note.C, 2, 52);
	public static readonly NotePitch D2 = new NotePitch 		("D2", 	Note.D, 2, 53);
	public static readonly NotePitch D2Sharp = new NotePitch 	("D2#", Note.D, 2, 54);
	public static readonly NotePitch E2 = new NotePitch 		("E2", 	Note.E, 2, 55);
	public static readonly NotePitch F2 = new NotePitch 		("F2", 	Note.F, 2, 56);
	public static readonly NotePitch F2Sharp = new NotePitch 	("F2#", Note.F, 2, 57);
	public static readonly NotePitch G2 = new NotePitch 		("G2", 	Note.G, 2, 58);
	public static readonly NotePitch G2Sharp = new NotePitch 	("G2#", Note.G, 2, 59);

	public static readonly NotePitch A2Flat = new NotePitch 	("A2b", Note.A, 2, 47);
	public static readonly NotePitch B2Flat = new NotePitch 	("B2b", Note.B, 2, 49);
	public static readonly NotePitch D2Flat = new NotePitch 	("D2b", Note.D, 2, 52);
	public static readonly NotePitch E2Flat = new NotePitch 	("E2b", Note.E, 2, 54);
	public static readonly NotePitch G2Flat = new NotePitch 	("G2b", Note.G, 2, 57);

	public static readonly NotePitch A3 = new NotePitch 		("A3", 	Note.A, 3, 60);
	public static readonly NotePitch A3Sharp = new NotePitch 	("A3#", Note.A, 3, 61);
	public static readonly NotePitch B3 = new NotePitch 		("B3", 	Note.B, 3, 62);
	public static readonly NotePitch C3 = new NotePitch 		("C3", 	Note.C, 3, 63);
	public static readonly NotePitch C3Sharp = new NotePitch 	("C3#", Note.C, 3, 64);
	public static readonly NotePitch D3 = new NotePitch 		("D3",	Note.D, 3, 65);
	public static readonly NotePitch D3Sharp = new NotePitch 	("D3#", Note.D, 3, 66);
	public static readonly NotePitch E3 = new NotePitch 		("E3", 	Note.E, 3, 67);
	public static readonly NotePitch F3 = new NotePitch 		("F3", 	Note.F, 3, 68);
	public static readonly NotePitch F3Sharp = new NotePitch 	("F3#", Note.F, 3, 69);
	public static readonly NotePitch G3 = new NotePitch 		("G3", 	Note.G, 3, 70);
	public static readonly NotePitch G3Sharp = new NotePitch 	("G3#", Note.G, 3, 71);

	public static readonly NotePitch A3Flat = new NotePitch 	("A3b", Note.A, 3, 59);
	public static readonly NotePitch B3Flat = new NotePitch 	("B3b", Note.B, 3, 61);
	public static readonly NotePitch D3Flat = new NotePitch 	("D3b", Note.D, 3, 64);
	public static readonly NotePitch E3Flat = new NotePitch 	("E3b", Note.E, 3, 66);
	public static readonly NotePitch G3Flat = new NotePitch 	("G3b", Note.G, 3, 69);

	public static readonly NotePitch A4 = new NotePitch 		("A4", 	Note.A, 4, 72);
	public static readonly NotePitch A4Sharp = new NotePitch 	("A4#", Note.A, 4, 73);
	public static readonly NotePitch B4 = new NotePitch 		("B4", 	Note.B, 4, 74);
	public static readonly NotePitch C4 = new NotePitch 		("C4", 	Note.C, 4, 75);
	public static readonly NotePitch C4Sharp = new NotePitch 	("C4#", Note.C, 4, 76);
	public static readonly NotePitch D4 = new NotePitch 		("D4", 	Note.D, 4, 77);
	public static readonly NotePitch D4Sharp = new NotePitch 	("D4#", Note.D, 4, 78);
	public static readonly NotePitch E4 = new NotePitch 		("E4", 	Note.E, 4, 79);
	public static readonly NotePitch F4 = new NotePitch 		("F4", 	Note.F, 4, 80);
	public static readonly NotePitch F4Sharp = new NotePitch 	("F4#", Note.F, 4, 81);
	public static readonly NotePitch G4 = new NotePitch 		("G4", 	Note.G, 4, 82);
	public static readonly NotePitch G4Sharp = new NotePitch 	("G4#", Note.G, 4, 83);

	public static readonly NotePitch A4Flat = new NotePitch 	("A4b", Note.A, 4, 71);
	public static readonly NotePitch B4Flat = new NotePitch 	("B4b", Note.B, 4, 73);
	public static readonly NotePitch D4Flat = new NotePitch 	("D4b", Note.D, 4, 76);
	public static readonly NotePitch E4Flat = new NotePitch 	("E4b", Note.E, 4, 78);
	public static readonly NotePitch G4Flat = new NotePitch 	("G4b", Note.G, 4, 81);

	public static readonly NotePitch A5 = new NotePitch 		("A5", 	Note.A, 5, 84);
	public static readonly NotePitch A5Sharp = new NotePitch 	("A5#", Note.A, 5, 85);
	public static readonly NotePitch B5 = new NotePitch 		("B5", 	Note.B, 5, 86);
	public static readonly NotePitch C5 = new NotePitch 		("C5", 	Note.C, 5, 87);
	public static readonly NotePitch C5Sharp = new NotePitch 	("C5#", Note.C, 5, 88);
	public static readonly NotePitch D5 = new NotePitch 		("D5", 	Note.D, 5, 89);
	public static readonly NotePitch D5Sharp = new NotePitch 	("D5#", Note.D, 5, 90);
	public static readonly NotePitch E5 = new NotePitch 		("E5", 	Note.E, 5, 91);
	public static readonly NotePitch F5 = new NotePitch 		("F5", 	Note.F, 5, 92);
	public static readonly NotePitch F5Sharp = new NotePitch 	("F5#", Note.F, 5, 93);
	public static readonly NotePitch G5 = new NotePitch 		("G5", 	Note.G, 5, 94);
	public static readonly NotePitch G5Sharp = new NotePitch 	("G5#", Note.G, 5, 95);

	public static readonly NotePitch A5Flat = new NotePitch 	("A5b", Note.A, 5, 83);
	public static readonly NotePitch B5Flat = new NotePitch 	("B5b", Note.B, 5, 85);
	public static readonly NotePitch D5Flat = new NotePitch 	("D5b", Note.D, 5, 88);
	public static readonly NotePitch E5Flat = new NotePitch 	("E5b", Note.E, 5, 90);
	public static readonly NotePitch G5Flat = new NotePitch 	("G5b", Note.G, 5, 93);

	public static readonly NotePitch Rest = new NotePitch 		("R", 	Note.Rest, 	0, 00);

	private static Dictionary<string, NotePitch> noteDictionary;
	private static List<NotePitch> noteList = new List<NotePitch>()
	{
		A2Flat, A2, A2Sharp, B2Flat, B2, C2, C2Sharp, D2Flat, D2, D2Sharp, E2Flat, E2, F2, F2Sharp, G2Flat, G2, G2Sharp,
		A3Flat, A3, A3Sharp, B3Flat, B3, C3, C3Sharp, D3Flat, D3, D3Sharp, E3Flat, E3, F3, F3Sharp, G3Flat, G3, G3Sharp,
		A4Flat, A4, A4Sharp, B4Flat, B4, C4, C4Sharp, D4Flat, D4, D4Sharp, E4Flat, E4, F4, F4Sharp, G4Flat, G4, G4Sharp,
		A5Flat, A5, A5Sharp, B5Flat, B5, C5, C5Sharp, D5Flat, D5, D5Sharp, E5Flat, E5, F5, F5Sharp, G5Flat, G5, G5Sharp,
	};

	LinkedList<NotePitch> list;

	private NotePitch (string name, Note pitch, int octave, int midiNote)
	{
		this.name = name;
		this.note = pitch;
		this.octave = octave;
		this.midiNote = midiNote;
	}

	public int RetrieveNoteIndex(Key key)
	{
		if (this.isRest) return 0;

		if (key.noteList.Contains(this)) return key.noteList.IndexOf(this);
		else Debug.LogError("THE KEY '" + key.name + "' DOES NOT CONTAIN THE NOTE '" + this.name + "'");

		return 0;
	}

	public string StringValue
	{
		get { return name; }
	}

	public bool isRest
	{
		get { return IsNote(Note.Rest); }
	}

	public NotePitch Add(Key key, int addValue)
	{
		int noteIndex = RetrieveNoteIndex(key);
		return key.noteList[noteIndex + addValue];
	}

	public bool IsNote(Note note)
	{
		return this.note == note;
	}

	public static NotePitch ParseString(string stringPitch)
	{
		//If the dictionary is null, create it
		if (noteDictionary == null) CreateNoteDictionary();

		if (stringPitch == "R") return Rest;
		else return noteDictionary[stringPitch];
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