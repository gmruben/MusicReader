using UnityEngine;
using System.Collections;

public class SpriteManager : Singleton<SpriteManager>
{
	public Sprite buttonASprite;
	public Sprite buttonBSprite;
	public Sprite buttonXSprite;
	public Sprite buttonYSprite;

	public Sprite RetrieveSpriteByNotePitch(NotePitch notePitch)
	{
		if (notePitch == NotePitch.A || notePitch == NotePitch.B) return buttonASprite;
		else if (notePitch == NotePitch.C || notePitch == NotePitch.D) return buttonBSprite;
		else if (notePitch == NotePitch.E || notePitch == NotePitch.F) return buttonXSprite;
		else return buttonYSprite;
	}

	public override void init() { }
}