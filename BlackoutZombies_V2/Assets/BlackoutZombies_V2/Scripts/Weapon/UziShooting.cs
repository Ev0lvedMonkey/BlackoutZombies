using UnityEngine;

public class UziShooting : WeaponShooting
{
    private const float OffsetAngle = 9f;

    public override void Shoot()
    {
        Instantiate(_weaponConfig.BulletPrefab, _firePoint.position, Quaternion.Euler(_firePoint.rotation.eulerAngles.x,
            _firePoint.rotation.eulerAngles.y, _firePoint.rotation.eulerAngles.z + Random.Range(-OffsetAngle, OffsetAngle)));
    }
}
