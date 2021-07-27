using Solcery.Utils;
using System;
using UnityEngine;

namespace Solcery
{

    public class LogApplyer : Singleton<LogApplyer>
    {
        public BoardData ApplyLogStep(BoardData origin, LogStepData logStep)
        {
            // TODO: Proper action type parsing. Casting is just a particular type
            return CastCard(origin, logStep.playerId, logStep.cardId);
        }

        public BoardData CastCard(BoardData origin, int casterId, int cardId) 
        {
        	var ctx = new BrickRuntime.Context(origin, cardId, casterId);
        	var cardData = origin.GetCard(cardId);
        	var cardTypeData = origin.GetCardTypeById(cardData.CardType);
        	var brickTree = cardTypeData.BrickTree;
        	Debug.Log("CAST CARD");
        	BrickRuntime.Action.Run(brickTree.Genesis, ref ctx);
        	return ctx.boardData;
        	
        }
    }
}
