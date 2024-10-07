using Better.UIControls.Runtime;
using UnityEditor;

namespace Better.UIControls.EditorAddons
{
    [CustomEditor(typeof(ExtendedToggle))]
    public class ExtendedToggleEditor : ExtendedControlEditor
    {
        private readonly string[] _customProperties = new[] { ScriptPropertyName, InteractablePropertyName, NavigationPropertyName, TransitionPropertyName, "_toggleTransition"};

        protected override string[] CustomProperties => _customProperties;
    }
}