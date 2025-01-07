using UnityEngine;

public class Battery : PickUpObject
{
    protected override void ReactToCollision(Collider2D collider)
    {
       if(collider.TryGetComponent(out CharacterLightZone characterLightZone))
            characterLightZone.ReloadBattery();
    }
}
