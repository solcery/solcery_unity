using System;

namespace Solcery.Modules
{
    [Serializable]
    public class LogStepData
    {
    	public int actionType; // always 0 for now
    	public int playerId;
    	public int data;
    }
}
