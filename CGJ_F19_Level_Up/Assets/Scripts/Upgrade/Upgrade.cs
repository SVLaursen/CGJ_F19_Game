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
	public int MsBetweenShots = 0;
	public float fireSpread = 0f;
	public int burstCount = 0;

	[Header("Projectile effects")]
	public float lifetime = 0f;
	public int damage = 0;
	public float projectileSpeed = 0f;

	public void ApplyUpgrade(Player player, GunController gunController)
	{
		//Player
		player.MoveSpeed += moveSpeed;
		//TODO: Vitality

		//GunController
		gunController.MsBetweenShots += MsBetweenShots;
		//TODO: FireSpread
		gunController.MuzzleVelocity += projectileSpeed;
		gunController.BurstCount += burstCount;
		//TODO: Kickback

		//Projectile
		gunController.ProjStats.lifetime += lifetime;
		gunController.ProjStats.damage += damage;
		gunController.ProjStats.speed += projectileSpeed;
	}

	public string toString()
	{
		string result = "";

		//Player
		if (moveSpeed != 0)
		{
			result += "Movement speed " + (moveSpeed > 0 ? $"+{moveSpeed}" : $"{moveSpeed}") + "\n";
		}
		if (vitality != 0)
		{
			result += "Vitality " + (vitality > 0 ? $"+{vitality}" : $"{vitality}") + "\n";
		}

		//GunController
		if (MsBetweenShots != 0)
		{
			result += "Shot delay " + (MsBetweenShots > 0 ? $"+{MsBetweenShots}" : $"{MsBetweenShots}") + "\n";
		}
		if (fireSpread != 0)
		{
			result += "Fire spread " + (fireSpread > 0 ? $"+{fireSpread}" : $"{fireSpread}") + "\n";
		}
		if (burstCount != 0)
		{
			result += "Burst Count " + (burstCount > 0 ? $"+{burstCount}" : $"{burstCount}") + "\n";
		}

		//Projectile
		if (lifetime != 0)
		{
			result += "Projectile lifetime " + (lifetime > 0 ? $"+{lifetime}" : $"{lifetime}") + "\n";
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