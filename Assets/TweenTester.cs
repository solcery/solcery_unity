using DG.Tweening;
using UnityEngine;

public class TweenTester : MonoBehaviour
{
    public Transform Body;
    public Transform Target1;
    public Transform Target2;

    private Tweener _tweener;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var destination1 = Target1.position;
            _tweener = Body.DOMove(destination1, 1f).SetEase(Ease.InOutSine);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            var destination2 = Target2.position;
            _tweener = Body.DOMove(destination2, 1f).SetEase(Ease.InOutSine);
            // _tweener?.ChangeEndValue(destination2, 1, true).SetEase(Ease.InOutSine);
        }
    }
}
