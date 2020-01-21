using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable {

	public int Health{ get; set;}

	// Used for Initilization
	public override void Init()
	{
		base.Init();

		Health = base.m_health;

	}

	public override void Move(){
		base.Move();
	}

	public void Damage(){
		
		if (isDead) {
			return;
		}

		Health--;
		m_anim.SetTrigger("Hit");
		isHit = true;
		m_anim.SetBool("InCombat", true);

		if (Health < 1) {
			m_anim.SetTrigger("Death");
			isDead = true;
			m_collider.enabled = false;

			//Spawn a Diamond as a GameObject
			GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
			//Get access to Diamond Component and set m_gems variable to the gems amount for this enemy
			if (diamond.GetComponent<Diamond>() != null) {
				diamond.GetComponent<Diamond>().m_gems = m_gems;
			}

			StartCoroutine(EnemyDeathPause(3f));
		}
	}

	IEnumerator EnemyDeathPause(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Destroy(this.gameObject);
	}

}
