using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class LogData
    {
        public List<LogStepData> Steps;

        public LogData()
        {

        }

        public LogData(LogData origin)
        {
            if (origin == null)
            {
                Steps = new List<LogStepData>();
            }
            else
            {
                Steps = origin.Steps;
            }
        }
    }
}
