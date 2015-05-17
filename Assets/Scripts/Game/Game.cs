using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public GameHUD gameHUD;
	public GameBar gameBar;

	private int score;
	private int multiplier;
	private int multiplierCount;

	private Note currentNote;

	private SongData songData;
	private TrackData trackData;

	void Start()
	{
		init ();
	}

	private void init()
	{
		score = 0;
		multiplier = 1;
		multiplierCount = 0;

		gameHUD.Init();

		songData = GameConfig.songData;
		trackData = GameConfig.trackData;

		gameBar.init(trackData.barList[0]);

		gameBar.onHitNote += onHitNote;
		gameBar.onMissNote += onMissNote;
	}

	private void onHitNote()
	{
		score += GameParameters.noteScore * multiplier;
		multiplierCount += 1;

		if (multiplierCount == GameParameters.multiplierUpValue)
		{
			multiplierCount = 0;
			multiplier += 1;
		}

		gameHUD.updateScore(score);
		gameHUD.updateMultiplier(multiplier);
		gameHUD.updateMultiplierCount(multiplierCount);
	}

	private void onMissNote()
	{
		multiplierCount = 0;
		multiplier = 1;

		gameHUD.updateMultiplier(multiplier);
		gameHUD.updateMultiplierCount(multiplierCount);
	}
}