namespace Ilumisoft.Plugins.StartupManager.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Data))]
    [CanEditMultipleObjects]
    public class DataEditor : Editor
    {
        private const string InfoMessage = "All added prefabs are automatically instantiated and marked as DontDestroyOnLoad when the application is started.";

        private Scrollview scrollview = new Scrollview();

        private PrefabList prefabList;

        private void OnEnable()
        {
            prefabList = new PrefabList(serializedObject, serializedObject.FindProperty("prefabs"));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            scrollview.OnGUILayout(() =>
            {
                OnHelpBoxGUI();
                OnListGUI();
            });

            serializedObject.ApplyModifiedProperties();
        }
        private static void OnHelpBoxGUI()
        {
            GUILayout.Space(8);

            EditorGUILayout.HelpBox(InfoMessage, MessageType.Info, true);
        }

        private void OnListGUI()
        {
            GUILayout.Space(12);

            prefabList.OnGuiLayout();

            GUILayout.Space(20);
        }
    }
}