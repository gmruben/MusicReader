using UnityEngine;
using System.Collections;

using CSharpSynth.Synthesis;
using CSharpSynth.Banks;

public class MidiPlayer : MonoBehaviour
{
	private int bufferSize = 1024;
	private string bankFilePath = "GM Bank/_CustomBank";
	
	private int midiNote = 60;
	private int midiNoteVolume = 100;
	private int midiInstrument = 53;

	private float[] sampleBuffer;
	private float gain = 1f;
	private StreamSynthesizer midiStreamSynthesizer;

	private NoteData currentNoteData;
	private float nextTime = 0;

	void Awake ()
	{
		midiStreamSynthesizer = new StreamSynthesizer (44100, 2, bufferSize, 40);
		sampleBuffer = new float[midiStreamSynthesizer.BufferSize];		

		midiStreamSynthesizer.LoadBank (bankFilePath);
	}

	void Update()
	{
		if (currentNoteData != null)
		{
			if (Time.time > nextTime)
			{
				noteOff(currentNoteData.pitch.midiNote);
			}
		}
	}

	public void noteOn(NoteData noteData)
	{
		midiStreamSynthesizer.NoteOn (1, noteData.pitch.midiNote, midiNoteVolume, midiInstrument);

		currentNoteData = noteData;
		nextTime = Time.time + ((noteData.duration - 1) * (0.5f * 0.25f));
	}

	public void noteOff(int midiNote)
	{
		midiStreamSynthesizer.NoteOff (1, midiNote);
	}

	// See http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.OnAudioFilterRead.html for reference code
	//	If OnAudioFilterRead is implemented, Unity will insert a custom filter into the audio DSP chain.
	//
	//	The filter is inserted in the same order as the MonoBehaviour script is shown in the inspector. 	
	//	OnAudioFilterRead is called everytime a chunk of audio is routed thru the filter (this happens frequently, every ~20ms depending on the samplerate and platform). 
	//	The audio data is an array of floats ranging from [-1.0f;1.0f] and contains audio from the previous filter in the chain or the AudioClip on the AudioSource. 
	//	If this is the first filter in the chain and a clip isn't attached to the audio source this filter will be 'played'. 
	//	That way you can use the filter as the audio clip, procedurally generating audio.
	//
	//	If OnAudioFilterRead is implemented a VU meter will show up in the inspector showing the outgoing samples level. 
	//	The process time of the filter is also measured and the spent milliseconds will show up next to the VU Meter 
	//	(it turns red if the filter is taking up too much time, so the mixer will starv audio data). 
	//	Also note, that OnAudioFilterRead is called on a different thread from the main thread (namely the audio thread) 
	//	so calling into many Unity functions from this function is not allowed ( a warning will show up ). 	
	private void OnAudioFilterRead (float[] data, int channels)
	{
		//This uses the Unity specific float method we added to get the buffer
		midiStreamSynthesizer.GetNext (sampleBuffer);
			
		for (int i = 0; i < data.Length; i++) {
			data [i] = sampleBuffer [i] * gain;
		}
	}
}