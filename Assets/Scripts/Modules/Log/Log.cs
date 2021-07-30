using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.Modules
{
    public class Log : Singleton<Log>
    {
        public AsyncReactiveProperty<LogData> LogData => _logData;
        private AsyncReactiveProperty<LogData> _logData = new AsyncReactiveProperty<LogData>(null);

        [SerializeField] private bool initWithTestJson = false;
        [ShowIf("initWithTestJson")] [Multiline(20)] [SerializeField] private string testJson;

        public void FakeLogAction(LogStepData logStepData)
        {
            Debug.Log("Log.FakeCastCard");

            var newLogData = new LogData(_logData.Value);
            newLogData.Steps.Add(logStepData);

            UpdateLog(newLogData);
        }

        public void UpdateLog(LogData logData)
        {
            Debug.Log("Log.UpdateLog");
            _logData.Value = logData;
        }

        public void Init()
        {
            InitWithJson();
        }

        public void DeInit()
        {
            _logData = null;
        }

        private void InitWithJson()
        {
            if (initWithTestJson)
            {
                var logData = JsonConvert.DeserializeObject<LogData>(testJson);
                UpdateLog(logData);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var logData = JsonConvert.DeserializeObject<LogData>(testJson);
                UpdateLog(logData);
            }
        }
    }
}
