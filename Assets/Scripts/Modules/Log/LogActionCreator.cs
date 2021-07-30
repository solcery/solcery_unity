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
            //TODO: use real id
            var myId = Board.Instance.BoardData.Value.MyIndex + 1;
            // var myId = Board.Instance.BoardData.Value.Me.PlayerId;
            var castStep = new LogStepData(0, myId, cardId);

            SendLogAction(castStep);
        }

        public void DeclareVictory(int playerId)
        {
            SetOutcome(playerId, PlayerOutcome.Victory);
            SetStatus(playerId, PlayerStatus.Offline);
        }

        public void DeclareDefeat(int playerId)
        {
            SetOutcome(playerId, PlayerOutcome.Defeat);
            SetStatus(playerId, PlayerStatus.Offline);
        }

        public void SetStatus(int playerId, PlayerStatus status)
        {
            var statusStep = new LogStepData(1, playerId, (int)status);
            SendLogAction(statusStep);
        }

        public void SetOutcome(int playerId, PlayerOutcome outcome)
        {
            var outcomeStep = new LogStepData(2, playerId, (int)outcome);
            SendLogAction(outcomeStep);
        }

        private void SendLogAction(LogStepData logStepData)
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
            var logStepDataJson = JsonConvert.SerializeObject(logStepData);
            UnityToReact.Instance?.CallLogAction(logStepDataJson);
#else
            Log.Instance?.FakeLogAction(logStepData);
#endif
        }
    }
}
