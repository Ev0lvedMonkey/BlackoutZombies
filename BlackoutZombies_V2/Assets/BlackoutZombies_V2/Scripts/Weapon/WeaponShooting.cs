using UnityEngine;

public abstract class WeaponShooting : ResourcePrefab
{
    [SerializeField] protected Transform _firePoint;

    [Header("Config")]
    [SerializeField] protected WeaponConfig _weaponConfig;

    private float _fireCooldown;
    private int _bulletsCountInClip;
    private EventManager _eventManager;
    private HUD _hud;
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
        _hud = ServiceLocator.Current.Get<HUD>();
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
        _hud.UpdateBulletsCount(_bulletsCountInClip);
        _hud.ShowBulletsCountText();
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
            _hud.UpdateBulletsCount(_bulletsCountInClip);
        }
        else
            _fireCooldown -= Time.deltaTime;
    }

    private void BlockShoot() =>
        _canShoot = false;

    private void OpenShoot() =>
        _canShoot = true;
}
