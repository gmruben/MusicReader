using UnityEngine;
using System.Collections;

public class SpriteManager : Singleton<SpriteManager>
{
	public Sprite buttonASprite;
	public Sprite buttonBSprite;
	public Sprite buttonXSprite;
	public Sprite buttonYSprite;

	public Sprite buttonAHoldCenterSprite;
	public Sprite buttonBHoldCenterSprite;
	public Sprite buttonXHoldCenterSprite;
	public Sprite buttonYHoldCenterSprite;

	public Sprite buttonAHoldEndSprite;
	public Sprite buttonBHoldEndSprite;
	public Sprite buttonXHoldEndSprite;
	public Sprite buttonYHoldEndSprite;

	public Sprite RetrieveButtonSpriteByNotePitch(NotePitch notePitch)
	{
		if (notePitch.IsNote(NotePitch.Note.A) || notePitch.IsNote(NotePitch.Note.B)) return buttonASprite;
		else if (notePitch.IsNote(NotePitch.Note.C) || notePitch.IsNote(NotePitch.Note.D)) return buttonBSprite;
		else if (notePitch.IsNote(NotePitch.Note.E) || notePitch.IsNote(NotePitch.Note.F)) return buttonXSprite;
		else return buttonYSprite;
	}

	public Sprite RetrieveButtonHoldCenterSpriteByNotePitch(NotePitch notePitch)
	{
		if (notePitch.IsNote(NotePitch.Note.A) || notePitch.IsNote(NotePitch.Note.B)) return buttonAHoldCenterSprite;
		else if (notePitch.IsNote(NotePitch.Note.C) || notePitch.IsNote(NotePitch.Note.D)) return buttonBHoldCenterSprite;
		else if (notePitch.IsNote(NotePitch.Note.E) || notePitch.IsNote(NotePitch.Note.F)) return buttonXHoldCenterSprite;
		else return buttonYHoldCenterSprite;
	}

	public Sprite RetrieveButtonHoldEndSpriteByNotePitch(NotePitch notePitch)
	{
		if (notePitch.IsNote(NotePitch.Note.A) || notePitch.IsNote(NotePitch.Note.B)) return buttonAHoldEndSprite;
		else if (notePitch.IsNote(NotePitch.Note.C) || notePitch.IsNote(NotePitch.Note.D)) return buttonBHoldEndSprite;
		else if (notePitch.IsNote(NotePitch.Note.E) || notePitch.IsNote(NotePitch.Note.F)) return buttonXHoldEndSprite;
		else return buttonYHoldEndSprite;
	}

	public override void Init() { }
}