using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;



public class Projectile : MonoBehaviour
{
	[SerializeField] private LayerMask collisionMask;
	[SerializeField] private CameraShaker.Properties shakeSettings;

	private float _speed = 10f;
	private int _damage = 1;

	private float _lifetime = 3f;
	private float _skinWidth = 0.1f;

	public void SetSpeed(float speed) => _speed = speed;
	private CinemachineImpulseSource _spawnShake;

	private void Start()
	{
		Destroy(gameObject, _lifetime);
		Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);

		if (initialCollisions.Length > 0)
			OnHitObject(initialCollisions[0], transform.position);

		CameraController.Instance.Shaker.StartShake(shakeSettings);
	}

	private void Update()
	{
		var moveDistance = _speed * Time.deltaTime;
		CheckCollisions(moveDistance);
		transform.Translate(Vector3.forward * moveDistance);
	}

	public void ApplyStats(ProjectileStats ps)
	{
		_speed = ps.speed;
		_damage = ps.damage;
		_lifetime = ps.lifetime;
	}

	private void OnHitObject(Collider coll, Vector3 hitPoint)
	{
		var damagable = coll.GetComponent<ITakeDamage>();

		if (damagable != null)
			damagable.TakeHit(_damage, hitPoint, transform.forward);

		Destroy(gameObject);
	}

	private void CheckCollisions(float moveDistance)
	{
		var ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance + _skinWidth, collisionMask, QueryTriggerInteraction.Collide))
			OnHitObject(hit.collider, hit.point);
	}
}