using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Solcery.Editor
{
    public static class EditorMenu
    {
        [MenuItem("Solcery/Play", false, -1)]
        static async UniTask Play()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/_Main.unity");
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("Solcery/Scene/Main", false, 21)]
        static async UniTask OpenMainScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/_Main.unity");
        }

        [MenuItem("Solcery/Scene/Menu", false, 22)]
        static async UniTask OpenMenuScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Menu.unity");
        }

        [MenuItem("Solcery/Scene/Farm", false, 23)]
        static async UniTask OpenFarmScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Farm.unity");
        }

        [MenuItem("Solcery/Scene/Create", false, 24)]
        static async UniTask OpenCreateScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Create.unity");
        }

        [MenuItem("Solcery/Scene/Play", false, 25)]
        static async UniTask OpenPlayScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Play.unity");
        }

        [MenuItem("Solcery/Scene/Test", false, 101)]
        static async UniTask OpenTestScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Test/Test.unity");
        }

        [MenuItem("Solcery/Scene/GUI Kit", false, 201)]
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
