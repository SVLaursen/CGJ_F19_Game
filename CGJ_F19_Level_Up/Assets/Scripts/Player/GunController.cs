using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Vector3 shootPosition;
    [SerializeField] private FireMode _startFiringMode;

    [SerializeField] private Transform[] projectileSpawn;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float msBetweenShots = 100f;
    [SerializeField] private float muzzleVelocity = 35f;
    [SerializeField] private int burstCount;
    [SerializeField] private Vector2 kickMinMax = new Vector2(0.05f, 0.2f);

    private float _nextShotTime;
    private bool _triggerReleasedSinceLastShot;
    private int _shotsRemainingInBurst;

    public FireMode FiringMode { get; set; }

    public float ShootingHeight => shootPosition.y;

    private void Start()
    {
        FiringMode = _startFiringMode;
    }

    public void OnTriggerHold()
    {
        Shoot();
        _triggerReleasedSinceLastShot = false;
    }

    public void OnTriggerRelease()
    {
        _triggerReleasedSinceLastShot = true;
    }

    private void Shoot()
    {
        if (!(Time.time > _nextShotTime)) return;
        
        if (FiringMode == FireMode.Burst) {
            if (_shotsRemainingInBurst == 0) {
                return;
            }
            _shotsRemainingInBurst --;
        }
        else if (FiringMode == FireMode.Single) {
            if (!_triggerReleasedSinceLastShot) {
                return;
            }
        }

        for (var i = 0; i < projectileSpawn.Length; i ++) 
        {
            _nextShotTime = Time.time + msBetweenShots / 1000;
                
            var newProjectile = Instantiate (projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
            newProjectile.SetSpeed (muzzleVelocity);
        }

        transform.localPosition -= Vector3.forward * Random.Range(kickMinMax.x, kickMinMax.y);
    }    

    public enum FireMode
    {
        Auto,
        Burst,
        Single
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(shootPosition, Vector3.one);
    }
}
