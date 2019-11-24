using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour
{
	[SerializeField] private UpgradeScreenManager upgradeManager;
	public string key;
	[SerializeField] private Text buttonText;
	[SerializeField] private Upgrade upgrade;
	[SerializeField] private bool hide = true;
	[SerializeField] private float slideTime = 0.5f;
	[SerializeField] private float pickedDelay = 0.5f;
	private float timePassed = 0f;
	private Vector3 visiblePosition;
	private Vector3 hiddenPosition;

	private RectTransform rectTransform;


	// Start is called before the first frame update
	void Start()
	{
		rectTransform = GetComponent<RectTransform>();

		visiblePosition = rectTransform.anchoredPosition;
		hiddenPosition = visiblePosition;
		hiddenPosition.y = rectTransform.rect.height;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(key))
		{
			PickUpgrade();
		}

		if (hide)
		{
			SlideOut();
		}
		else
		{
			SlideIn();
		}
	}

	public void SetButtonUpgrade(Upgrade newUpgrade)
	{
		upgrade = newUpgrade;
		UseUpgradeText();
	}

	private void UseUpgradeText()
	{
		buttonText.text = upgrade.UpgradeText();
	}

	public void PickUpgrade()
	{
		if(!hide)
		{
			GameMaster.Instance.player.AddUpgrade(upgrade);
			upgradeManager.HideAllButtons();
			timePassed -= pickedDelay;
		}
	}

	public void Hide()
	{
		timePassed = 0f;
		hide = true;
	}

	public void Reveal()
	{
		timePassed = 0f;
		hide = false;
	}

	public void SlideOut()
	{
		if (timePassed < slideTime && hide)
		{
			timePassed += Time.deltaTime;

			float normalizedValue = timePassed / slideTime;

			rectTransform.anchoredPosition = Vector3.Lerp(visiblePosition, hiddenPosition, normalizedValue);
		}
	}

	public void SlideIn()
	{
		if (timePassed < slideTime && !hide)
		{
			timePassed += Time.deltaTime;

			float normalizedValue = timePassed / slideTime;

			rectTransform.anchoredPosition = Vector3.Lerp(hiddenPosition, visiblePosition, normalizedValue);
		}
	}
}
