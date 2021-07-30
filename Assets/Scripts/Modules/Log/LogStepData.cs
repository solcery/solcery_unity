using System;

namespace Solcery.Modules
{
    [Serializable]
    public class LogStepData
    {
        public int actionType; // always 0 for now
        public int playerId;
        public int data;

        public LogStepData(int actionType, int playerId, int data)
        {
            this.actionType = actionType;
            this.playerId = playerId;
            this.data = data;
        }
    }
}
