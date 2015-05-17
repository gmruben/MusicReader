using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EditorTrackList : MonoBehaviour
{
	private const float buttonSizeY = 75.0f;
	
	public TrackButton trackButton;

	private SongData songData;
	private List<GameObject> buttonList;

	public void Init(SongData songData)
	{
		buttonList = new List<GameObject>();
		UpdateData(songData);
	}

	public void UpdateData(SongData songData)
	{
		this.songData = songData;

		//Remove all previous data
		RemoveData();

		float posY = 0;
		trackButton.gameObject.SetActive(true);

		foreach(TrackData trackData in songData.trackList)
		{
			TrackButton button = (GameObject.Instantiate(trackButton.gameObject) as GameObject).GetComponent<TrackButton>();
			
			button.transform.SetParent(trackButton.transform.parent);
			button.transform.localPosition = trackButton.transform.localPosition.AddY(posY);

			button.Init(trackData);

			button.OnSelect += OnTrackSelect;
			button.OnDelete += OnTrackDelete;
			
			posY -= buttonSizeY;
			buttonList.Add(button.gameObject);
		}

		trackButton.gameObject.SetActive(false);
	}

	private void RemoveData()
	{
		foreach (GameObject button in buttonList)
		{
			GameObject.Destroy(button);
		}
		buttonList.Clear();
	}

	private void OnTrackSelect(TrackData trackData)
	{
		GameConfig.songData = songData;
		GameConfig.trackData = trackData;

		Application.LoadLevel("Editor");
	}

	private void OnTrackDelete(TrackData trackData)
	{
		songData.trackList.Remove(trackData);
		UpdateData(songData);

		UserSongDataStore.storeSongData(songData);
	}
}