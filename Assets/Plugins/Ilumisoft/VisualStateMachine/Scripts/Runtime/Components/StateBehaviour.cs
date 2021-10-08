#pragma warning disable 1998
namespace Ilumisoft.VisualStateMachine
{
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Base class to create custom behaviours for states
    /// </summary>
    public abstract class StateBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected StateMachine stateMachine;

        State state = null;

        /// <summary>
        /// ID of the state this behaviour is belonging to
        /// </summary>
        public abstract string StateID { get; }

        /// <summary>
        /// The State Machine owning the state this behaviour is belonging to
        /// </summary>
        public StateMachine StateMachine { get => this.stateMachine; set => this.stateMachine = value; }

        protected CancellationTokenSource _stateCTS;
        private UnityAction _onEnterAciton;
        private UnityAction _onExitAciton;

        protected virtual void Awake()
        {
            if (StateMachine != null)
            {
                // Get the state
                state = StateMachine.Graph.GetState(StateID);

                // Add listeners to enter, exit and update events of the state
                if (state != null)
                {
                    _onEnterAciton = UniTask.UnityAction(async () => { await OnEnterState(); });
                    _onExitAciton = UniTask.UnityAction(async () => { await OnExitState(); });

                    // state.OnEnterState.AddListener(OnEnterState);
                    state.OnEnterState.AddListener(_onEnterAciton);
                    state.OnExitState.AddListener(_onExitAciton);
                    state.OnUpdateState.AddListener(OnUpdateState);
                }
                else
                {
                    Debug.Log($"Could not find state with the id '{StateID}'", this);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            _stateCTS?.Dispose();

            // Stop listening to state events when the behaviour gets destroyed
            if (StateMachine != null && state != null)
            {
                state.OnEnterState.RemoveListener(_onEnterAciton);
                state.OnExitState.RemoveListener(_onExitAciton);
                state.OnUpdateState.RemoveListener(OnUpdateState);
            }
        }

        /// <summary>
        /// Automatically tries to assign a state machine, when the component gets created or reset
        /// </summary>
        private void Reset()
        {
            if (StateMachine == null)
            {
                StateMachine = GetComponentInParent<StateMachine>();
            }
        }

        /// <summary>
        /// Returns true if the state is the currently active one
        /// </summary>
        public bool IsActiveState => StateMachine.CurrentState == StateID;


        /// <summary>
        /// Callback invoked when the state is entered
        /// </summary>
        protected virtual async UniTask OnEnterState()
        {
            if (_stateCTS != null && !_stateCTS.IsCancellationRequested)
            {
                _stateCTS?.Cancel();
                _stateCTS?.Dispose();
            }

            _stateCTS = new CancellationTokenSource();
        }

        /// <summary>
        /// Callback invoked when the state is exit
        /// </summary>
        protected virtual async UniTask OnExitState()
        {
            if (_stateCTS != null && !_stateCTS.IsCancellationRequested)
            {
                _stateCTS?.Cancel();
                _stateCTS?.Dispose();
            }
        }

        /// <summary>
        /// Callback invoked when the state is active and updated
        /// </summary>
        protected virtual void OnUpdateState() { }
    }
}
#pragma warning restore 1998