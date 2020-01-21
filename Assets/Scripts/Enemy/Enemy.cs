using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : MonoBehaviour {
	
	public GameObject diamondPrefab;

	[SerializeField]
    protected int m_health;
    [SerializeField]
	protected float m_speed;
    [SerializeField]
    protected int m_gems;
	[SerializeField]
	protected float m_attackRange = 2f;
	[SerializeField]
	protected Transform m_pointA, m_pointB;
	[SerializeField]
	protected Transform m_startPoint;

	protected Vector3 m_currentTarget;
	protected SpriteRenderer m_spriteRenderer;
	protected Animator m_anim;
	protected Collider2D m_collider;

	protected bool isHit = false;
	protected bool isDead = false;

	//variable to store player
	protected GameObject m_player;

	void Start(){
		Init();

		if (m_startPoint != null && m_pointA != null) {
			m_currentTarget = m_startPoint.position;
		} else {
			m_currentTarget = transform.position;
		}

		if (m_anim == null) {
			Debug.LogError("The Enemy Script component reguires a Animator component on the " + transform.name +"'s first child object");
			return;
		}

		if (m_spriteRenderer == null) {
			Debug.LogError("The Enemy Script component reguires a SpriteRenderer conponent on the " + transform.name + "'s first child object");
			return;
		}
	}

	public virtual void Init(){

		m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		m_anim = GetComponentInChildren<Animator>();
		m_player = FindObjectOfType<Player>().gameObject;
		m_collider = GetComponent<Collider2D>();
	}

	public virtual void Update(){
		// enemy can only move if not in idle state or combat
		if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && m_anim.GetBool("InCombat") == false) {
			return;
		}
		// Stop moving if enemy is dead
		if (isDead == false) {
			Move();
		}
	}

	public virtual void Move(){

		// Set enemy sprite in direction to move
		if (m_pointA != null && m_currentTarget.x == m_pointA.position.x) {
			m_spriteRenderer.flipX = true;

		} else {
			m_spriteRenderer.flipX = false;
		}

		// move the player between to waypoints
		if (m_pointB != null && transform.position == m_pointA.position) {

			m_currentTarget = m_pointB.position;
			m_anim.SetTrigger("Idle");

		}
		else if (m_pointA != null && transform.position == m_pointB.position) {

			m_currentTarget = m_pointA.position;
			m_anim.SetTrigger("Idle");

		}

		// freezes enemy movement when hit by player
		// by only moving enemy when player is out of reach for attack
		if (isHit == false && m_anim.GetBool("InCombat") == false) {
			transform.position = Vector3.MoveTowards(transform.position, m_currentTarget, m_speed * Time.deltaTime);
		}

		// check for distance between player and enemy
		// if distance > then float unfreeze enemy movement
		float _distance = Vector2.Distance(m_player.transform.localPosition, transform.localPosition);

		if (_distance >= m_attackRange) {
			isHit = false;
			m_anim.SetBool("InCombat", false);
		}

		//Flip Enemy sprite when in combat with player
		Vector2 m_direction = m_player.transform.localPosition - transform.localPosition;

		if (m_anim.GetBool("InCombat") == true && m_direction.x < 0f) {
			m_spriteRenderer.flipX = true;

		} else if (m_anim.GetBool("InCombat") == true && m_direction.x > 0f) {
			m_spriteRenderer.flipX = false;
		}
	}

}
