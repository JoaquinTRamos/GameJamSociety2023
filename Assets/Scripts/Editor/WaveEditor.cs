using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor
{
    [CustomPropertyDrawer(typeof(Wave))]
    public class WaveDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();


            // Create property fields.
            var title = new Label("Next Wave");
            var fireField = new PropertyField(property.FindPropertyRelative("fire"));
            var waterField = new PropertyField(property.FindPropertyRelative("water"));
            var earthField = new PropertyField(property.FindPropertyRelative("earth"));
            var lightningField = new PropertyField(property.FindPropertyRelative("lightning"));
            var windField = new PropertyField(property.FindPropertyRelative("wind"));

            // Add fields to the container.
            container.Add(title);
            container.Add(fireField);
            container.Add(waterField);
            container.Add(earthField);
            container.Add(lightningField);
            container.Add(windField);

            return container;
        }
    }
}