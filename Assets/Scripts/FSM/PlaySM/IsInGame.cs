using System.Threading;
using Solcery.Modules;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.FSM.Play
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Play/Bools/IsInGame", fileName = "IsInGame")]
    public class IsInGame : Bool
    {
        private CancellationTokenSource _cts;
        private bool _isTracking;

        public override void Subscribe()
        {
            // Debug.Log("Subscribe");

            // if (!_isTracking)
            // {
                // _isTracking = true;
                _cts = new CancellationTokenSource();
                Reactives.Subscribe(Board.Instance?.BoardData, OnBoardUpdate, _cts.Token);
            // }
        }

        private void SetOutcome(bool value)
        {
            if (outcome == value)
                OnPassed?.Invoke();
        }

        private void OnBoardUpdate(BoardData boardData)
        {
            if (boardData == null)
            {
                SetOutcome(false);
            }
            else if (boardData.Players != null && boardData.Players.Count < 2)
            {
                SetOutcome(false);
            }
            else
            {
                SetOutcome(true);
            }
        }
    }
}
