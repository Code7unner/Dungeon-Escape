using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable{

	public int Health{ get; set;}

	[SerializeField]
	private GameObject m_acidEffectPrefab;
	[SerializeField]
	private Transform m_spawnPoint;

	// Used for Initilization
	public override void Init()
	{
		base.Init();
		Health = base.m_health;
	}

	public override void Update(){
		//Override the enemy update function

	}

	public override void Move(){
		//Stay Still
	}

	public void Attack(){

		// Instantiate AcidEffect attack
		Instantiate(m_acidEffectPrefab, m_spawnPoint.position, Quaternion.identity);
	}

	public void Damage(){
		
		if (isDead) {
			return;
		}
		//Deduct 1 from health 
		Health--;
		//Set Animator paramater InCombat to true
		m_anim.SetBool("InCombat", true);

		//Death Sequence
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
