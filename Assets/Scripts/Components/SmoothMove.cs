using System.Collections;
using UnityEngine;

public class SmoothMove
    : MonoBehaviour
{
    protected float moveTime = 1.0f;
    public void MoveTo(Vector3 dst)
    {
        this.StartCoroutine(this.DoSmoothMove(dst));
    }

    private IEnumerator DoSmoothMove(Vector3 dst)
    {
        float remainingDistance = (transform.position - dst).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, dst, (1 / moveTime) * Time.deltaTime);
            remainingDistance = (transform.position - dst).sqrMagnitude;
            yield return null;
        }

        MoveEnded();
    }

    protected virtual void MoveEnded()
    {

    }
}
