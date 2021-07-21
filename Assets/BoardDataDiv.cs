using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solcery
{
    public class BoardDataDiv
    {
        public BoardData CurrentBoardData;
        public List<BoardDataCardChangedPlace> CardsThatChangedPlaces;
        public Dictionary<CardPlace, CardPlaceDiv> CardPlaceDivs;

        public BoardDataDiv(BoardData currentBoardData, List<BoardDataCardChangedPlace> cardsThatChangedPlaces, Dictionary<CardPlace, CardPlaceDiv> cardPlaceDivs)
        {
            CurrentBoardData = currentBoardData;
            CardsThatChangedPlaces = cardsThatChangedPlaces;
            CardPlaceDivs = cardPlaceDivs;
        }
    }
}
