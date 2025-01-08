using System.Collections.Generic;
using UnityEngine;

public class SpawnDotsManager : MonoBehaviour
{
    [SerializeField] private List<SpawnDot> _spawnPoints;
    [SerializeField]private CameraTargetTracker _visibilityChecker;

    private void Update()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            bool isVisible = _visibilityChecker.IsPointVisible(spawnPoint.transform.position);
            spawnPoint.IsActive = !isVisible;
        }
    }

}
