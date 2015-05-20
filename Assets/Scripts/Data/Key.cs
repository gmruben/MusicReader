using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Key
{
	public string name;
	public List<NotePitch> noteList;

	//Natural
	public static readonly Key C = new Key("C", KeyNoteList.CNoteList);
	
	//Sharps
	public static readonly Key G = new Key("G", KeyNoteList.GNoteList);
	public static readonly Key D = new Key("D", KeyNoteList.DNoteList);
	public static readonly Key A = new Key("A", KeyNoteList.ANoteList);
	public static readonly Key E = new Key("E", KeyNoteList.ENoteList);
	public static readonly Key B = new Key("B", KeyNoteList.BNoteList);
	//public static readonly Key FSharp = new Key("F#", KeyNoteList.FSharpNoteList);
	//public static readonly Key CSharp = new Key("C#", KeyNoteList.CSharpNoteList);
	
	//Flats
	public static readonly Key F = new Key("F", KeyNoteList.FNoteList );
	public static readonly Key BFlat = new Key("Bb", KeyNoteList.BFlatNoteList);
	public static readonly Key EFlat = new Key("Eb", KeyNoteList.EFlatNoteList);
	public static readonly Key AFlat = new Key("Ab", KeyNoteList.AFlatNoteList);
	public static readonly Key DFlat = new Key("Db", KeyNoteList.DFlatNoteList);
	//public static readonly Key GFlat = new Key("Gb", KeyNoteList.GFlatNoteList);
	//public static readonly Key CFlat = new Key("Cb", KeyNoteList.CFlatNoteList);
	
	private static Dictionary<string, Key> keyDictionary;
	private static List<Key> keyList = new List<Key>() { C, G, D, A, E, B, F, BFlat, EFlat, AFlat, DFlat };

	private Key(string name, List<NotePitch> noteList)
	{
		this.name = name;
		this.noteList = noteList;
	}

	public Key Prev()
	{
		int index = keyList.IndexOf(this) - 1;
		if (index < 0) index = keyList.Count - 1;

		return keyList[index];
	}

	public Key Next()
	{
		int index = keyList.IndexOf(this) + 1;
		if (index > keyList.Count - 1) index = 0;
		
		return keyList[index];
	}

	public static Key ParseString(string stringKey)
	{
		//If the dictionary is null, create it
		if (keyDictionary == null) CreateKeyDictionary();
		return keyDictionary[stringKey];
	}
	
	private static void CreateKeyDictionary()
	{
		keyDictionary = new Dictionary<string, Key>();
		foreach (Key key in keyList)
		{
			keyDictionary.Add(key.name, key);
		}
	}
}