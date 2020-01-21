using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerAnimation))]

public class Player : MonoBehaviour, IDamageable, ICollectable{

	[SerializeField]
	private int m_diamonds = 0;

	[Range(1,4)]
	[SerializeField]
	private int m_health = 4;
	// variable for move speed
	[SerializeField]
	private float m_speed = 5f;
	// variable for jump force
	[SerializeField]
	private float m_jumpForce = 5f;
	// layer of ground objects used for detection
	[SerializeField]
	private LayerMask m_groundLayer;

	// get handle for rigidbody
	private Rigidbody2D m_rBody2D;
	private PlayerAnimation m_playerAnim;
	private SpriteRenderer m_playerSprite;
	private SpriteRenderer m_swordArcSprite;

	private bool m_resetJump = false;
	private bool m_isGrounded = false;
	private bool m_isDead = false;

	public int Health { get; set;}
	public int Diamonds { get; set;}
	public bool canAttack = true;

	// Use this for initialization
	void Start(){

		Health = m_health;
		Diamonds = m_diamonds;
		Debug.Log("Player Health = " + Health.ToString());
		Debug.Log("Player Diamonds = " + Diamonds.ToString());
		// assign handle of rigidbody
		m_rBody2D = GetComponent<Rigidbody2D>();
		m_playerAnim = GetComponent<PlayerAnimation>();
		m_playerSprite = GetComponentInChildren<SpriteRenderer>();
		m_swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
		UIManager.Instance.UI_UpDateGems(UIManager.Instance.hudGemCountText, Diamonds);

	}
	
	// Update is called once per frame
	void Update(){
		
		Move();
		Attack();
	}

	void Move(){
		
		// horizontal input for left/right
		#if MOBILE_INPUT
		float move = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		#endif
		#if !MOBILE_INPUT
		float move = Input.GetAxisRaw("Horizontal");
		#endif

		// check if player is grounded
		//TODO: move to update function so IsGrounded is only called once per frame 
		m_isGrounded = IsGrounded();

		// Flip Sprite in X if move is < 0
		if (move > 0) {
			FlipSprite(true);
		} else 
			if (move < 0) {
			FlipSprite(false);
		}

		// Jump
		#if MOBILE_INPUT
		if (CrossPlatformInputManager.GetButtonDown("Jump") && IsGrounded()) {
			
			m_rBody2D.velocity = new Vector2(m_rBody2D.velocity.x, m_jumpForce);
			m_playerAnim.Jump(true);
			StartCoroutine(ResetJumpRoutine());
		}
		#endif

		#if !MOBILE_INPUT
		if (Input.GetKeyDown("space") && IsGrounded()) {

			m_rBody2D.velocity = new Vector2(m_rBody2D.velocity.x, m_jumpForce);
			m_playerAnim.Jump(true);
			StartCoroutine(ResetJumpRoutine());
		}
		#endif
		m_rBody2D.velocity = new Vector2(move * m_speed, m_rBody2D.velocity.y);
		m_playerAnim.Move(move);

	}

	void Attack(){
		if (!canAttack) {
			return;
		}
		#if MOBILE_INPUT
		if (CrossPlatformInputManager.GetButtonDown("Attack") && IsGrounded()) {
			m_playerAnim.Attack();
		}
		#endif

		#if !MOBILE_INPUT
		if(Input.GetMouseButtonDown(0) && IsGrounded()){
			m_playerAnim.Attack();
		}

		#endif
	}

	void FlipSprite(bool faceRight){
		if (m_playerSprite != null) {
			// facing right
			if (faceRight == true) {
				m_playerSprite.flipX = false;
				m_swordArcSprite.flipX = false;
				m_swordArcSprite.flipY = false;

				Vector3 newPos = m_swordArcSprite.transform.localPosition;
				newPos.x = Mathf.Abs(newPos.x);
				m_swordArcSprite.transform.localPosition = newPos;

				// faceing left
			} else if (faceRight == false) {
				m_playerSprite.flipX = true;
				m_swordArcSprite.flipX = false;
				m_swordArcSprite.flipY = true;

				Vector3 newPos = m_swordArcSprite.transform.localPosition;
				newPos.x = -0.8f;
				//newPos.x = newPos.x * -1;
				m_swordArcSprite.transform.localPosition = newPos;
			}
		}
	}

	bool IsGrounded(){

		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, m_groundLayer);
		Debug.DrawRay(transform.position, Vector2.down, Color.green);
		if (hitInfo.collider != null) {
			if (!m_resetJump) {
				m_playerAnim.Jump(false);
				return true;
			}
		}

		return false;
	}

	IEnumerator ResetJumpRoutine(){

		m_resetJump = true;
		yield return new WaitForSeconds(0.2f);
		m_resetJump = false;
	}

	public void Damage(){
		if (m_isDead) {
			return;
		}
		//Deduct 1 from health
		Health--;
		//Update UI Health
		UIManager.Instance.UpDatePlayerHealth(Health);
		//Play Hit Animation
		//m_playerAnim.Hit();
		//TODO: Play hit sound
		Debug.Log("Player Health = " + Health.ToString());
		//Check for dead
		if(Health < 1){
			m_isDead = true;
			//Play death animation
			m_playerAnim.Death();
			//TODO: Play death sound
			//TODO: Respawn player
		}

	}

	public void AddDiamonds(int value){

		Diamonds += value;
		UIManager.Instance.UI_UpDateGems(UIManager.Instance.hudGemCountText, Diamonds);
	}

}
