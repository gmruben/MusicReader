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

	public static string retrieveIdByDuration(int duration, bool isRest)
	{
		switch(duration)
		{
		case 16:
			return (isRest ? SymbolId.SemibreveRest : SymbolId.Semibreve).ToString();
		case 12:
			return SymbolId.DottedMinim.ToString();
		case 8:
			return (isRest ? SymbolId.MinimRest : SymbolId.Minim).ToString();
		case 6: 
			return SymbolId.DottedCrotchet.ToString();
		case 4:
			return (isRest ? SymbolId.CrotchetRest : SymbolId.Crotchet).ToString();
		case 3:
			return SymbolId.DottedQuaver.ToString();
		case 2:
			return (isRest ? SymbolId.QuaverRest : SymbolId.Quaver).ToString();
		case 1:
			return (isRest ? SymbolId.SemiquaverRest : SymbolId.Semiquaver).ToString();
		}
		
		return SymbolId.Crotchet.ToString();
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
	Semiquaver,

	SemibreveRest,
	MinimRest,
	CrotchetRest,
	QuaverRest,
	SemiquaverRest
}