using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour 
{
	/**Where should this platform end up*/
	public Vector3 targetPos = new Vector3();
	/**Where did this platform start*/
	public Vector3 spawnPos = new Vector3();
	/**Where will this platform move next*/
	private Vector3 nextMove = new Vector3();
	/**How much will this platform move per update*/
	public float moveSpeed = 0.1f;
	/**Is the platform moving forward*/
	public bool movingForward = true;
	/**How many seconds should the platform wait before reverse moving*/
	public float waitTime = 1.0f;
	/**How long does the platform still have to wait*/
	private float waitLeft = 0.0f;
	/**Is the platform waiting to swap movement*/
	private bool waiting = false;

	// Use this for initialization
	void Start() 
	{
		spawnPos = transform.position;
	}
	
	// Update is called once per frame
	void Update() 
	{
		calculateNextMovement();
		transform.Translate(nextMove);
	}

	/**Calculates the next movement for the platform*/
	void calculateNextMovement()
	{
		nextMove = targetPos - transform.position;
		nextMove.Normalize();
		nextMove *= moveSpeed;

		if(nextMove.magnitude < moveSpeed)
		{
			swapTarget();

			if(waitLeft > 0)
			{
				nextMove = Vector3.zero;
				waitLeft -= Time.deltaTime;
			}
			else
			{
				waitLeft = waitTime;
			}
		}
			
	}

	/**Swaps the target and spawn positions*/
	void swapTarget()
	{
		Vector3 temp = spawnPos;
		spawnPos = targetPos;
		targetPos = temp;

		movingForward = !movingForward;
	}
}
