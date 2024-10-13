using Better.Commons.EditorAddons.CustomEditors;
using Better.Commons.EditorAddons.Utility;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Better.UIControls.EditorAddons
{
    public abstract class ExtendedControlEditor : MultiEditor
    {
        public const string TransitionPropertyName = "_transition";
        public const string ScriptPropertyName = "m_Script";
        public const string InteractablePropertyName = "m_Interactable";
        public const string NavigationPropertyName = "m_Navigation";

        protected abstract string[] CustomProperties { get; }

        private void FillContainer(VisualElement container, params string[] drawProperties)
        {
            foreach (var drawProperty in drawProperties)
            {
                var property = serializedObject.FindProperty(drawProperty);
                if (property == null) continue;

                var propertyField = CreateField(property);
                
                container.Add(propertyField);
            }
        }

        protected virtual VisualElement CreateField(SerializedProperty property)
        {
            var propertyField = new PropertyField(property);
            
            if (property.propertyPath == "m_Script")
            {
                propertyField.SetEnabled(false);
            }

            return propertyField;
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

            FillContainer(container, CustomProperties);

            IteratePostEditors(container);
            container.TrackSerializedObjectValue(serializedObject, OnSerializedObjectTrack);
            return container;
        }
    }
}