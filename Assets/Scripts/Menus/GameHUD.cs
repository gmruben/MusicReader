using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUD : UIMenu
{
	public Text scoreLabel;
	public Text multiplier;

	public void init()
	{
		scoreLabel.text = "SCORE: 0";
		multiplier.text = "MULTIPLIER: x1";
	}
}