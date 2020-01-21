using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public GameObject prefab;

	private GameObject playerPrefab;
	private Player m_player;
	// Use this for initialization
	void Start () {
		SpawnPlayer(prefab);
	}

	void Update(){
		
	}

	void SpawnPlayer(GameObject prefab){
		playerPrefab = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
		m_player = playerPrefab.GetComponent<Player>();
	}
}
