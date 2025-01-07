using UnityEngine;

public class BulletsKit : PickUpObject
{
    protected override void ReactToCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out WeaponShooting characterWeapon))
            characterWeapon.ReloadGun();
    }
}
