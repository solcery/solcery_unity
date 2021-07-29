using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.Modules.Log
{
    public class Log : Singleton<Log>
    {
        public AsyncReactiveProperty<LogData> LogData => _logData;
        private AsyncReactiveProperty<LogData> _logData = new AsyncReactiveProperty<LogData>(null);

        [SerializeField] private bool initWithTestJson = false;
        [ShowIf("initWithTestJson")] [Multiline(20)] [SerializeField] private string initJson;

        public void FakeCastCard(int playerId, int cardId)
        {
            Debug.Log("Log.FakeCastCard");

            var newLogData = new LogData(_logData.Value);

            newLogData.Steps.Add(new LogStepData
            {
                actionType = 0, // always 0 for now
                playerId = playerId,
                cardId = cardId,
            });

            UpdateLog(newLogData);
        }

        public void UpdateLog(LogData logData)
        {
            Debug.Log("Log.UpdateLog");
            _logData.Value = logData;
        }

        public void Init()
        {
            // InitWithJson();
        }

        public void DeInit()
        {
            _logData = null;
        }

        private void InitWithJson()
        {
            if (initWithTestJson)
            {
                var logData = JsonConvert.DeserializeObject<LogData>(initJson);
                UpdateLog(logData);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var logData = JsonConvert.DeserializeObject<LogData>(initJson);
                UpdateLog(logData);
            }
        }
    }
}
