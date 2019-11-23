using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenManager : MonoBehaviour
{
	public UpgradeButton[] buttons = new UpgradeButton[3];
	public UpgradeSet upgradeSet;

	public void SetupButtons(UpgradeSet newUpgrades)
	{
		upgradeSet = newUpgrades;

		int i = 0;
		foreach (UpgradeButton button in buttons)
		{
			Debug.Log(upgradeSet.upgrades[i].UpgradeText());
			button.SetButtonUpgrade(upgradeSet.upgrades[i]);
			i++;
		}
	}

	public void HideAllButtons()
	{
		foreach (UpgradeButton button in buttons)
		{
			button.Hide();
		}
	}

	public void ShowAllButtons()
	{
		foreach (UpgradeButton button in buttons)
		{
			button.Reveal();
		}
	}
}
