using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Grimmz.Editor
{
    public class EditorMenu : MonoBehaviour
    {
        [MenuItem("Grimmz/Play", false, -1)]
        static async UniTask Play()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/_Main.unity");
            EditorApplication.EnterPlaymode();
        }

        [MenuItem("Grimmz/Scene/Main", false, 21)]
        static async UniTask OpenMainScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/_Main.unity");
        }

        [MenuItem("Grimmz/Scene/Menu", false, 22)]
        static async UniTask OpenMenuScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Menu.unity");
        }

        [MenuItem("Grimmz/Scene/Farm", false, 23)]
        static async UniTask OpenFarmScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Farm.unity");
        }

        [MenuItem("Grimmz/Scene/Create", false, 24)]
        static async UniTask OpenCreateScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Create.unity");
        }

        [MenuItem("Grimmz/Scene/Sandbox", false, 25)]
        static async UniTask OpenSandboxScene()
        {
            await StopPlayingAndOpenScene("Assets/Scenes/Sandbox.unity");
        }

        [MenuItem("Grimmz/Scene/GUI Kit", false, 101)]
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
