public class AKShooting : WeaponShooting
{
    public override void Shoot()
    {
        Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
    }
}
