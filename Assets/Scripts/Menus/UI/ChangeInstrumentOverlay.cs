using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using CSharpSynth.Synthesis;
using CSharpSynth.Banks;

public class ChangeInstrumentOverlay : MonoBehaviour
{
	public UIButton okButton;
	public UIButton cancelButton;

	private int bufferSize = 1024;
	private string bankFilePath = "GM Bank/_CustomBank";
	private StreamSynthesizer midiStreamSynthesizer;

	public void init()
	{
		midiStreamSynthesizer = new StreamSynthesizer (44100, 2, bufferSize, 40);
		midiStreamSynthesizer.LoadBank (bankFilePath);

		for (int i = 0; i < BankManager.Banks[0].SampleNameList.Count; i++)
		{
			Debug.Log("INSTRUMENT: " + BankManager.Banks[0].SampleNameList[i]);
		}
	}
}