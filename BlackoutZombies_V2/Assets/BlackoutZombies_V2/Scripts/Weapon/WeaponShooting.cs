using UnityEngine;

public abstract class WeaponShooting : ResourcePrefab
{
    [SerializeField] protected Transform _firePoint;

    [Header("Config")]
    [SerializeField] protected WeaponConfig _weaponConfig;

    private float _fireCooldown;
    private int _bulletsCountInClip;

    private const int LMB = 0;

    private void OnEnable()
    {
        ReloadGun();
    }

    public abstract void Shoot();

    public void ReloadGun()
    {
        _bulletsCountInClip = _weaponConfig.BulletsCount;
    }

    private void Update()
    {
        Fire();
    }

    public void Fire()
    {
        if (Input.GetMouseButton(LMB) && _fireCooldown <= 0 && _bulletsCountInClip > 0)
        {
            Shoot();
            _bulletsCountInClip--;
            _fireCooldown = _weaponConfig.FireRate;
        }
        else
            _fireCooldown -= Time.deltaTime;
    }
}
