using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class UserSongDataStore
{
	public static Dictionary<string, SongData> songDataList;

	public static void retrieveSongDataList()
	{
		songDataList = new Dictionary<string, SongData>();
		string path = Application.persistentDataPath + "/SongData";

		DirectoryInfo directory = new DirectoryInfo(path);
		FileInfo[] fileInfo = directory.GetFiles("*.songData");

		foreach (FileInfo file in fileInfo) 
		{
			using(FileStream fs = new FileStream(file.FullName, FileMode.Open))
			{
				BinaryReader fileReader = new BinaryReader(fs);
				
				SongData songData = SongData.parseJsonToSongData(fileReader.ReadString());
				songDataList.Add(songData.id, songData);

				fs.Close();
			}
		}
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
		FileStream fs;

		string dirpath = Application.persistentDataPath + "/SongData";
		string filepath = dirpath + "/" + songData.id + ".songData";

		if (!File.Exists(filepath))
		{
			if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
			fs = File.Create(filepath);
		}
		else
		{
			fs = new FileStream(filepath, FileMode.Create);
		}

		BinaryWriter fileWriter = new BinaryWriter(fs);
		
		fileWriter.Write(SongData.parseSongDataToJson(songData));
		fs.Close();
	}
}