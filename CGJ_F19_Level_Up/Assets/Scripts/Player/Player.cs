using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : LivingEntity
{
    [SerializeField] private float moveSpeed = 5;
	[SerializeField] private float moveSpeedDefault = 5;


	private Camera _viewCamera;
    private PlayerMotor _motor;
    GunController gunController;
	
    protected override void Start () {
        base.Start ();
        
        _motor = new PlayerMotor(GetComponent<Rigidbody>());
        gunController = GetComponent<GunController> ();
        _viewCamera = Camera.main;
    }

    private void Update () {
        // Movement input
        var moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
        var moveVelocity = moveInput.normalized * moveSpeed;
        _motor.SetVelocity(moveVelocity);

        // Look input
        var ray = _viewCamera.ScreenPointToRay (Input.mousePosition);
        var groundPlane = new Plane (Vector3.up, Vector3.up * gunController.ShootingHeight);

        if (groundPlane.Raycast(ray,out var rayDistance)) {
            var point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point,Color.red);
            _motor.LookAt(point);
        }

        // Weapon input
        if (Input.GetMouseButton(0))
            gunController.OnTriggerHold();
        
        if (Input.GetMouseButtonUp(0))
            gunController.OnTriggerRelease();
    }
    private void FixedUpdate() => _motor.Movement();
}
