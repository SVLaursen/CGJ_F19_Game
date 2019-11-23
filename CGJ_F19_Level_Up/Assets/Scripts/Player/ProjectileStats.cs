using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "ScriptableObjects/ProjectileStats", order = 2)]
public class ProjectileStats : ScriptableObject
{
	public float speedDefault = 10f, speed = 10f, lifetimeDefault = 3f, lifetime = 3f;
	public int damageDefault = 1, damage = 1;

	public void SetStatsToDefault()
	{
		speed = speedDefault;
		damage = damageDefault;
		lifetime = lifetimeDefault;
	}
}
