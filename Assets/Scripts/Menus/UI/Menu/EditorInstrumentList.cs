using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EditorInstrumentList : MonoBehaviour
{
	private const float buttonSizeY = 75.0f;

	public event Action<string> onInstrumentClick;

	public StringDataButton instrumentButton;

	public void Init(SongData songData)
	{
		int index = 0;
		//foreach(InstrumentId instrumentId in Enum.GetValues(typeof(InstrumentId)))
		//{
		foreach(TrackData trackData in songData.trackList)
		{
			UIDataButton<string> button = (GameObject.Instantiate(instrumentButton.gameObject) as GameObject).GetComponent<UIDataButton<string>>();
			
			button.transform.SetParent(instrumentButton.transform.parent);
			button.transform.localPosition = instrumentButton.transform.localPosition.AddY(-buttonSizeY * index);

			button.setText(trackData.instrumentId.ToString());
			button.setData(trackData.instrumentId.ToString());

			//button.setText(instrumentId.ToString());
			//button.setData(instrumentId.ToString());
			
			button.onClick += OnInstrumentButtonClick;
			
			index++;
		}

		GameObject.Destroy(instrumentButton.gameObject);
	}

	private void OnInstrumentButtonClick(string instrumentId)
	{
		if (onInstrumentClick != null) onInstrumentClick(instrumentId);
	}
}