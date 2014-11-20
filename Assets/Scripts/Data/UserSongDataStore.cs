using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class UserSongDataStore
{
	private static Dictionary<string, SongData> songDataList;

	public static List<SongData> retrieveSongDataList()
	{
		List<SongData> songDataList = new List<SongData> ();
		string path = Application.persistentDataPath + "/SongData";

		DirectoryInfo directory = new DirectoryInfo(path);
		FileInfo[] fileInfo = directory.GetFiles("*.songData");

		foreach (FileInfo file in fileInfo) 
		{
			using(FileStream fs = new FileStream(Application.persistentDataPath + "/gamesave.json", FileMode.Open))
			{
				BinaryReader fileReader = new BinaryReader(fs);
				
				SongData songData = SongData.parseJSonToSongData(fileReader.ReadString());
				songDataList.Add(songData);

				fs.Close();
			}
		}

		return songDataList;
	}

	public static SongData retrieveSongData(string id)
	{
		if (songDataList.ContainsKey (id))
		{
			return songDataList [id];
		}

		Debug.LogError ("COULDN'T FIND SONG DATA WITH ID: " + id);
		return null;
	}

	public static void storeSongData(SongData songData)
	{
		string path = Application.persistentDataPath + "/SongData/" + songData.id + ".songData";
		if (!File.Exists(path))
		{
			if (!Directory.Exists(Application.persistentDataPath + "/SongData")) Directory.CreateDirectory(Application.persistentDataPath + "/SongData");
			File.Create(path);
		}

		using(FileStream fs = new FileStream(Application.persistentDataPath + "/gamesave.json", FileMode.Create))
		{
			BinaryWriter fileWriter = new BinaryWriter(fs);
			
			fileWriter.Write(SongData.parseSongDataToJSon(songData));
			fs.Close();
		}
	}
}