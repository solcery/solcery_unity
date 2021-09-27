using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NFTLoader : MonoBehaviour
{
    [SerializeField] private string link1 = null;
    [SerializeField] private string link2 = null;
    [SerializeField] private Image image1 = null;
    [SerializeField] private Image image2 = null;

    async UniTask Start()
    {
        // var tasks = new List<UnityWebRequestAsyncOperation>();

        // var task1 = UnityWebRequestTexture.GetTexture(link1).SendWebRequest();
        // var task2 = UnityWebRequestTexture.GetTexture(link2).SendWebRequest();

        // tasks.Add(task1);
        // tasks.Add(task2);

        // await UniTask.WaitUntil(() => AllRequestsDone(tasks)).ContinueWith(() => { Debug.Log("both loaded"); });

        var req1 = UnityWebRequestTexture.GetTexture(link1);
        var req2 = UnityWebRequestTexture.GetTexture(link2);

        var task1 = GetSpriteAsync(req1, image1);
        var task2 = GetSpriteAsync(req2, image2);

        // await UniTask.WhenAll(task1, task2);
    }

    async UniTask GetSpriteAsync(UnityWebRequest req, Image image)
    {
        Debug.Log("start loading...");
        var op = await req.SendWebRequest();
        var tex = ((DownloadHandlerTexture)(op.downloadHandler)).texture;
        Debug.Log("finished loading...");

        var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = sprite;
    }
}
