using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Positioner : MonoBehaviour
{
    public RectTransform originRect;
    public Canvas canvas;
    public GameObject card;
    public Transform cardTransform;
    public Animator cardAnimator;

    async UniTask Start()
    {
        // goalRect.transform.position = originRect.transform.position;
        // var start = goalRect.transform.position;
        var cardClone = Instantiate(card, canvas.transform, true);
        cardClone.GetComponent<Animator>().SetTrigger("Moving");
        await cardClone.transform.DOMove(originRect.transform.position, 1f);
        // await cardTransform.DOMove(start, 1f);
        // transform.DOMove(new Vector3(1,2,3), 1);
        // Debug.Log(goalRect.anchoredPosition);
    }
}
