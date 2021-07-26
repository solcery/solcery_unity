using Cysharp.Threading.Tasks;
using Solcery.Utils;

namespace Solcery.Modules.Log
{
    public class Log : Singleton<Log>
    {
        public AsyncReactiveProperty<LogData> LogData => _logData;
        private AsyncReactiveProperty<LogData> _logData = new AsyncReactiveProperty<LogData>(null);

        public void Init()
        {
            
        }

        public void DeInit()
        {

        }
    }
}
