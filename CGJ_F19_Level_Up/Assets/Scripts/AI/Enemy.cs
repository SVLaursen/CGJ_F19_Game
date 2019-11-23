using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Cinemachine;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity {

	public enum State {Idle, Chasing, Attacking};
	private State _currentState;

	[Header("Cool FX!")]
	[SerializeField] private ParticleSystem deathEffect;
	[SerializeField] private CameraShaker.Properties shakeEffect;

	private NavMeshAgent _pathfinder;
	private LivingEntity _targetEntity;
	private Transform _target;

	[Header("Attack Settings")]
	[SerializeField] private float attackDistanceThreshold = .5f;
	[SerializeField] private float timeBetweenAttacks = 1;
	[SerializeField] private int damage = 1;

	private float nextAttackTime;
	private float myCollisionRadius;
	private float targetCollisionRadius;

	private bool hasTarget;
	private AiController _controller;
	private CinemachineImpulseSource _shaker;

	private void Awake() {
		_pathfinder = GetComponent<NavMeshAgent> ();
		
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			hasTarget = true;
			
			_target = GameObject.FindGameObjectWithTag ("Player").transform;
			_targetEntity = _target.GetComponent<LivingEntity> ();
			
			myCollisionRadius = GetComponent<CapsuleCollider> ().radius;
			targetCollisionRadius = _target.GetComponent<CapsuleCollider> ().radius;

			_controller = FindObjectOfType<AiController>();
		}
	}
	
	protected override void Start () {
		base.Start ();

		_shaker = GetComponent<CinemachineImpulseSource>();

		if (!hasTarget) return;
		_currentState = State.Chasing;
		_targetEntity.OnDeath += OnTargetDeath;

		StartCoroutine (UpdatePath ());
	}

	public void SetCharacteristics(float moveSpeed, int damageIncrease, int enemyHealth) {
		_pathfinder.speed = moveSpeed;

		if (hasTarget)
			damage += damageIncrease;
		
		health = enemyHealth;;
	}

	public override void TakeHit (int damage, Vector3 hitPoint, Vector3 hitDirection)
	{
		if (damage >= health) {
			GameMaster.Instance.PlayerScore += 1;
			CameraController.Instance.Shaker.StartShake(shakeEffect);
			Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
		}
		base.TakeHit (damage, hitPoint, hitDirection);
	}

	private void OnTargetDeath() {
		hasTarget = false;
		_currentState = State.Idle;
		_controller.DeactivateEnemy(this);
	}

	private void Update ()
	{
		if (!hasTarget) return;
		if (!(Time.time > nextAttackTime)) return;
		
		float sqrDstToTarget = (_target.position - transform.position).sqrMagnitude;
		
		if (sqrDstToTarget < Mathf.Pow (attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2)) {
			nextAttackTime = Time.time + timeBetweenAttacks;
			StartCoroutine (Attack ());
		}
	}

	private IEnumerator Attack() {

		_currentState = State.Attacking;
		_pathfinder.enabled = false;

		var originalPosition = transform.position;
		var dirToTarget = (_target.position - transform.position).normalized;
		var attackPosition = _target.position - dirToTarget * (myCollisionRadius);

		var attackSpeed = 3f;
		var percent = 0f;
		var hasAppliedDamage = false;

		while (percent <= 1) {

			if (percent >= .5f && !hasAppliedDamage) {
				hasAppliedDamage = true;
				CameraController.Instance.PlayerHitEffect();
				_targetEntity.TakeDamage(damage);
				_shaker.GenerateImpulse();
			}

			percent += Time.deltaTime * attackSpeed;
			
			float interpolation = (-Mathf.Pow(percent,2) + percent) * 4;
			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

			yield return null;
		}

		_currentState = State.Chasing;
		_pathfinder.enabled = true;
	}

	private IEnumerator UpdatePath()
	{
		const float refreshRate = .25f;

		while (hasTarget) {
			if (_currentState == State.Chasing)
			{
				var dirToTarget = (_target.position - transform.position).normalized;
				var targetPosition = _target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
				
				if (!_isDead)
					_pathfinder.SetDestination (targetPosition);
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}
}