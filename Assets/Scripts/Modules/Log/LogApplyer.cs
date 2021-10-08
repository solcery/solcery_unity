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
            var gameContent = OldGame.Instance?.GameContent?.Value;

            if (currentLog == null || gameContent == null)
                return origin;

            var newBoardData = JsonConvert.DeserializeObject<BoardData>(JsonConvert.SerializeObject(origin)).Prettify(); //Cloning via JSON
            ApplyLog(newBoardData, currentLog, gameContent);
            return newBoardData.Prettify();
        }

        private void OnLogUpdate(LogData logData)
        {
            if (logData == null)
                return;

            var gameContent = OldGame.Instance?.GameContent?.Value;

            if (gameContent == null)
                return;

            var currentBoardData = Board.Instance?.BoardData?.Value;
            if (currentBoardData == null || currentBoardData.Players == null || currentBoardData.Players.Count < 2)
                return;

            var newBoardData = JsonConvert.DeserializeObject<BoardData>(JsonConvert.SerializeObject(currentBoardData)).Prettify(); //Cloning via JSON

            ApplyLog(newBoardData, logData, gameContent);
            Board.Instance?.UpdateBoard(newBoardData.Prettify());
        }

        private void ApplyLog(BoardData origin, LogData log, OldGameContent gameContent)
        {
            // TODO: Proper action type parsing. Casting is just a particular type
            //return CastCard(ref origin, logStep.playerId, logStep.cardId);
            var currentStep = origin.Step;
            for (int i = origin.Step; i < log.Steps.Count; i++)
            {
                ApplyLogStep(origin, log.Steps[i], gameContent);
            }
            origin.Step = log.Steps.Count;
            // return origin;
        }

        private void ApplyLogStep(BoardData origin, LogStepData logStep, OldGameContent gameContent)
        {
            origin.Prettify();

            switch (logStep.actionType)
            {
                case 0:
                    CastCard(origin, gameContent, logStep.playerId, logStep.data);
                    break;
                case 1:
                    SetStatus(origin, gameContent, logStep.playerId, logStep.data);
                    break;
                case 2:
                    SetOutcome(origin, gameContent, logStep.playerId, logStep.data);
                    break;
            }
        }

        private void CastCard(BoardData origin, OldGameContent gameContent, int casterId, int cardId)
        {
            var ctx = new Solcery.BrickRuntime.Context(origin, gameContent, cardId, casterId);
            var cardData = origin.GetCard(cardId);
            var cardTypeData = gameContent.GetCardTypeById(cardData.CardType);
            var brickTree = cardTypeData.BrickTree;
            BrickRuntime.Action.Run(brickTree.Genesis, ref ctx);
            origin = ctx.boardData;
        }

        private void SetStatus(BoardData origin, OldGameContent gameContent, int playerId, int status)
        {
            var playerData = origin.Players[playerId - 1];
            playerData.Status = (PlayerStatus)status;
        }

        private void SetOutcome(BoardData origin, OldGameContent gameContent, int playerId, int outcome)
        {
            if (origin != null && origin.Players != null && origin.Players.Count >= playerId)
            {
                var playerData = origin.Players[playerId - 1];
                playerData.Outcome = (PlayerOutcome)outcome;
            }
        }
    }
}
