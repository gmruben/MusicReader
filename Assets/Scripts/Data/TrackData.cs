using System.Collections;
using System.Collections.Generic;

public class TrackData
{
	public InstrumentId instrumentId;
	public List<NoteData> noteDataList;

	public TrackData(InstrumentId instrumentId)
	{
		this.instrumentId = instrumentId;
	}
}