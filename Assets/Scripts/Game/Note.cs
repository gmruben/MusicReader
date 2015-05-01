using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
	public SpriteRenderer sprite;
	private NoteData noteData;

	public void init(NoteData noteData)
	{
		string symbolId = SymbolData.retrieveIdByDuration(noteData.intDuration, noteData.isRest);

		this.noteData = noteData;
		sprite.sprite = Resources.Load<Sprite>("Sprites/Symbols/" + symbolId);
	}
}