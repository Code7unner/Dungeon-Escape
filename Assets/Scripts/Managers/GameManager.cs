using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public static GameManager Instance{

		get{
			if (_instance == null) {
				Debug.LogError("No GameManager Found");
			}
			return _instance;
		}
	}
	public bool FlameSword{ get; set;}
	public bool BootsOfFlight{ get; set;}
	public bool KeyToCastle{ get; set;}

	void Awake(){
		_instance = this;
	}

	void OnEnable(){
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
