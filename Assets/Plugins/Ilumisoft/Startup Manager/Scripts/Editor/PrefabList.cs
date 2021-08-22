namespace Ilumisoft.Plugins.StartupManager.Editor
{
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    public class PrefabList
    {
        private ReorderableList reorderableList;

        private SerializedProperty elements;

        public PrefabList(SerializedObject serializedObject, SerializedProperty elements)
        {
            this.elements = elements;

            this.reorderableList = new ReorderableList(serializedObject, elements, true, true, true, true);

            this.reorderableList.drawHeaderCallback = DrawHeaderCallback;
            this.reorderableList.drawElementCallback = DrawElementCallback;
        }

        private void DrawHeaderCallback(Rect rect)
        {
            GUI.Label(rect, "Prefabs");
        }

        private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            var element = elements.GetArrayElementAtIndex(index); ;

            EditorGUI.ObjectField(rect, element, GUIContent.none);
        }

        public void OnGuiLayout()
        {
            reorderableList.DoLayoutList();
        }
    }
}