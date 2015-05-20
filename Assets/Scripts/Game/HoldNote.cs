using UnityEngine;
using System.Collections;

public class HoldNote : MonoBehaviour
{
	private const float size = 150.0f;
	private const float spriteSize = 1.5f;

	public SpriteRenderer center;
	public SpriteRenderer end;

	public GameObject consumeAnimation;

	private float holdDuration;

	public void init(NoteData noteData)
	{
		holdDuration = noteData.duration - 1;

		center.transform.localScale = center.transform.localScale.setX(holdDuration * spriteSize);
		end.transform.localPosition = end.transform.localPosition.setX(holdDuration * size);

		center.sprite = SpriteManager.instance.RetrieveButtonHoldCenterSpriteByNotePitch(noteData.pitch);
		end.sprite = SpriteManager.instance.RetrieveButtonHoldEndSpriteByNotePitch(noteData.pitch);

		consumeAnimation.SetActive(false);
	}

	public void hit()
	{
		consumeAnimation.SetActive(true);
	}

	public void update(float consumeDuration)
	{
		//Calculate the duration left on the note
		float remain = holdDuration - consumeDuration;

		center.transform.localScale = center.transform.localScale.setX(remain * spriteSize);

		center.transform.localPosition = center.transform.localPosition.setX(consumeDuration * size);
		consumeAnimation.transform.localPosition = consumeAnimation.transform.localPosition.setX(consumeDuration * size);
	}
}