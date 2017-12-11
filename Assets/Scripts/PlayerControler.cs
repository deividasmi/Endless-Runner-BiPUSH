using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

	public float moveSpeed;
	public float speedMultiplier;
	public float speedMilestone;
	private float speedMilestoneCount;
	private float moveSpeedStore;
	private float speedMilestoneCountStore;
	private float speedMilestoneStore;

	public float jumpForce;
	public float jumpTime;
	private float jumpTimeCounter;

	private Rigidbody2D myRigidBody;

	private bool stoppedJumping; // jump fix dont know if needed 
	private bool canDoubleJump = true;

	public bool grounded;
	public LayerMask whatIsGround;
	//private Collider2D myCollider; not needed anymore
	private Animator myAnimator;

	public Transform groundPos;

	public GameManager theGameManager;

	public AudioSource jumpSound;
	public AudioSource deathSound;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();
		jumpTimeCounter = jumpTime;
		speedMilestoneCount = speedMilestone;
		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedMilestoneStore = speedMilestone;
		stoppedJumping = true; // jump fix 1.1
	}

	
	// Update is called once per frame
	void Update () {

		//grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);
		grounded = Physics2D.OverlapCircle (new Vector2(groundPos.position.x, groundPos.position.y),0.5f,whatIsGround.value);
	
		if (transform.position.x > speedMilestoneCount) {
			speedMilestoneCount += speedMilestone;
			moveSpeed = moveSpeed * speedMultiplier;
			speedMilestone = speedMilestone * speedMultiplier;
		}
		
		myRigidBody.velocity = new Vector2 (moveSpeed, myRigidBody.velocity.y);

		//jumping and double junping
		if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
			if (grounded) {
				myRigidBody.velocity = new Vector2 (myRigidBody.velocity.x, jumpForce);
				stoppedJumping = false; //jump fix 1.1
				canDoubleJump = true;
				jumpSound.Play ();
			}

			if (!grounded && canDoubleJump) {
				myRigidBody.velocity = new Vector2 (myRigidBody.velocity.x, jumpForce);
				jumpTimeCounter = jumpTime;
				stoppedJumping = false; //jump fix 1.1
				canDoubleJump = false;
				jumpSound.Play ();
			}
		}

		if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) && !stoppedJumping) { //&& !stoppedJumping fix 1.1
			if (jumpTimeCounter > 0) {
				myRigidBody.velocity = new Vector2 (myRigidBody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) {
			jumpTimeCounter = 0;
			stoppedJumping = true; // jump fix 1.1
		}
		if (grounded) {
			jumpTimeCounter = jumpTime;
			canDoubleJump = true;
		}

		myAnimator.SetFloat ("Speed",myRigidBody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "killbox") {
			deathSound.Play ();
			theGameManager.RestartGame ();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedMilestone = speedMilestoneStore;
		}
	}
}
