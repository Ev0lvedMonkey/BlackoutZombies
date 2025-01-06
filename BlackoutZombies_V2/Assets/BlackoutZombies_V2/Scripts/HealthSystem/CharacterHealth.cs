using UnityEngine;

public class CharacterHealth : AliveObject
{
    private EventManager _eventManager;

    public void Init(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    public void TakeHeal(int healPoint)
    {
        Health += healPoint;
        Debug.Log($"{gameObject.name} take heal {healPoint}, bros health now {Health}");
    }

    protected override void Die()
    {
        Instantiate(_aliveObjectConfig.DeadBodyPrefab, transform.position, transform.rotation);
        _eventManager.OnStopGame?.Invoke();
    }
}
