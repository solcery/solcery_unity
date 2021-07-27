using System;

namespace Solcery
{
    [Serializable]
    public class LogStepData
    {
    	public int actionType; // always 0 for now
    	public int playerId;
    	public int cardId;
    }
}
