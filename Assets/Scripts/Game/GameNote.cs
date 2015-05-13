using UnityEngine;
using System.Collections;

public class GameNote : MonoBehaviour
{
	public SpriteRenderer noteSprite;
	public GameObject missedSprite;

	public GameObject flatButton;
	public GameObject sharpButton;

	public NoteData noteData { get; private set;}
	private Animator burstAnimator;

	public void Init(NoteData noteData)
	{
		this.noteData = noteData;
		burstAnimator = GetComponent<Animator>();

		flatButton.SetActive(false);
		sharpButton.SetActive(false);

		missedSprite.SetActive(false);
		noteSprite.sprite = SpriteManager.instance.RetrieveSpriteByNotePitch(noteData.pitch);
	}

	public void Hit()
	{
		burstAnimator.Play("Burst");
	}

	public void Miss()
	{
		missedSprite.SetActive(true);
	}
}