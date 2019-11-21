using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor
{
    private Vector3 _velocity;
    private Rigidbody _rigidbody;

    public PlayerMotor(Rigidbody rigidbody) => _rigidbody = rigidbody;

    public void SetVelocity(Vector3 velocity) => _velocity = velocity;

    public void Movement() => _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
    
    public void LookAt(Vector3 lookPoint)
    {
        var heightCorrectedPoint = new Vector3(lookPoint.x, _rigidbody.position.y, lookPoint.z);
        _rigidbody.transform.LookAt(heightCorrectedPoint);
    }
}
