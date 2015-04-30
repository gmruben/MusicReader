using UnityEngine;
using System.Collections;

public class Tween
{
	public bool hasEnded { get; private set; }

	private float startValue;
	private float endValue;
	private float deltaValue;

	private float speed;
	private float currentTime;

	public Tween(float startValue, float endValue, float speed)
	{
		this.startValue = startValue;
		this.endValue = endValue;

		this.speed = speed;

		deltaValue = endValue - startValue;
		currentTime = 0;

		hasEnded = false;
	}

	public float update(float time)
	{
		currentTime += time * speed;
		if (currentTime >= 1.0f)
		{
			currentTime = 1.0f;
			hasEnded = true;
		}

		return startValue + deltaValue * ScaleFuncs.QuadraticEaseInOutImpl(currentTime);
	}
}