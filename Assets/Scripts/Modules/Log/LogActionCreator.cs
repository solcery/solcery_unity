using System.Collections.Generic;
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
            SendLogAction(new LogData(CastCardStep(cardId)));
        }

        public void LeaveGame(int playerId, bool hasOutcome = false, PlayerOutcome outcome = PlayerOutcome.Undefined)
        {
            var steps = new List<LogStepData>();

            if (hasOutcome)
                steps.Add(OutcomeStep(playerId, outcome));

            steps.Add(StatusStep(playerId, PlayerStatus.Offline));

            SendLogAction(new LogData(steps));
        }

        public static LogStepData CastCardStep(int cardId)
        {
            //TODO: use real id
            var myId = Board.Instance.BoardData.Value.MyIndex + 1; // var myId = Board.Instance.BoardData.Value.Me.PlayerId;
            return new LogStepData(0, myId, cardId);
        }

        public static LogStepData StatusStep(int playerId, PlayerStatus status)
        {
            return new LogStepData(1, playerId, (int)status);
        }

        public static LogStepData OutcomeStep(int playerId, PlayerOutcome outcome)
        {
            return new LogStepData(2, playerId, (int)outcome);
        }

        private void SendLogAction(LogData logData)
        {
            UnityToReact.Instance?.CallLogAction(logData);
        }
    }
}
