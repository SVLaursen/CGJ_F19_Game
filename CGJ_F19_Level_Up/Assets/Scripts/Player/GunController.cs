using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunController : MonoBehaviour
{
	[SerializeField] private Transform shootOrigin;
	[SerializeField] private FireMode _startFiringMode;

	[SerializeField] private Transform[] projectileSpawn;
	[SerializeField] private Projectile projectile;
	[SerializeField] private float msBetweenShots = 100f;
	[SerializeField] private float muzzleVelocity = 35f;
	[SerializeField] private int burstCount;
	[SerializeField] private Vector2 kickMinMax = new Vector2(0.05f, 0.2f);
	[SerializeField] private ProjectileStats projectileStats;


	private float _nextShotTime;
	private bool _triggerReleasedSinceLastShot;
	private int _shotsRemainingInBurst;

	public FireMode FiringMode { get; set; }
	public float MsBetweenShots { get; set; }
	public float MuzzleVelocity { get; set; }
	public float BurstCount { get; set; }
	public Vector2 KickMinMax { get; set; }

	public ProjectileStats ProjStats { get; set; }

	public float ShootingHeight => shootOrigin.position.y;

	private void Start()
	{
		FiringMode = _startFiringMode;
		MsBetweenShots = msBetweenShots;
		MuzzleVelocity = muzzleVelocity;
		BurstCount = burstCount;
		KickMinMax = kickMinMax;
		ProjStats = projectileStats;
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

		if (FiringMode == FireMode.Burst)
		{
			if (_shotsRemainingInBurst == 0)
			{
				return;
			}
			_shotsRemainingInBurst--;
		}
		else if (FiringMode == FireMode.Single)
		{
			if (!_triggerReleasedSinceLastShot)
			{
				return;
			}
		}

		for (var i = 0; i < projectileSpawn.Length; i++)
		{
			_nextShotTime = Time.time + MsBetweenShots / 1000;

			var newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
			newProjectile.SetSpeed(MuzzleVelocity);
			newProjectile.ApplyStats(ProjStats);
		}

		transform.localPosition -= Vector3.forward * Random.Range(KickMinMax.x, KickMinMax.y);
	}

	public enum FireMode
	{
		Auto,
		Burst,
		Single
	}

	public void SetStatsToDefault()
	{
		MsBetweenShots = msBetweenShots;
		MuzzleVelocity = muzzleVelocity;
		BurstCount = burstCount;
		KickMinMax = kickMinMax;

		ProjStats.SetStatsToDefault();
	}
}
