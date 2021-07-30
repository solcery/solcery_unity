using System;
using System.Collections.Generic;

namespace Solcery.Modules
{
    [Serializable]
    public class LogData
    {
        public List<LogStepData> Steps;

        public LogData()
        {

        }

        public LogData(LogStepData singleStep)
        {
            Steps = new List<LogStepData>();
            if (singleStep != null)
                Steps.Add(singleStep);
        }

        public LogData(List<LogStepData> steps)
        {
            if (steps == null)
                Steps = new List<LogStepData>();
            else
                Steps = steps;
        }

        public LogData(LogData origin)
        {
            if (origin == null)
                Steps = new List<LogStepData>();
            else
                Steps = origin.Steps;
        }
    }
}
