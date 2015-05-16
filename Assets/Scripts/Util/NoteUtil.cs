using UnityEngine;
using System.Collections;

public class NoteUtil
{
	public static int retrieveDurationById(NoteId noteId)
	{
		switch(noteId)
		{
		case NoteId.Semibreve:
			return 16;
		case NoteId.DottedMinim:
			return 12;
		case NoteId.Minim:
			return 8;
		case NoteId.DottedCrotchet:
			return 6;
		case NoteId.Crotchet:
			return 4;
		case NoteId.DottedQuaver:
			return 3;
		case NoteId.Quaver:
			return 2;
		case NoteId.Semiquaver:
			return 1;
		}
		
		return 4;
	}
	
	public static string retrieveIdByDuration(int duration, bool isRest)
	{
		switch(duration)
		{
		case 16:
			return (isRest ? NoteId.SemibreveRest : NoteId.Semibreve).ToString();
		case 12:
			return NoteId.DottedMinim.ToString();
		case 8:
			return (isRest ? NoteId.MinimRest : NoteId.Minim).ToString();
		case 6: 
			return NoteId.DottedCrotchet.ToString();
		case 4:
			return (isRest ? NoteId.CrotchetRest : NoteId.Crotchet).ToString();
		case 3:
			return NoteId.DottedQuaver.ToString();
		case 2:
			return (isRest ? NoteId.QuaverRest : NoteId.Quaver).ToString();
		case 1:
			return (isRest ? NoteId.SemiquaverRest : NoteId.Semiquaver).ToString();
		}
		
		return NoteId.Crotchet.ToString();
	}
}