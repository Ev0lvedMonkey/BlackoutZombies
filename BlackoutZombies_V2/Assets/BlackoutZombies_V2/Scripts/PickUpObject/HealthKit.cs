using UnityEngine;

public class HealthKit : PickUpObject
{
    private const int HealPoints = 1;

    protected override void ReactToCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out CharacterHealth characterHealth))
            characterHealth.TakeHeal(HealPoints);
    }

}
