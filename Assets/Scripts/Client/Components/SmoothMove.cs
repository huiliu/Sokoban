﻿using System.Collections;
using UnityEngine;

public class SmoothMove
    : MonoBehaviour
{
    protected float moveTime = 0.5f;
    public void MoveTo(Vector3 dst)
    {
        //Base.Log.Info("Move", "{0} -> {1}", this.transform.position.z, dst.z);
        this.StartCoroutine(this.DoSmoothMove(dst));
    }

    private float time = 0.0f;
    private IEnumerator DoSmoothMove(Vector3 dst)
    {
        float remainingDistance = (transform.localPosition - dst).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            time += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(this.transform.localPosition, dst, time / moveTime);
            remainingDistance = (transform.localPosition - dst).sqrMagnitude;
            yield return null;
        }

        time = 0.0f;
        MoveEnded();
    }

    protected virtual void MoveEnded()
    {

    }
}
