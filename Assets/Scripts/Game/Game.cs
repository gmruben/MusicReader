using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public GameHUD gameHUD;
	public GameBar gameBar;

	private int score;
	private int multiplier;

	private Note currentNote;

	void Start()
	{
		init ();
	}

	private void init()
	{
		score = 0;
		multiplier = 1;

		gameHUD.init();

		//Load song data
		UserSongDataStore.retrieveSongDataList();
		SongData songData = UserSongDataStore.retrieveSongData("Song1");

		gameBar.init(songData);

	}
}