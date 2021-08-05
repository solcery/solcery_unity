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
        [Multiline(20)] [SerializeField] private string json1;

        public void FakeLogAction(LogData logData)
        {
            var newLogData = new LogData(_logData.Value);
            newLogData.Steps.AddRange(logData.Steps);

            UpdateLog(newLogData);
        }

        public void UpdateLog(LogData logData)
        {
            _logData.Value = logData;
        }

        public void UpdateWithTestJson()
        {
            var logData = JsonConvert.DeserializeObject<LogData>(testJson);
            UpdateLog(logData);
        }

        public void UpdateWithJson1()
        {
            var logData = JsonConvert.DeserializeObject<LogData>(json1);
            UpdateLog(logData);
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
    }
}
