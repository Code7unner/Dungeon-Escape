using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Diamond : MonoBehaviour{

	//Value of Diamond
	public int m_gems = 1;
	//AudioClips for Swawn and Collect
	public AudioClip m_spawnSound, m_colletSound;
	public float m_volume = 1f;
	public float collectDelay = 0.15f;
	//Handle for the player script
	private Player m_player;
	//Handle for the AudioSource
	private AudioSource m_audioSource;

	void Awake(){
		//Get the AudioSource Component
		m_audioSource = GetComponent<AudioSource>();

	}

	void Start(){
		
		//Get the Player Script
		m_player = FindObjectOfType<Player>();

	}

	//Get number of diamond value from enemy script
	//OnTriggerEnter to collect
	void OnTriggerEnter2D(Collider2D other){
		//Check for player
		ICollectable hit = other.GetComponent<ICollectable>();

		//If obect that entered trigger has ICollectable and Player 
		if(hit != null && m_player != null){
			Debug.Log("Add Value of Diamond to player");
			//add the value of the diamond to the player using ICollectable interface
			m_player.AddDiamonds(m_gems);

			//Play collect Sound
			if (m_colletSound != null) {
				// play sound
				m_audioSource.PlayOneShot(m_colletSound, m_volume);
			}

			StartCoroutine(DiamondDestroyPause(collectDelay));

		}
	}

	public void PlaySpawnSound(){
		//Play Spawn Sound
		if(m_spawnSound != null){
			// play sound
			m_audioSource.PlayOneShot(m_spawnSound, m_volume);
		}
	}

	IEnumerator DiamondDestroyPause(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Destroy(this.gameObject);
	}
}
