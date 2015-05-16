using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
	public SpriteRenderer sprite;
	private NoteData noteData;

	public void init(NoteData noteData)
	{
		string symbolId = NoteUtil.retrieveIdByDuration(noteData.duration, noteData.isRest);

		this.noteData = noteData;
		sprite.sprite = Resources.Load<Sprite>("Sprites/Symbols/" + symbolId);
	}
}