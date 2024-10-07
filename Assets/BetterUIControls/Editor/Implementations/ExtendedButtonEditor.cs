using Better.UIControls.Runtime;
using UnityEditor;

namespace Better.UIControls.EditorAddons
{
    [CustomEditor(typeof(ExtendedButton))]
    public class ExtendedButtonEditor : ExtendedControlEditor
    {
        private readonly string[] _customProperties = new[] { ScriptPropertyName, InteractablePropertyName, NavigationPropertyName, TransitionPropertyName, };

        protected override string[] CustomProperties => _customProperties;
    }
}