using UnityEngine;
using System.Json;
using System.Collections;
using System.Collections.Generic;

public class SongData
{
	public string id;
	public string name;
	
	public List<NoteData> noteList;
	public List<BarData2> barList;

	public static SongData parseJsonToSongData(string json)
	{
		SongData songData = new SongData();
		JsonObject jsonObject = JsonObject.Parse(json) as JsonObject;

		songData.id = jsonObject["id"].ToString().Replace ("\"", "");
		songData.name = jsonObject["name"].ToString().Replace ("\"", "");

		JsonArray jsonArray = JsonArray.Parse(jsonObject["noteList"].ToString()) as JsonArray;
		songData.noteList = jsonArrayToNoteList(jsonArray);

		return songData;
	}
	
	public static string parseSongDataToJson(SongData songData)
	{
		JsonObject jsonObject = new JsonObject();
		
		jsonObject.Add("id", songData.id);
		jsonObject.Add("name", songData.name);

		jsonObject.Add("noteList", noteListToJsonArray(songData.noteList));
		
		return jsonObject.ToString();
	}

	private static JsonArray noteListToJsonArray(List<NoteData> noteList)
	{
		JsonArray jsonArray = new JsonArray();
		foreach (NoteData noteData in noteList)
		{
			jsonArray.Add(noteDataToJsonObject(noteData));
		}
		return jsonArray;
	}

	private static List<NoteData> jsonArrayToNoteList(JsonArray jsonArray)
	{
		List<NoteData> noteList = new List<NoteData>();
		foreach (JsonObject jsonObject in jsonArray)
		{
			noteList.Add(jsonObjectToNoteData(jsonObject));
		}
		return noteList;
	}

	private static JsonObject noteDataToJsonObject(NoteData noteData)
	{
		JsonObject jsonObject = new JsonObject();

		jsonObject.Add("duration", noteData.duration);
		jsonObject.Add("pitch", noteData.pitch.ToString());

		return jsonObject;
	}

	private static NoteData jsonObjectToNoteData(JsonObject jsonObject)
	{
		float duration = float.Parse(jsonObject["duration"].ToString());
		NotePitch pitch = NotePitch.ParseString(jsonObject["pitch"].ToString());

		NoteData noteData = new NoteData(pitch, duration);
		return noteData;
	}
}