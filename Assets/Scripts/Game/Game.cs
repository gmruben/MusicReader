using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	private const float startX = 262.5f;
	private const float sizeX = 1200.0f;

	public GameHUD gameHUD;
	//public GameBar gameBar;

	private int score;
	private int multiplier;
	private int multiplierCount;

	private Note currentNote;

	private SongData songData;
	private TrackData trackData;

	void Start()
	{
		if (GameConfig.songData == null)
		{
			UserSongDataStore.retrieveSongDataList();
			
			GameConfig.songData = (SongData) UserSongDataStore.retrieveFirstSongData();
			GameConfig.trackData = GameConfig.songData.trackList[0];
			
			Debug.Log(GameConfig.songData);
		}

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

		for (int i = 0; i < trackData.barList.Count; i++)
		{
			GameBar gameBar = (GameObject.Instantiate(EntityManager.instance.gameBarPrefab) as GameObject).GetComponent<GameBar>();

			gameBar.transform.position = new Vector3(startX + (sizeX * i), 0, 0);
			gameBar.init(i, trackData.barList[i].noteList);

			gameBar.onHitNote += onHitNote;
			gameBar.onMissNote += onMissNote;
		}
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