using UnityEngine;
using System.Collections;

public class HoldNote : MonoBehaviour
{
	private const float size = 150.0f;
	private const float spriteSize = 1.5f;

	public Transform center;
	public Transform end;

	public GameObject consumeAnimation;

	private float holdDuration;

	public void init(int duration)
	{
		holdDuration = duration - 1;

		center.localScale = center.localScale.setX(holdDuration * spriteSize);
		end.localPosition = end.localPosition.setX(holdDuration * size);

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

		center.localScale = center.localScale.setX(remain * spriteSize);

		center.localPosition = center.localPosition.setX(consumeDuration * size);
		consumeAnimation.transform.localPosition = consumeAnimation.transform.localPosition.setX(consumeDuration * size);
	}
}