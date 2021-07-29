using Newtonsoft.Json;
using Solcery.Modules.Board;
using Solcery.Modules.Log;
using Solcery.WebGL;

namespace Solcery
{
    public static class LogActionCreator
    {
        public static void CastCard(int cardId)
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
