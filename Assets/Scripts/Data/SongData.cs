using UnityEngine;
using System.Json;
using System.Collections;

public class SongData
{
	public string id;
	public string name;

	public static SongData parseJSonToSongData(string json)
	{
		SongData songData = new SongData();
		JsonObject jsonObject = JsonObject.Parse(json) as JsonObject;
		
		songData.id = jsonObject["id"].ToString();
		songData.name = jsonObject["name"].ToString();
		
		return songData;
	}
	
	public static string parseSongDataToJSon(SongData songData)
	{
		JsonObject jsonObject = new JsonObject();
		
		jsonObject.Add("id", songData.id);
		jsonObject.Add("name", songData.name);
		
		return jsonObject.ToString();
	}
}