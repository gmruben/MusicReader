using UnityEngine;
using System.Json;
using System.Collections;
using System.Collections.Generic;

public class SongData
{
	public string id;
	public string name;
	
	public List<NoteData> noteList = new List<NoteData>();
	public List<BarData2> barList = new List<BarData2>();

	public List<TrackData> trackList = new List<TrackData>();

	public static SongData parseJsonToSongData(string json)
	{
		SongData songData = new SongData();
		JsonObject jsonObject = JsonObject.Parse(json) as JsonObject;

		songData.id = jsonValueToString(jsonObject["id"].ToString());
		songData.name = jsonValueToString(jsonObject["name"].ToString());

		JsonArray jsonArray = JsonArray.Parse(jsonObject["noteList"].ToString()) as JsonArray;
		songData.noteList = jsonArrayToNoteList(jsonArray);

		JsonArray trackListJsonArray = JsonArray.Parse(jsonObject["trackList"].ToString()) as JsonArray;
		songData.trackList = jsonArrayToTrackList(trackListJsonArray);

		return songData;
	}
	
	public static string parseSongDataToJson(SongData songData)
	{
		JsonObject jsonObject = new JsonObject();
		
		jsonObject.Add("id", songData.id);
		jsonObject.Add("name", songData.name);

		jsonObject.Add("noteList", noteListToJsonArray(songData.noteList));
		jsonObject.Add("trackList", trackListToJsonArray(songData.trackList));
		
		return jsonObject.ToString();
	}

	private static JsonArray trackListToJsonArray(List<TrackData> trackList)
	{
		JsonArray jsonArray = new JsonArray();
		foreach (TrackData trackData in trackList)
		{
			jsonArray.Add(trackDataToJsonObject(trackData));
		}
		return jsonArray;
	}

	private static List<TrackData> jsonArrayToTrackList(JsonArray jsonArray)
	{
		List<TrackData> trackList = new List<TrackData>();
		foreach (JsonObject jsonObject in jsonArray)
		{
			trackList.Add(jsonObjectToTrackData(jsonObject));
		}
		return trackList;
	}

	private static JsonObject trackDataToJsonObject(TrackData trackData)
	{
		JsonObject jsonObject = new JsonObject();
		
		jsonObject.Add("instrumentId", trackData.instrumentId.ToString());
		
		return jsonObject;
	}

	private static TrackData jsonObjectToTrackData(JsonObject jsonObject)
	{	
		InstrumentId instrumentId = (InstrumentId) System.Enum.Parse(typeof(InstrumentId), jsonValueToString(jsonObject["instrumentId"].ToString()));
		
		TrackData trackData = new TrackData(instrumentId);
		return trackData;
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
	/// It parses a json value to string, removing the slashes.
	/// </summary>
	/// <returns>The value to string.</returns>
	private static string jsonValueToString(string jsonString)
	{
		return jsonString.Replace ("\"", "");
	}
}