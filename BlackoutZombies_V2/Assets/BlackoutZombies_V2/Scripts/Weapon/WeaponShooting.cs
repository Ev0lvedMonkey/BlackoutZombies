using UnityEngine;

public abstract class WeaponShooting : ResourcePrefab
{
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected Transform _firePoint;
    [SerializeField, Range(8, 30)] protected int _bulletsCount;
    [SerializeField, Range(0.1f, 2f)] protected float _fireRate;

    private float _fireCooldown;
    private int _bulletsForThisGun;

    private const int LMB = 0;

    private void OnEnable()
    {
        DeafaultBulletsCount();
    }

    public abstract void Shoot();

    public void ReloadGun(bool isRevive = false)
    {
        _bulletsCount = _bulletsForThisGun;
    }

    public void Fire()
    {
        if (Input.GetMouseButton(LMB) && _fireCooldown <= 0 && _bulletsCount > 0)
        {
            Shoot();
            _bulletsCount--;
            _fireCooldown = _fireRate;
        }
        else
            _fireCooldown -= Time.deltaTime;
    }

    private void DeafaultBulletsCount()
    {
        _bulletsForThisGun = _bulletsCount;
    }



}
