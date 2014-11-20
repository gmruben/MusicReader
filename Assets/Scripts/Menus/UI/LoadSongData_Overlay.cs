using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadSongData_Overlay : MonoBehaviour
{
	public GameObject noSongDataTitle;
	public UIButton cancelButton;

	public void init()
	{
		List<SongData> songDataList = UserSongDataStore.retrieveSongDataList ();
		noSongDataTitle.gameObject.SetActive (songDataList.Count == 0);

		for (int i = 0; i < songDataList.Count; i++)
		{

		}
	}
}