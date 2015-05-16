using UnityEngine;
using System;
using System.Collections;

public class GameNote : MonoBehaviour
{
	public event Action OnEnd;

	public SpriteRenderer noteSprite;
	public GameObject missedSprite;

	public GameObject flatButton;
	public GameObject sharpButton;

	public HoldNote holdNote;

	public NoteData noteData { get; private set;}
	//Whether we have finished the note or not (hit, missed or finished hold)
	public bool isDone { get; private set;}

	private Animator burstAnimator;

	public void Init(NoteData noteData)
	{
		this.noteData = noteData;
		burstAnimator = GetComponent<Animator>();

		flatButton.SetActive(false);
		sharpButton.SetActive(false);

		missedSprite.SetActive(false);
		noteSprite.sprite = SpriteManager.instance.RetrieveButtonSpriteByNotePitch(noteData.pitch);

		if (noteData.isHold)
		{
			holdNote.gameObject.SetActive(true);
			holdNote.init(noteData.duration);
		}
		else
		{
			holdNote.gameObject.SetActive(false);
		}
	}

	public void Hit()
	{
		if (noteData.isHold)
		{
			holdNote.hit();
		}
		else
		{
			//If it is not a hold note, dispatch OnEnd event
			if (OnEnd != null) OnEnd();
		}

		burstAnimator.Play("Burst");
	}

	public void UpdateHold(float duration)
	{
		if (noteData.duration > 1)
		{
			//HACK:Unify this -1 thing for hold notes (and the intDuration thing)
			if (duration < noteData.duration - 1)
			{
				holdNote.update(duration);
			}
			else
			{
				if (OnEnd != null) OnEnd();
			}
		}
	}

	public void Miss()
	{
		missedSprite.SetActive(true);
	}
}