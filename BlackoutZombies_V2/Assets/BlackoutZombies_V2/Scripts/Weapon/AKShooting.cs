public class AKShooting : WeaponShooting
{
    public override void Shoot()
    {
        Instantiate(_weaponConfig.BulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
