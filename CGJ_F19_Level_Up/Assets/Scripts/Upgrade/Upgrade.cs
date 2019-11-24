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
	public float accuracy = 0f;
	public int burstCount = 0;

	[Header("Projectile effects")]
	public float lifetime = 0f;
	public int damage = 0;
	public float projectileSpeed = 0f;

	public void ApplyUpgrade(Player player, GunController gunController)
	{
		//Player
		player.MoveSpeed += moveSpeed;
		player.Health += vitality;

		//GunController
		gunController.MsBetweenShots += MsBetweenShots;
		gunController.Accuracy += accuracy;
		gunController.MuzzleVelocity += projectileSpeed;
		gunController.BurstCount += burstCount;
		//TODO: Kickback

		//Projectile
		gunController.ProjStats.lifetime += lifetime;
		gunController.ProjStats.damage += damage;
		gunController.ProjStats.speed += projectileSpeed;
	}

	public string UpgradeText()
	{
		string result = title + "\n";

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
		if (accuracy != 0)
		{
			result += "Accuracy " + (accuracy > 0 ? $"+{accuracy}" : $"{accuracy}") + "\n";
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