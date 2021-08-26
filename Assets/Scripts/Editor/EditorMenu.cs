using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Solcery.Editor
{
    public static class EditorMenu
    {
        [MenuItem("Solcery/NodeEditor", false, -1)]
        static async UniTask NodeEditor()
        {
            await StopPlayingAndOpenScene("Assets/NodeEditor/NodeEditor.unity");
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("Solcery/Play", false, -1)]
        static async UniTask Play()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Play.unity");
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("Solcery/Create", false, -1)]
        static async UniTask Create()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Create.unity");
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("Solcery/Scene/NodeEditor", false, 0)]
        static async UniTask OpenNodeEditorScene()
        {
            await StopPlayingAndOpenScene("Assets/NodeEditor/NodeEditor.unity");
        }

        [MenuItem("Solcery/Scene/Play", false, 1)]
        static async UniTask OpenPlayScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Play.unity");
        }

        [MenuItem("Solcery/Scene/Lobby", false, 2)]
        static async UniTask OpenLobbyScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Lobby.unity");
        }

        [MenuItem("Solcery/Scene/Game", false, 3)]
        static async UniTask OpenGameScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Game.unity");
        }

        [MenuItem("Solcery/Scene/Create", false, 101)]
        static async UniTask OpenCreateScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Create.unity");
        }

        [MenuItem("Solcery/Scene/Test", false, 201)]
        static async UniTask OpenTestScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Test/Test.unity");
        }

        [MenuItem("Solcery/Scene/GUI Kit", false, 301)]
        static async UniTask OpenGUIKitScene()
        {
            await StopPlayingAndOpenScene("Assets/GUI Kit - Dark Geo/Scenes/DemoScene.unity");
        }

        static async UniTask StopPlayingAndOpenScene(string scenePath)
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.ExitPlaymode();
                await UniTask.WaitUntil(() => !EditorApplication.isPlaying && !EditorApplication.isCompiling);
                await UniTask.Yield();
            }

            EditorSceneManager.OpenScene(scenePath);
        }
    }
}
