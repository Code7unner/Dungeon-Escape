using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public float hitDelay = 0.5f;
	// variable to determine if the damage function can be called
	private bool m_canDamage = true;


	void OnTriggerEnter2D(Collider2D other){
		
		IDamageable hit = other.GetComponent < IDamageable>();

		if (hit != null) {
			// if can attack
			if (m_canDamage == true) {
				hit.Damage();
				m_canDamage = false;
				StartCoroutine(damagePause());
			}
		}
	}

	// Coroutine to switch variable back to true after 0.5 seconds
	IEnumerator damagePause(){
		
		yield return new WaitForSeconds(hitDelay);
		m_canDamage = true;
	}
}
