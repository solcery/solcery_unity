using Solcery.Utils;
using Newtonsoft.Json;
using System.Threading;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.Modules
{
    public class LogApplyer : Singleton<LogApplyer>
    {
        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            Reactives.Subscribe(Log.Instance.LogData, OnLogUpdate, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public BoardData ApplyCurrentLog(BoardData origin)
        {
            if (origin == null || origin.Players == null || origin.Players.Count < 2)
                return origin;

            var currentLog = Log.Instance?.LogData?.Value;

            if (currentLog == null)
                return origin;

            var newBoardData = JsonConvert.DeserializeObject<BoardData>(JsonConvert.SerializeObject(origin)).Prettify(); //Cloning via JSON
            ApplyLog(newBoardData, currentLog);

            return newBoardData.Prettify(isVirgin: true);
        }

        private void OnLogUpdate(LogData logData)
        {
            if (logData == null)
                return;

            var currentBoardData = Board.Instance?.BoardData?.Value;
            if (currentBoardData == null || currentBoardData.Players == null || currentBoardData.Players.Count < 2)
                return;

            var newBoardData = JsonConvert.DeserializeObject<BoardData>(JsonConvert.SerializeObject(currentBoardData)).Prettify(); //Cloning via JSON

            ApplyLog(newBoardData, logData);
            Board.Instance?.UpdateBoard(newBoardData.Prettify());
        }

        private void ApplyLog(BoardData origin, LogData log)
        {
            // TODO: Proper action type parsing. Casting is just a particular type
            //return CastCard(ref origin, logStep.playerId, logStep.cardId);
            var currentStep = origin.Step;
            for (int i = origin.Step; i < log.Steps.Count; i++)
            {
                ApplyLogStep(origin, log.Steps[i]);
            }
            origin.Step = log.Steps.Count;
            // return origin;
        }

        private void ApplyLogStep(BoardData origin, LogStepData logStep)
        {
            origin.Prettify();

            switch (logStep.actionType)
            {
                case 0:
                    CastCard(origin, logStep.playerId, logStep.data);
                    break;
                case 1:
                    SetStatus(origin, logStep.playerId, logStep.data);
                    break;
                case 2:
                    SetOutcome(origin, logStep.playerId, logStep.data);
                    break;
            }
        }

        private void CastCard(BoardData origin, int casterId, int cardId)
        {
            var ctx = new Solcery.BrickRuntime.Context(origin, cardId, casterId);
            var cardData = origin.GetCard(cardId);
            var cardTypeData = origin.GetCardTypeById(cardData.CardType);
            var brickTree = cardTypeData.BrickTree;
            BrickRuntime.Action.Run(brickTree.Genesis, ref ctx);
            origin = ctx.boardData;
        }

        private void SetStatus(BoardData origin, int playerId, int status)
        {
            var playerData = origin.Players[playerId - 1];
            playerData.Status = (PlayerStatus)status;
        }

        private void SetOutcome(BoardData origin, int playerId, int outcome)
        {
            var playerData = origin.Players[playerId - 1];
            playerData.Outcome = (PlayerOutcome)outcome;
        }
    }
}
