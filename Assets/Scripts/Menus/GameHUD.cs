using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUD : UIMenu
{
	public Text scoreLabel;
	public Text multiplier;

	public MultiplierOrb[] multiplierOrbList;

	public void Init()
	{
		updateScore(0);
		updateMultiplier(1);
		updateMultiplierCount(0);
	}

	public void updateScore(int value)
	{
		scoreLabel.text = "SCORE: " + value;
	}

	public void updateMultiplier(int value)
	{
		multiplier.text = "MULTIPLIER: x" + value;
	}

	public void updateMultiplierCount(int value)
	{
		for (int i = 0; i < multiplierOrbList.Length; i++)
		{
			multiplierOrbList[i].SetOrbActive(i < value);
		}
	}
}