#pragma warning disable 1998
namespace Ilumisoft.VisualStateMachine
{
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Serialization;

    [System.Serializable]
    public class Transition
    {
        [SerializeField, FormerlySerializedAs("name")]
        private string id = string.Empty;

        [SerializeField]
        private string label = string.Empty;

        [SerializeField, FormerlySerializedAs("origin")]
        private string originID = string.Empty;

        [SerializeField, FormerlySerializedAs("target")]
        private string targetID = string.Empty;

        [SerializeField]
        private UnityEvent onEnterTransition = new UnityEvent();

        [SerializeField]
        private UnityEvent onExitTransition = new UnityEvent();

        [SerializeField, Min(0)]
        private float duration = 0.0f;

        [SerializeField]
        private TimeMode timeMode;

        /// <summary>
        /// Gets or sets the time mode of the transition duration (scaled vs unscaled)
        /// </summary>
        public TimeMode TimeMode
        {
            get => timeMode;
            set => timeMode = value;
        }

        /// <summary>
        /// Gets or sets the duration in seconds the transition takes to finish
        /// before the transition is triggered
        /// </summary>
        public float Duration
        {
            get => duration;
            set => duration = Mathf.Max(0, value);
        }

        /// <summary>
        /// The unique id of the transition
        /// </summary>
        public string ID
        {
            get => this.id;
            set => this.id = value;
        }

        /// <summary>
        /// The label of the transition. This does not need to be unique.
        /// </summary>
        public string Label
        {
            get => this.label;
            set => this.label = value;
        }

        // public UniTask EnterTransition;
        public Ilumisoft.VisualStateMachine.TransitionBehaviour behaviour;
        // public UniTask ExitTransition;

        /// <summary>
        /// The name of the origin state of the transition
        /// </summary>
        public string OriginID
        {
            get => originID;
            set => originID = value;
        }

        /// <summary>
        /// The name of the target state of the transition
        /// </summary>
        public string TargetID
        {
            get => targetID;
            set => targetID = value;
        }
    }
}
#pragma warning restore 1998
