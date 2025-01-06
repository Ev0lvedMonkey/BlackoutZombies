using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [Header("Properties")]
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField, Range(8, 30)] protected int _bulletsCount;
    [SerializeField, Range(0.1f, 2f)] protected float _fireRate;

    public Bullet BulletPrefab => _bulletPrefab;
    public int BulletsCount => _bulletsCount;
    public float FireRate => _fireRate;
}
