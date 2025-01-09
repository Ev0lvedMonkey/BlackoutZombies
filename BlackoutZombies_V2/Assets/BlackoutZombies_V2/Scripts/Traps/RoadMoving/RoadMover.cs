using System.Collections.Generic;
using UnityEngine;

public class RoadMover : ResourcePrefab
{
    [SerializeField] private RoadPath _roadPath;

    private IEnumerator<Transform> _dotInPath;

    private const float _distanceOffset = 0.1f;
    private const float _movingSpeed = 5f;

    private void Start()
    {
        _dotInPath = _roadPath.GetNextPathPoint();
        _dotInPath.MoveNext();

        if (_dotInPath.Current == null)
            return;

        transform.position = _dotInPath.Current.position;
    }

    private void Update()
    {
        if (_dotInPath == null || _dotInPath.Current == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, _dotInPath.Current.position, Time.deltaTime * _movingSpeed);

        float suffitientOffset = (transform.position - _dotInPath.Current.position).sqrMagnitude;

        if (suffitientOffset < Mathf.Pow(_distanceOffset, 2))
        {
            _dotInPath.MoveNext();
            Vector3 direction = (_dotInPath.Current.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void SetRoadPath(RoadPath roadPath) =>
        _roadPath = roadPath;

}
