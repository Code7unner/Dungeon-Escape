using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopKeeper : MonoBehaviour {

	public GameObject m_UICanvas;
	public String[] itemName;
	public int[] itemCost;
	private Player m_player;
	private int m_gemCount;
	private int currentSellection;
	private int currentItemCost;

	void Awake(){
		if (m_UICanvas.activeSelf) {
			m_UICanvas.SetActive(false);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		//Turn Canvas On and UpDate Gems
		m_player = other.gameObject.GetComponent<Player>();

		if (m_player != null) {
			m_gemCount = m_player.Diamonds;
			m_UICanvas.SetActive(true);
			UIManager.Instance.UI_UpDateGems(UIManager.Instance.playerGemCountText, m_gemCount);
			//Disable player's attack ability
			m_player.canAttack = false;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		//Turn Canvas Off
		m_player = other.gameObject.GetComponent<Player>();

		if (m_player != null) {
			m_UICanvas.SetActive(false);
			//Enable player's attack ability
			m_player.canAttack = true;
		}
	}

	public void SelectItem(int item){
		//Switch between selected item
		switch (item) {
			
			case 0: //Flame Sword
				currentSellection = 0;
				UIManager.Instance.UI_UpdateSelectedItem(-36.9f);
				break;

			case 1: //Boots
				currentSellection = 1;
				UIManager.Instance.UI_UpdateSelectedItem(-138.9f);
				break;

			case 2: //Key
				currentSellection = 2;
				UIManager.Instance.UI_UpdateSelectedItem(-243.3f);
				break;
		}
	}

	public void BuyItem(){
		//BuyItem method
		//check if player gems is greater then or equal to itemCost
		if (m_gemCount >= itemCost[currentSellection]) {
			//Get Handle to Player Script
			m_player = FindObjectOfType<Player>().GetComponent<Player>();
			//Subtract currentItemCost from gems
			m_player.Diamonds -= itemCost[currentSellection];
			//reset gemCount variable
			m_gemCount = m_player.Diamonds;
			//update UI Gem Count
			UIManager.Instance.UI_UpDateGems(UIManager.Instance.playerGemCountText, m_gemCount);
			//update HUD Gem Display
			UIManager.Instance.UI_UpDateGems(UIManager.Instance.hudGemCountText, m_gemCount);
			//Switch items on currentSellection
			//Award item to players inventory
			switch (currentSellection) {
				
				case 0: //Flame Sword
					GameManager.Instance.FlameSword = true;
					UIManager.Instance.EnableInventoryIcon(currentSellection);
					//Increase Damage amount
					break;

				case 1: //Boots Of Flight
					GameManager.Instance.BootsOfFlight = true;
					UIManager.Instance.EnableInventoryIcon(currentSellection);
					//Increase Jump Ability
					break;

				case 2: //Key To Castle
					GameManager.Instance.KeyToCastle = true;
					UIManager.Instance.EnableInventoryIcon(currentSellection);
					//Allow Win Condition
					break;
			}

			Debug.Log("Remaining gems: " + m_player.Diamonds);
			//Close Shopkeeper UI
			m_UICanvas.SetActive(false);
			//Enable player's attack ability
			m_player.canAttack = true;

		} 
		else {
			//Display error panel with error message
			UIManager.Instance.UI_ToggleErrorPanel();
			UIManager.Instance.errorText.text = "Not Enough Gems to Buy \n" + itemName[currentSellection].ToString();
			return;
		}

	}

	public void ClosePanel(){
		//Close error panel and Shopkeeper UI
		UIManager.Instance.UI_ToggleErrorPanel();
		m_UICanvas.SetActive(false);
		//Enable player's attack ability
		m_player.canAttack = true;
	}

}
