using UnityEngine;
using System.Collections;

public class UICreator
{
	public static NoteButton createNoteButton()
	{
		GameObject resource = Resources.Load<GameObject>("Menus/NoteButton");
		return (GameObject.Instantiate(resource) as GameObject).GetComponent<NoteButton>();
	}
}