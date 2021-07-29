using Newtonsoft.Json;
using Solcery.Utils;
using Solcery.WebGL;

namespace Solcery.Modules
{
    public class LogActionCreator : Singleton<LogActionCreator>
    {
        public void Init() { }
        public void DeInit() { }

        public void CastCard(int cardId)
        {
            var casterId = Board.Instance.BoardData.Value.MyIndex + 1;
            var logStepData = new LogStepData()
            {
                actionType = 0,
                playerId = casterId,
                data = cardId,
            };

#if (UNITY_WEBGL && !UNITY_EDITOR)
            var logStepDataJson = JsonConvert.SerializeObject(logStepData);
            UnityToReact.Instance?.CallLogAction(logStepDataJson);
#else
            Log.Instance?.FakeCastCard(logStepData);
#endif
        }
    }
}
