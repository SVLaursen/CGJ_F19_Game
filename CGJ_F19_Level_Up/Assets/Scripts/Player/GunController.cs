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
	[SerializeField] private float accuracy = 0.8f;
	[SerializeField] private Vector2 kickMinMax = new Vector2(0.05f, 0.2f);
	[SerializeField] private ProjectileStats projectileStats;

	[SerializeField] private AudioClip[] bulletSounds;

	private AudioSource audioSource;

	private float _nextShotTime;
	private bool _triggerReleasedSinceLastShot;
	private int _shotsRemainingInBurst;

	public FireMode FiringMode { get; set; }
	public float MsBetweenShots { get; set; }
	public float MuzzleVelocity { get; set; }
	public float BurstCount { get; set; }
	public float Accuracy { get; set; }
	public Vector2 KickMinMax { get; set; }

	public ProjectileStats ProjStats { get; set; }

	public float ShootingHeight => shootOrigin.position.y;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void ShootSound()
	{
		AudioClip clip = GetRandomSound();
		audioSource.PlayOneShot(clip);
	}

	private AudioClip GetRandomSound()
	{
		return bulletSounds[UnityEngine.Random.Range(0, bulletSounds.Length)];
	}

	private void Start()
	{
		FiringMode = _startFiringMode;
		MsBetweenShots = msBetweenShots;
		MuzzleVelocity = muzzleVelocity;
		BurstCount = burstCount;
		Accuracy = accuracy;
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

			Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
			direction *= 1f - accuracy;
			direction += projectileSpawn[i].forward * accuracy;
			direction.y = 0;
			Quaternion rotation = Quaternion.LookRotation(direction);

			var newProjectile = Instantiate(projectile, projectileSpawn[i].position, rotation) as Projectile;
			newProjectile.SetSpeed(MuzzleVelocity);
			newProjectile.ApplyStats(ProjStats);
		}
		ShootSound();
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
		Accuracy = accuracy;
		KickMinMax = kickMinMax;

		ProjStats.SetStatsToDefault();
	}
}
