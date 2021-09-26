using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NFTLoader : MonoBehaviour
{
    [SerializeField] private string link = null;
    [SerializeField] private RawImage rawImage = null;

    async UniTask Start()
    {
        var texture = ((DownloadHandlerTexture)(await UnityWebRequestTexture.GetTexture(link).SendWebRequest()).downloadHandler).texture;
        rawImage.texture = texture;
    }
}
