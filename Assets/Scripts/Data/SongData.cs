using UnityEngine;
using System;
using System.Json;
using System.Collections;
using System.Collections.Generic;

public class SongData
{
	public string id;
	public Key key;
	public string name;

	public List<TrackData> trackList = new List<TrackData>();

	public static SongData parseJsonToSongData(string json)
	{
		SongData songData = new SongData();
		JsonObject jsonObject = JsonObject.Parse(json) as JsonObject;

		songData.id = jsonValueToString(jsonObject["id"].ToString());
		songData.name = jsonValueToString(jsonObject["name"].ToString());

		//Check if it exists to make sure it works with previous versions
		if (jsonObject.ContainsKey("key")) songData.key = Key.ParseString(jsonObject["key"].ToString());
		songData.key = Key.C;

		JsonArray trackListJsonArray = JsonArray.Parse(jsonObject["trackList"].ToString()) as JsonArray;
		songData.trackList = jsonArrayToDataList<TrackData>(trackListJsonArray, jsonObjectToTrackData);

		return songData;
	}
	
	public static string parseSongDataToJson(SongData songData)
	{
		JsonObject jsonObject = new JsonObject();
		
		jsonObject.Add("id", songData.id);
		jsonObject.Add("name", songData.name);
		jsonObject.Add("key", songData.key.name);

		jsonObject.Add("trackList", dataListToJsonArray(songData.trackList, trackDataToJsonObject));
		
		return jsonObject.ToString();
	}

	private static JsonObject trackDataToJsonObject(TrackData trackData)
	{
		JsonObject jsonObject = new JsonObject();

		jsonObject.Add("id", trackData.id);
		jsonObject.Add("name", trackData.name);
		jsonObject.Add("instrumentId", trackData.instrumentId.ToString());

		jsonObject.Add("barList", dataListToJsonArray<BarData>(trackData.barList, barDataToJsonObject));
		
		return jsonObject;
	}

	private static TrackData jsonObjectToTrackData(JsonObject jsonObject)
	{	
		string id = jsonValueToString(jsonObject["id"].ToString());
		string name = jsonValueToString(jsonObject["name"].ToString());
		InstrumentId instrumentId = (InstrumentId) System.Enum.Parse(typeof(InstrumentId), jsonValueToString(jsonObject["instrumentId"].ToString()));

		TrackData trackData = new TrackData(id, name, instrumentId);

		//Add note list to track
		JsonArray jsonArray = JsonArray.Parse(jsonObject["barList"].ToString()) as JsonArray;
		trackData.barList = jsonArrayToDataList<BarData>(jsonArray, jsonObjectToBarData);

		return trackData;
	}

	private static JsonObject barDataToJsonObject(BarData barData)
	{
		JsonObject jsonObject = new JsonObject();

		jsonObject.Add("index", barData.index);
		jsonObject.Add("bpm", barData.beatsPerMinute);

		jsonObject.Add("noteList", dataListToJsonArray<NoteData>(barData.noteList, noteDataToJsonObject));
		
		return jsonObject;
	}
	
	private static BarData jsonObjectToBarData(JsonObject jsonObject)
	{	
		int index = int.Parse(jsonObject["index"].ToString());
		int beatsPerMinute = int.Parse(jsonObject["bpm"].ToString());

		BarData barData = new BarData(index, beatsPerMinute);

		JsonArray jsonArray = JsonArray.Parse(jsonObject["noteList"].ToString()) as JsonArray;
		barData.noteList = jsonArrayToDataList<NoteData>(jsonArray, jsonObjectToNoteData);
		
		return barData;
	}

	private static JsonObject noteDataToJsonObject(NoteData noteData)
	{
		JsonObject jsonObject = new JsonObject();
		
		jsonObject.Add("start", noteData.start);
		jsonObject.Add("duration", noteData.duration);
		jsonObject.Add("pitch", noteData.pitch.StringValue);
		
		return jsonObject;
	}
	
	private static NoteData jsonObjectToNoteData(JsonObject jsonObject)
	{
		string pitchString  = jsonValueToString(jsonObject["pitch"].ToString());
		
		int start = int.Parse(jsonObject["start"].ToString());
		int duration = int.Parse(jsonObject["duration"].ToString());
		NotePitch pitch = NotePitch.ParseString(pitchString);
		
		NoteData noteData = new NoteData(pitch, start, duration);
		return noteData;
	}

	/// <summary>
	/// Generic function that parses a list of objects of type T to a JsonArray.
	/// </summary>
	/// <returns>The JsonArray.</returns>
	/// <param name="dataList">The list of objects of type T.</param>
	/// <param name="parseDataToJsonFunc">Function that parses an object of type T to JsonObject.</param>
	/// <typeparam name="T">The type of the object to be parsed.</typeparam>
	private static JsonArray dataListToJsonArray<T>(List<T> dataList, System.Func<T, JsonObject> parseDataToJsonFunc)
	{
		JsonArray jsonArray = new JsonArray();
		foreach (T data in dataList)
		{
			jsonArray.Add(parseDataToJsonFunc(data));
		}
		return jsonArray;
	}

	/// <summary>
	/// Generic function that parses a JsonArray to a list of objects of type T.
	/// </summary>
	/// <returns>The list of object of type T.</returns>
	/// <param name="jsonArray">The JsonArray.</param>
	/// <param name="parseJsonToDataFunc">Function that parses a JsonObject to an object of type T.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	private static List<T> jsonArrayToDataList<T>(JsonArray jsonArray, System.Func<JsonObject, T> parseJsonToDataFunc)
	{
		List<T> dataList = new List<T>();
		foreach (JsonObject jsonObject in jsonArray)
		{
			dataList.Add(parseJsonToDataFunc(jsonObject));
		}
		return dataList;
	}

	/// <summary>
	/// It parses a json value to string, removing the slashes.
	/// </summary>
	/// <returns>The value to string.</returns>
	private static string jsonValueToString(string jsonString)
	{
		return jsonString.Replace ("\"", "");
	}
}