using Cysharp.Threading.Tasks;
using Solcery.Utils;
using Solcery.WebGL;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;

namespace Solcery.Modules.Log
{
    public class Log : Singleton<Log>
    {
        public AsyncReactiveProperty<LogData> LogData => _logData;
        private AsyncReactiveProperty<LogData> _logData = new AsyncReactiveProperty<LogData>(null);

        public LogData testLog;

        public void CastCard(int playerId, int cardId) {// TODO: delete
            
            testLog.Steps.Add(new LogStepData {
                actionType = 0, // always 0 for now
                playerId = playerId,
                cardId = cardId,
            });

            ReactToUnity.Instance.UpdateLog(JsonConvert.SerializeObject(testLog));
        }

        public void Init()
        {
            testLog = new LogData() {
                Steps = new List<LogStepData>(),
            };
        }

        public void DeInit()
        {

        }
    }
}
