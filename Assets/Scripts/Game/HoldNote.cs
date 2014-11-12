using UnityEngine;
using System.Collections;

public class HoldNote : MonoBehaviour
{
	private const int size = 100;

	public Transform spriteStart;
	public Transform spriteMiddle;
	public Transform spriteEnd;

	void Start()
	{
		init (5);
	}

	public void init(float time)
	{
		spriteStart.localPosition = new Vector3(0, spriteStart.localPosition.y, spriteStart.localPosition.z);
		spriteEnd.localPosition = new Vector3(size * (time + 1), spriteStart.localPosition.y, spriteStart.localPosition.z);

		spriteMiddle.localScale = new Vector3(time, 1.0f, 1.0f);
		spriteMiddle.localPosition = new Vector3(((time + 1) / 2) * size, spriteStart.localPosition.y, spriteStart.localPosition.z);

	}
}