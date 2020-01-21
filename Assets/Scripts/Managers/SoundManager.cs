using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private static SoundManager _instance;
	public static SoundManager Instance {
		get {
			if (_instance == null) {
				Debug.LogError("No SoundManager Availiable");
			}
			return _instance;
		}
	}
	private AudioSource audioSource;

	[Header("Volume Controls")]
	[Range(0f,1f)]
	public float backgroundVolume = 0.7f;
	[Range(0f,1f)]
	public float sfxVolume = 1f;

	[Header("Gem Sounds")]
	public AudioClip gemSpawnSound;
	public AudioClip gemCollectSound;

	void Awake(){
		_instance = this;
	}
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayBackgroundSound(AudioClip clip, float bgVolume){
		
		if (clip != null) {
			audioSource.clip = clip;
			audioSource.volume = bgVolume;
			audioSource.loop = true;
			audioSource.Play();

		}
	}

}
