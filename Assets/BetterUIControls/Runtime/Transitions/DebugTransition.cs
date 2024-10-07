using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public class DebugTransition : SelectableTransition
    {
        private const string DefaultId = nameof(DebugTransition);

        [SerializeField] private string _id = DefaultId;

        public override Task PlayAsync(TransitionState transitionState, CancellationToken token)
        {
            var log = Prebuild("Play Async", transitionState);
            Log(log);
            return Task.CompletedTask;
        }

        public override void PlayInstant(TransitionState transitionState)
        {
            var log = Prebuild("Play Instant", transitionState);
            Log(log);
        }

        private StringBuilder Prebuild(string operationName, TransitionState transitionState)
        {
            var stringBuilder = new StringBuilder();
            return stringBuilder.Append(_id)
                .Append(" - ")
                .AppendLine(operationName)
                .AppendFieldLine(nameof(transitionState), transitionState);
        }

        private void Log(StringBuilder builder)
        {
            var message = builder.ToString();
            Debug.Log(message);
        }
    }
}