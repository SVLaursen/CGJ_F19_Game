using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenManager : MonoBehaviour
{
	public UpgradeButton[] buttons = new UpgradeButton[3];
	public UpgradeSet upgradeSet;

	[SerializeField] private float timeToForcedPick = 3f;
	private float timePassed = 0f;
	private bool active;
	private bool ButtonsActive
	{
		get
		{
			return active;
		}
		set
		{
			active = value;
			timePassed = 0f;
		}
	}

	private void Update()
	{
		if (active)
		{
			if (timePassed > timeToForcedPick)
			{
				PickRandomUpgrade();
			}
			else
			{
				timePassed += Time.deltaTime;
			}
		}

	}

	public void SetupButtons(UpgradeSet newUpgrades)
	{
		if (ButtonsActive)
		{
			PickRandomUpgrade();
			ShowAllButtons();
		}

		upgradeSet = newUpgrades;

		int i = 0;
		foreach (UpgradeButton button in buttons)
		{
			button.SetButtonUpgrade(upgradeSet.upgrades[i]);
			i++;
		}

		ShowAllButtons();
	}

	public void HideAllButtons()
	{
		foreach (UpgradeButton button in buttons)
		{
			button.Hide();
		}

		ButtonsActive = false;
	}

	public void ShowAllButtons()
	{
		foreach (UpgradeButton button in buttons)
		{
			button.Reveal();
		}

		ButtonsActive = true;
	}

	public void PickRandomUpgrade()
	{
		buttons[Random.Range(0, buttons.Length)].PickUpgrade();
	}
}
