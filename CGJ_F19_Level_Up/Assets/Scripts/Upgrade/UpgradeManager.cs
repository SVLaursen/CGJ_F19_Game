using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UpgradeSet
{
	public List<Upgrade> upgrades;

	public UpgradeSet(Upgrade u1, Upgrade u2, Upgrade u3)
	{
		upgrades = new List<Upgrade>();
		upgrades.Add(u1);
		upgrades.Add(u2);
		upgrades.Add(u3);

		UpgradeManager.ShuffleUpgrades(this);
	}
}

public class UpgradeManager : MonoBehaviour
{
	[SerializeField] private List<Upgrade> positiveUpgrades = new List<Upgrade>();
	[SerializeField] private List<Upgrade> neutralUpgrades = new List<Upgrade>();
	[SerializeField] private List<Upgrade> negativeUpgrades = new List<Upgrade>();
	[SerializeField] private UpgradeScreenManager upgradeScreen;

	// Start is called before the first frame update
	void Start()
	{
		LoadResources();
		MakeUpgradesAvailable();
	}

	private void LoadResources()
	{
		Object[] temp = Resources.LoadAll("Upgrades", typeof(Upgrade));

		foreach (Object o in temp)
		{
			Upgrade u = (Upgrade)o;

			switch (u.upgradeType)
			{
				case UpgradeType.Positive:
					positiveUpgrades.Add(u);
					break;
				case UpgradeType.Neutral:
					neutralUpgrades.Add(u);
					break;
				case UpgradeType.Negative:
					negativeUpgrades.Add(u);
					break;
			}
		}
	}

	private Upgrade GetPositiveUpgrade()
	{
		int random = Random.Range(0, positiveUpgrades.Count);
		return positiveUpgrades[random];
	}

	private Upgrade GetNeutralUpgrade()
	{
		int random = Random.Range(0, neutralUpgrades.Count);
		return neutralUpgrades[random];
	}

	private Upgrade GetNegativeUpgrades()
	{
		int random = Random.Range(0, negativeUpgrades.Count);
		return negativeUpgrades[random];
	}

	public static void ShuffleUpgrades(UpgradeSet upgradeSet)
	{
		for (int i = 0; i < upgradeSet.upgrades.Count; i++)
		{
			Upgrade temp = upgradeSet.upgrades[i];
			int randomIndex = Random.Range(i, upgradeSet.upgrades.Count);
			upgradeSet.upgrades[i] = upgradeSet.upgrades[randomIndex];
			upgradeSet.upgrades[randomIndex] = temp;
		}
	}

	private UpgradeSet GetUpgradeSet()
	{
		UpgradeSet result = new UpgradeSet(GetPositiveUpgrade(), GetNeutralUpgrade(), GetNegativeUpgrades());

		return result;
	}

	public void MakeUpgradesAvailable()
	{
		upgradeScreen.SetupButtons(GetUpgradeSet());
	}
}
