using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPath : MonoBehaviour
{
    [SerializeField] private Transform[] dotsCollection;

    private int currentDotIndex = 0;

    private void OnDrawGizmos()
    {
        if (dotsCollection == null || dotsCollection.Length < 2)
            return;

        for (int i = 1; i < dotsCollection.Length; i++)
            Gizmos.DrawLine(dotsCollection[i - 1].position, dotsCollection[i].position);
        Gizmos.DrawLine(dotsCollection[0].position, dotsCollection[dotsCollection.Length - 1].position);
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (dotsCollection == null || dotsCollection.Length < 1)
            yield break;
        while (true)
        {
            yield return dotsCollection[currentDotIndex];
            if (dotsCollection.Length == 1)
                continue;
            currentDotIndex++;

            if (currentDotIndex >= dotsCollection.Length)
                currentDotIndex = 0;
            if (currentDotIndex < 0)
                currentDotIndex = dotsCollection.Length - 1;
        }

    }
}

