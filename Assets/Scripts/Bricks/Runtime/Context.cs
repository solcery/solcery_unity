using System.Collections.Generic;
using System;

namespace Solcery.BrickRuntime
{
    public class Context
    {
        public CardData obj; // Stack of context objects
        public BoardData boardData; // TODO: BoardData passed to buildContext by reference, mutable?
        public OldGameContent gameContent;
        public int casterId;
        public Dictionary<String, int> vars;

        public Context(BoardData origin, OldGameContent gameContent, int cardId, int casterPlayerId)
        {
            boardData = origin;
            this.gameContent = gameContent;
            casterId = casterPlayerId;
            vars = new Dictionary<String, int>();
            obj = origin.GetCard(cardId);
        }
    }
}
