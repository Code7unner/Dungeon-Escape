using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AcidEffect : MonoBehaviour {
	[SerializeField]
	[Tooltip("Speed, Float, The Speed the object travels")]
	private float m_speed = 5f;
	[SerializeField]
	[Tooltip("Destroy after Seconds, Float, Destroys the gameObject after n Seconds")]
	private float m_destroyAfterSec = 2.5f;
	[SerializeField]
	[Tooltip("Scale Over Time, Float, Scales the gameObject over Time")]
	private float m_scaleOverTime = 1f;
	[SerializeField]
	[Tooltip("UpScale, Vector3, Scales the gameObject Up over scaleOverTime")]
	private Vector3 m_upScale = new Vector3(1, 1, 1);


	void Start(){
		//scale acidEffect over time
		StartCoroutine(ScaleOverTime(m_destroyAfterSec));
		//destroy acidEffect after 5 sec
		Destroy(this.gameObject, m_scaleOverTime);
	}

	void Update(){
		
		//move constantly to the right at 3 meters per second
		transform.Translate(Vector3.right * m_speed * Time.deltaTime);

	}

	//detect player hit and apply damage (IDamagable interface)
	void OnTriggerEnter2D(Collider2D other){
		// if hit player
		if (other.gameObject.tag == "Player") {
			// get IDamagable contract
			IDamageable m_hit = other.GetComponent < IDamageable>();
			// if player has IDamagable call damage and destroy this gameobject
			if (m_hit != null) {
				Debug.Log("Hit: " + other.name);
				m_hit.Damage();
				Destroy(this.gameObject);
			}
		}
	}

	//Coroutine to Scale AcidEffect's transform over time
	IEnumerator ScaleOverTime(float time){
		//Variables for coroutine
		Vector3 m_originalScale = transform.localScale;
		Vector3 m_destinationScale = m_upScale;

		float m_currentTime = 0.0f;

		do {
			transform.localScale = Vector3.Lerp(m_originalScale, m_destinationScale, m_currentTime / time);

			m_currentTime += Time.deltaTime;
			yield return null;

		} while (m_currentTime <= time);
	}

}
