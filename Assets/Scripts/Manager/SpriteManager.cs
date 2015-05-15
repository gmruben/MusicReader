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
		if (notePitch.isPitch(NotePitch.Pitch.A) || notePitch.isPitch(NotePitch.Pitch.B)) return buttonASprite;
		else if (notePitch.isPitch(NotePitch.Pitch.C) || notePitch.isPitch(NotePitch.Pitch.D)) return buttonBSprite;
		else if (notePitch.isPitch(NotePitch.Pitch.E) || notePitch.isPitch(NotePitch.Pitch.F)) return buttonXSprite;
		else return buttonYSprite;
	}

	public Sprite RetrieveButtonHoldCenterSpriteByNotePitch(NotePitch notePitch)
	{
		if (notePitch.isPitch(NotePitch.Pitch.A) || notePitch.isPitch(NotePitch.Pitch.B)) return buttonAHoldCenterSprite;
		else if (notePitch.isPitch(NotePitch.Pitch.C) || notePitch.isPitch(NotePitch.Pitch.D)) return buttonBHoldCenterSprite;
		else if (notePitch.isPitch(NotePitch.Pitch.E) || notePitch.isPitch(NotePitch.Pitch.F)) return buttonXHoldCenterSprite;
		else return buttonYHoldCenterSprite;
	}

	public Sprite RetrieveButtonHoldEndSpriteByNotePitch(NotePitch notePitch)
	{
		if (notePitch.isPitch(NotePitch.Pitch.A) || notePitch.isPitch(NotePitch.Pitch.B)) return buttonAHoldEndSprite;
		else if (notePitch.isPitch(NotePitch.Pitch.C) || notePitch.isPitch(NotePitch.Pitch.D)) return buttonBHoldEndSprite;
		else if (notePitch.isPitch(NotePitch.Pitch.E) || notePitch.isPitch(NotePitch.Pitch.F)) return buttonXHoldEndSprite;
		else return buttonYHoldEndSprite;
	}

	public override void init() { }
}