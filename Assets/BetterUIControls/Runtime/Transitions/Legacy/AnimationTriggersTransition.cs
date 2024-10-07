using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Attributes.Runtime;
using Better.Attributes.Runtime.Misc;
using Better.Commons.Runtime.Extensions;
using Better.UIControls.Runtime.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Better.UIControls.Runtime.Transitions
{
    [Serializable]
    public class AnimationTriggersTransition : SelectableTransition<Animator>
    {
        [HideLabel] 
        [SerializeField] private AnimationTriggers _animationTriggers = new AnimationTriggers();

        public override Task PlayAsync(TransitionState transitionState, CancellationToken token)
        {
            var trigger = _animationTriggers.GetTrigger(transitionState);
            return TriggerAnimation(trigger);
        }

        private Task TriggerAnimation(string triggerName)
        {
            if (_target == null || !_target.isActiveAndEnabled || !_target.hasBoundPlayables || string.IsNullOrEmpty(triggerName))
                return Task.CompletedTask;

            _target.ResetTrigger(_animationTriggers.normalTrigger);
            _target.ResetTrigger(_animationTriggers.highlightedTrigger);
            _target.ResetTrigger(_animationTriggers.pressedTrigger);
            _target.ResetTrigger(_animationTriggers.selectedTrigger);
            _target.ResetTrigger(_animationTriggers.disabledTrigger);

            _target.SetTrigger(triggerName);
            
            //TODO: Add awaiting for trigger finish
            return Task.CompletedTask;
        }

        public override void PlayInstant(TransitionState transitionState)
        {
            var trigger = _animationTriggers.GetTrigger(transitionState);
            TriggerAnimation(trigger).Forget();
        }

#if UNITY_EDITOR
        
        [EditorButton]
        private void GenerateController()
        {
            if (_target == null || _target.runtimeAnimatorController == null)
            {
                var controller = GenerateSelectableAnimatorContoller(_animationTriggers, _target);
                if (_target != null)
                {
                    UnityEditor.Animations.AnimatorController.SetAnimatorController(_target, controller);
                }
            }
        }
        
        private static UnityEditor.Animations.AnimatorController GenerateSelectableAnimatorContoller(AnimationTriggers animationTriggers, Animator target)
        {
            if (target == null)
                return null;

            // Where should we create the controller?
            var path = GetSaveControllerPath(target);
            if (string.IsNullOrEmpty(path))
                return null;

            // figure out clip names
            var normalName = string.IsNullOrEmpty(animationTriggers.normalTrigger) ? "Normal" : animationTriggers.normalTrigger;
            var highlightedName = string.IsNullOrEmpty(animationTriggers.highlightedTrigger) ? "Highlighted" : animationTriggers.highlightedTrigger;
            var pressedName = string.IsNullOrEmpty(animationTriggers.pressedTrigger) ? "Pressed" : animationTriggers.pressedTrigger;
            var selectedName = string.IsNullOrEmpty(animationTriggers.selectedTrigger) ? "Selected" : animationTriggers.selectedTrigger;
            var disabledName = string.IsNullOrEmpty(animationTriggers.disabledTrigger) ? "Disabled" : animationTriggers.disabledTrigger;

            // Create controller and hook up transitions.
            var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(path);
            GenerateTriggerableTransition(normalName, controller);
            GenerateTriggerableTransition(highlightedName, controller);
            GenerateTriggerableTransition(pressedName, controller);
            GenerateTriggerableTransition(selectedName, controller);
            GenerateTriggerableTransition(disabledName, controller);

            UnityEditor.AssetDatabase.ImportAsset(path);

            return controller;
        }
        
        private static AnimationClip GenerateTriggerableTransition(string name, UnityEditor.Animations.AnimatorController controller)
        {
            // Create the clip
            var clip = UnityEditor.Animations.AnimatorController.AllocateAnimatorClip(name);
            UnityEditor.AssetDatabase.AddObjectToAsset(clip, controller);

            // Create a state in the animatior controller for this clip
            var state = controller.AddMotion(clip);

            // Add a transition property
            controller.AddParameter(name, AnimatorControllerParameterType.Trigger);

            // Add an any state transition
            var stateMachine = controller.layers[0].stateMachine;
            var transition = stateMachine.AddAnyStateTransition(state);
            transition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, name);
            return clip;
        }
        
        private static string GetSaveControllerPath(Component target)
        {
            var defaultName = target.gameObject.name;
            var message = $"Create a new animator for the game object '{defaultName}':";
            return UnityEditor.EditorUtility.SaveFilePanelInProject("New Animation Contoller", defaultName, "controller", message);
        }
        
#endif
    }
}