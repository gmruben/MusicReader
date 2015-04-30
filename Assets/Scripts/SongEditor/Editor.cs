using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Editor : MonoBehaviour
{
	private const int barSize = 900;
	private const float speed = 2.5f;
	private const int numBars = 10;

	public GameObject barPrefab;

	private List<Bar> barList;

	private int currentIndex;
	private Transform cachedTransform;
	
	private bool inMove;
	private float targetX;

	private Tween tween;

	void Start()
	{
		init ();
	}

	void Update()
	{
		if (inMove)
		{
			if (!tween.hasEnded)
			{
				float posx = tween.update(Time.deltaTime);
				cachedTransform.position = cachedTransform.position.setX(posx);
			}
		}
	}

	public void init()
	{
		currentIndex = 0;
		cachedTransform = transform;

		inMove = false;
		targetX = 0;

		barList = new List<Bar>();
		for (int i = 0; i < numBars; i++)
		{
			GameObject barGameObject = GameObject.Instantiate(barPrefab) as GameObject;
			Bar bar = barGameObject.GetComponent<Bar>();

			bar.transform.parent = cachedTransform;
			bar.transform.localPosition = new Vector3(i * barSize, 0, 0);

			barList.Add(bar);
		}
	}

	public void move(int direction)
	{
		if ((direction < 0 && cachedTransform.position.x > -numBars * barSize) || (direction > 0 && cachedTransform.position.x < 0))
		{
			inMove = true;
			targetX = cachedTransform.position.x + (barSize * direction * 0.5f);

			tween = new Tween(cachedTransform.position.x, targetX, speed);
		}
	}
}