using System;
using UnityEngine;

public class PointLaserOrb : Projectile, IDamageable
{
    [SerializeField] private GameObject pointLaserPrefab;
    [SerializeField] private int cooldown;
    [SerializeField] private int maxLasers;
    [SerializeField] private float _size;
    private int _cooldown;
    private int _lasers;
    private new void Start()
    {
        base.Start();
        _cooldown = 0;
        _size = 0;
        _lasers = 0;
    }

    private void FixedUpdate()
    {
        _cooldown++;
    }

    private new void OnTriggerEnter2D(Collider2D other)
    {
        FirePointLaser(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        FirePointLaser(other);
    }

    private void FirePointLaser(Collider2D other)
    {
        if (_cooldown > cooldown)
        {
            GameObject pointLaser = Instantiate(pointLaserPrefab,transform.position,Quaternion.identity);
            Stretch(pointLaser, transform.position, other.transform.position, true);
            var enemy = other.gameObject.GetComponent<IDamageable>();
            if (enemy != null)
            {
                var dmg = new Damage
                {
                    RawDamage = damage,
                    Source = transform.position
                };
                enemy.TakeDamage(dmg);
            }
            else
            {
                Destroy(other.gameObject);
            }
            _lasers++;
            _size--;
            if (_lasers >= maxLasers)
            {
                _cooldown = 0;
                _lasers = 0;
            }
            var localScale = transform.localScale;
            localScale.x = 1 + (_size / 10);
            localScale.y = 1 + (_size / 10);
            transform.localScale = localScale;
        }
    }

    public void TakeDamage(Damage dmg)
    {
        if (_cooldown > cooldown)
        {
            switch (dmg.Type)
            {
                case CollisionType.energy:
                    _size += 2*dmg.RawDamage;
                    break;
                case CollisionType.kinetic:
                    _size--;
                    break;
                case CollisionType.pointlaser:
                    break;
                case CollisionType.ship:
                    break;
                default:
                    _size--;
                    break;
            }
        }
    }

    public bool DestroyProjectile(CollisionType type)
    {
        if (_cooldown > cooldown)
        {
            return true;
        }
        return false;
    }

    //https://answers.unity.com/questions/844792/unity-stretch-sprite-between-two-points-at-runtime.html
    public void Stretch(GameObject _sprite,Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ) {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(1,1,1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition);
        _sprite.transform.localScale = scale;
    }
}
