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

	public bool grounded;
	public LayerMask whatIsGround;
	private Collider2D myCollider;
	private Animator myAnimator;

	public Transform groundPos;

	public GameManager theGameManager;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myCollider = GetComponent<Collider2D> ();
		myAnimator = GetComponent<Animator> ();
		jumpTimeCounter = jumpTime;
		speedMilestoneCount = speedMilestone;
		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedMilestoneStore = speedMilestone;
	}

	
	// Update is called once per frame
	void Update () {

		//grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);
		grounded = Physics2D.OverlapCircle (new Vector2(groundPos.position.x, groundPos.position.y),0.75f,whatIsGround.value);
	
		if (transform.position.x > speedMilestoneCount) {
			speedMilestoneCount += speedMilestone;
			moveSpeed = moveSpeed * speedMultiplier;
			speedMilestone = speedMilestone * speedMultiplier;
		}
		
		myRigidBody.velocity = new Vector2 (moveSpeed, myRigidBody.velocity.y);

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ){
			if (grounded) {
				myRigidBody.velocity = new Vector2 (myRigidBody.velocity.x, jumpForce);
			}
		}

		if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) {
			if (jumpTimeCounter > 0) {
				myRigidBody.velocity = new Vector2 (myRigidBody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) {
			jumpTimeCounter = 0;
		}
		if (grounded) {
			jumpTimeCounter = jumpTime;
		}

		myAnimator.SetFloat ("Speed",myRigidBody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "killbox") {
			theGameManager.RestartGame ();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedMilestone = speedMilestoneStore;
		}
	}
}
