using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {
	
	private Player m_player;
	/// <summary>
	/// Shows the rewarded ad.
	/// </summary>

	void Awake(){
		m_player = FindObjectOfType<Player>().GetComponent<Player>();
	}

	public void ShowRewardedAd(){

		const string RewardPlacementId = "rewardedVideo";

		#if UNITY_ADS
		//Check if rewardedVideo is ready to play
		if (!Advertisement.IsReady(RewardPlacementId)) {
			Debug.Log("Advertisment not ready for reward placement");
			return;
		}

		Debug.Log("Showing rewarded ad");
		//options for callback handler
		var options = new ShowOptions();
		options.resultCallback = HandleShowResult;
		//show rewardedVideo
		Advertisement.Show(RewardPlacementId, options);
		#endif
	}

	public void ShowAdd(){
		
		const string DefaultPlacementId = "video";

		#if UNITY_ADS
		if(!Advertisement.IsReady(DefaultPlacementId)){
			Debug.Log("Advertisment not ready for default placement");
		}

		Advertisement.Show(DefaultPlacementId);
		#endif
	}

	#if UNITY_ADS
	/// <summary>
	/// Handles the show result.
	/// </summary>
	/// <param name="result">Result.</param>
	void HandleShowResult(ShowResult result){

		if (result == ShowResult.Finished) {
			Debug.Log("Video Complete - offer a reward to player");
			//Reward player code here
			m_player.AddDiamonds(100);
			//TODO: play reward sound
			//toggle shop error panel off
			UIManager.Instance.UI_ToggleErrorPanel();
			UIManager.Instance.UI_ActivateShopkeeperUI(false);

		}else if (result == ShowResult.Skipped) {
			Debug.LogWarning("Video was skipped - Do NOT reward the player");
		}else if (result == ShowResult.Failed) {
			Debug.LogError("Video Advertisment failed to show");
		}
	}
	#endif
}
