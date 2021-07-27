using System.Collections.Generic;
using System;

namespace Solcery
{

    namespace BrickRuntime {
		public class Context 
		{
			public List<BoardCardData> objects; // Stack of context objects
			public BoardData boardData; // TODO: BoardData passed to buildContext by reference, mutable?
			public int casterId;
			public Dictionary<String, int> vars;

			public Context(BoardData origin, int cardId, int casterId) {
				boardData = origin;
				casterId = casterId;
				vars = new Dictionary<String, int>(); 
				objects = new List<BoardCardData> { origin.GetCard(cardId) }; 
			}
		}
	}
}

