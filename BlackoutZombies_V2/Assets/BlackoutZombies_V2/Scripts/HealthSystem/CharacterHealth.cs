using UnityEngine;

public class CharacterHealth : AliveObject
{
    private EventManager _eventManager;
    private void Awake()
    {
        _eventManager = ServiceLocator.Current.Get<EventManager>();
    }


    public void TakeHeal(int healPoint)
    {
        Health += healPoint;
        Debug.Log($"{gameObject.name} take heal {healPoint}, bros health now {Health}");
    }

    protected override void Die()
    {
        base.Die();
        _eventManager.OnStopGame?.Invoke();
    }
}
