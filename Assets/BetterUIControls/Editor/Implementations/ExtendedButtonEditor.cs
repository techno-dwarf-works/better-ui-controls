using System.Linq;
using Better.Commons.EditorAddons.CustomEditors;
using Better.Commons.EditorAddons.Utility;
using Better.UIControls.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Better.UIControls.EditorAddons
{
    [CustomEditor(typeof(ExtendedButton))]
    public class ExtendedButtonEditor : MultiEditor
    {
        private const string TransitionPropertyName = "_transition";

        private static string[] _exclusionProperties = new[] { "m_OnClick", "m_Transition", "m_Colors", "m_TargetGraphic", "m_AnimationTriggers", "m_SpriteState", TransitionPropertyName };

        private VisualElement CreatePropertiesExcluding(SerializedObject obj, params string[] propertyToExclude)
        {
            var container = new VisualElement();
            var iterator = obj.GetIterator();
            var enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (propertyToExclude.Contains(iterator.name)) continue;
                var propertyField = new PropertyField(iterator);
                container.Add(propertyField);
            }
        
            return container;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetOverrideDefault(true);
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = VisualElementUtility.CreateVerticalGroup();

            IteratePreEditors(container);

            var imguiContainer = CreatePropertiesExcluding(serializedObject, _exclusionProperties);
            container.Add(imguiContainer);

            var property = serializedObject.FindProperty(TransitionPropertyName);
            var propertyField = new PropertyField(property);
            container.Add(propertyField);
            
            IteratePostEditors(container);
            container.TrackSerializedObjectValue(serializedObject, OnSerializedObjectTrack);
            return container;
        }
    }
}