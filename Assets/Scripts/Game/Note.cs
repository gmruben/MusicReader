using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
	public SpriteRenderer noteSprite;
	public SpriteRenderer ledgerLineSprite;

	private NoteData noteData;

	public void init(NoteData noteData)
	{
		string symbolId = NoteUtil.retrieveIdByDuration(noteData.duration, noteData.isRest);

		this.noteData = noteData;
		noteSprite.sprite = Resources.Load<Sprite>("Sprites/Symbols/" + symbolId);

		//Check if we need to show ledger lines. HACK: This is hardcoded at the moment
		if (noteData.pitch.IntValue <= 9)
		{
			ledgerLineSprite.gameObject.SetActive(true);
			ledgerLineSprite.sprite = Resources.Load<Sprite>("Sprites/Symbols/LedgerLines/LedgerLines_" + noteData.pitch.IntValue);
		}
		else
		{
			ledgerLineSprite.gameObject.SetActive(false);
		}
	}
}