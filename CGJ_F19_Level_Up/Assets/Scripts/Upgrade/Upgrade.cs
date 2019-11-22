using UnityEngine;
public enum UpgradeType { Positive, Negative, Neutral }

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
	public string title;
	public UpgradeType upgradeType;


	[Header("Player effects")]
	public float moveSpeed = 0f;
	public int vitality = 0;

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

	public string toString()
	{
		string result = "";

		if(moveSpeed != 0)
		{
			result += "Movement speed " + (moveSpeed > 0 ? $"+{moveSpeed}" : $"{moveSpeed}") + "\n";
		}
		if (vitality != 0)
		{
			result += "Vitality " + (vitality > 0 ? $"+{vitality}" : $"{vitality}") + "\n";
		}
		if (fireRate != 0)
		{
			result += "Fire rate " + (fireRate > 0 ? $"+{fireRate}" : $"{fireRate}") + "\n";
		}
		if (fireSpread != 0)
		{
			result += "Fire spread " + (fireSpread > 0 ? $"+{fireSpread}" : $"{fireSpread}") + "\n";
		}
		if (range != 0)
		{
			result += "Projectile range " + (range > 0 ? $"+{range}" : $"{range}") + "\n";
		}
		if (damage != 0)
		{
			result += "Projectile Damage " + (damage > 0 ? $"+{damage}" : $"{damage}") + "\n";
		}
		if (projectileSpeed != 0)
		{
			result += "Projectile Speed " + (projectileSpeed > 0 ? $"+{projectileSpeed}" : $"{projectileSpeed}") + "\n";
		}

		return result;
	}
}