using UnityEngine;

public class ShotgunShooting : WeaponShooting
{
    private readonly float[] angleOffsets = new float[] { 8f, 0f, -8f };

    public override void Shoot()
    {
        for (int i = 0; i < 3; i++)
            Instantiate(_weaponConfig.BulletPrefab, _firePoint.position, Quaternion.Euler(_firePoint.rotation.eulerAngles.x,
                _firePoint.rotation.eulerAngles.y, _firePoint.rotation.eulerAngles.z + angleOffsets[i % angleOffsets.Length]));
    }
}
