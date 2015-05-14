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
		if (notePitch == NotePitch.A || notePitch == NotePitch.B) return buttonASprite;
		else if (notePitch == NotePitch.C || notePitch == NotePitch.D) return buttonBSprite;
		else if (notePitch == NotePitch.E || notePitch == NotePitch.F) return buttonXSprite;
		else return buttonYSprite;
	}

	public Sprite RetrieveButtonHoldCenterSpriteByNotePitch(NotePitch notePitch)
	{
		if (notePitch == NotePitch.A || notePitch == NotePitch.B) return buttonAHoldCenterSprite;
		else if (notePitch == NotePitch.C || notePitch == NotePitch.D) return buttonBHoldCenterSprite;
		else if (notePitch == NotePitch.E || notePitch == NotePitch.F) return buttonXHoldCenterSprite;
		else return buttonYHoldCenterSprite;
	}

	public Sprite RetrieveButtonHoldEndSpriteByNotePitch(NotePitch notePitch)
	{
		if (notePitch == NotePitch.A || notePitch == NotePitch.B) return buttonAHoldEndSprite;
		else if (notePitch == NotePitch.C || notePitch == NotePitch.D) return buttonBHoldEndSprite;
		else if (notePitch == NotePitch.E || notePitch == NotePitch.F) return buttonXHoldEndSprite;
		else return buttonYHoldEndSprite;
	}

	public override void init() { }
}