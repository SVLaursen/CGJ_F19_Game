using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    int Health { get; }

    void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection);

    void TakeDamage(int damage);

}
