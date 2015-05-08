using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public GameHUD gameHUD;

	private int score;
	private int multiplier;

	void Start()
	{
		init ();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{

		}
	}

	private void init()
	{
		score = 0;
		multiplier = 1;

		gameHUD.init();
	}
}