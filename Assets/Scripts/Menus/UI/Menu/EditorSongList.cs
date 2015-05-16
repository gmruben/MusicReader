using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EditorSongList : MonoBehaviour
{
	private const float buttonSizeY = 75.0f;

	public event Action<SongData> onSongClick;

	public SongDataButton songButton;

	public void Init()
	{
		UserSongDataStore.retrieveSongDataList();

		int index = 0;
		foreach(KeyValuePair<string, SongData> pair in UserSongDataStore.songDataList)
		{
			SongDataButton button = (GameObject.Instantiate(songButton.gameObject) as GameObject).GetComponent<SongDataButton>();

			button.transform.SetParent(songButton.transform.parent);
			button.transform.localPosition = songButton.transform.localPosition.AddY(-buttonSizeY * index);

			button.setText(pair.Value.name);
			button.setData(pair.Value);

			button.onClick += OnSongButtonClick;

			index++;
		}

		GameObject.Destroy(songButton.gameObject);
	}

	private void OnSongButtonClick(SongData songData)
	{
		if (onSongClick != null) onSongClick(songData);
	}
}