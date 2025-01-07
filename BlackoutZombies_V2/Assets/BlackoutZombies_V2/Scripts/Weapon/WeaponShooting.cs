using UnityEngine;

public abstract class WeaponShooting : ResourcePrefab
{
    [SerializeField] protected Transform _firePoint;

    [Header("Config")]
    [SerializeField] protected WeaponConfig _weaponConfig;

    private float _fireCooldown;
    private int _bulletsCountInClip;
    private EventManager _eventManager;
    private bool _canShoot;
    private const int LMB = 0;

    private void Awake()
    {
        _eventManager = ServiceLocator.Current.Get<EventManager>();
        _eventManager.OnStartGame += OpenShoot;
        _eventManager.OnResumeGame += OpenShoot;
        _eventManager.OnStopGame += BlockShoot;
        _eventManager.OnPauseGame += BlockShoot;
    }

    private void OnEnable()
    {
        ReloadGun();
    }

    private void Update()
    {
        Fire();
    }
    public abstract void Shoot();

    public void ReloadGun()
    {
        _bulletsCountInClip = _weaponConfig.BulletsCount;
    }

    public void Fire()
    {
        if (_canShoot == false)
            return;
        if (Input.GetMouseButton(LMB) && _fireCooldown <= 0 && _bulletsCountInClip > 0)
        {
            Shoot();
            _bulletsCountInClip--;
            _fireCooldown = _weaponConfig.FireRate;
        }
        else
            _fireCooldown -= Time.deltaTime;
    }

    private void BlockShoot() =>
        _canShoot = false;

    private void OpenShoot() =>
        _canShoot = true;
}
