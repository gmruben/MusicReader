using UnityEngine;
using System.Collections;

public class SymbolData
{
	public SymbolId id;
	public float measure;

	public static float retrieveDurationById(SymbolId symbolId)
	{
		switch(symbolId)
		{
		case SymbolId.Semibreve:
			return 4.0f;
		case SymbolId.DottedMinim:
			return 3.0f;
		case SymbolId.Minim:
			return 2.0f;
		case SymbolId.DottedCrotchet:
			return 1.5f;
		case SymbolId.Crotchet:
			return 1.0f;
		case SymbolId.DottedQuaver:
			return 0.75f;
		case SymbolId.Quaver:
			return 0.5f;
		case SymbolId.DottedSemiquaver:
			return 0.375f;
		case SymbolId.Semiquaver:
			return 0.25f;
		}

		return 1.0f;
	}
}

public enum SymbolId
{
	Semibreve,
	DottedMinim,
	Minim,
	DottedCrotchet,
	Crotchet,
	DottedQuaver,
	Quaver,
	DottedSemiquaver,
	Semiquaver
}