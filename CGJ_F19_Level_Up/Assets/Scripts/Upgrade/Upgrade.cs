using UnityEngine;
public enum UpgradeType { Positive, Negative, Neutral }

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
	public string title;
	public UpgradeType upgradeType;


	[Header("Player effects")]
	public float moveSpeed = 0f;
	public float vitality = 0f;

	[Header("Weapon effects")]
	public float fireRate = 0f;
	public float fireSpread = 0f;

	[Header("Projectile effects")]
	public float range = 0f;
	public float damage = 0f;
	public float projectileSpeed = 0f;

	public void ApplyUpgrade()
	{


	}
}