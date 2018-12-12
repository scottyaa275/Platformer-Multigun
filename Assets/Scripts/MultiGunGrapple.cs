using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiGunGrapple : MultiGun 
{
	/**The target where the gun is pulling the player to*/
	public Vector3 target;
	/**The direction from the player to the target*
	public Vector3 direction;
	/**The player, this is what the script moves*/
	public GameObject player;
	/**Used to override some of the built in physics for the player*/
	private FPSRigidBodyWalker playerPhysics;
	/**How fast does the player move towards the grappling point*/
	public float grappleSpeed = 0.1f;
	/**How much distance is left for the player to move to the grappling point*
	public float grappleDistance = 0.0f;
	/**How close does the player need to be to the grapple point before it stops working*
	public float grappleTolerance = 0.05f;
	/**Has the player locked onto another object*/
	public bool isGrappeled = false;

	/**Is grappling direct or does the player swing from one place to another*/
	public bool swingGrapple = false;
	/**Only raycast through certan portions of the map*/
	public LayerMask grappleMask;
	/**How far away can the player grapple to objects*/
	public float grappleDistance = 10f;
	/**The position of the player when using swing grapple mechanics*/
	private Vector3 grappleSwingPos = new Vector3();
	/**The velocity of the player when using swing grapple mechanics*/
	private Vector3 grappleSwingVel = new Vector3();
	/**The acceleration of the player when using swing grapple mechanics*/
	private Vector3 grappleSwingAcc = new Vector3();
	/**The position of the player when using a swing grapple*/
	public GameObject grapplePlayerRef;
	/**How quickly the player slows down when grappling*/
	public float grappleAirResistance = 2.0f;
	/**The ridigbody used to simulate player swinging*/
	private Rigidbody grapplePlayerBody;
	/**The grapple swing target*/
	public GameObject grappleSwingTarget;
	/**The swing joint on this object*/
	private HingeJoint grappleSwingJoint;
	/**The point where the player swings from*/
	public GameObject grappleSwingPoint;

	public GameObject grapplePartBase;
	public GameObject grapplePartJoint;
	public GameObject grapplePartSpring;

	protected void Start()
	{
		playerPhysics = player.GetComponent<FPSRigidBodyWalker>();

		grapplePlayerBody = grapplePartJoint.GetComponent<Rigidbody>();

		grappleSwingJoint = grapplePartBase.GetComponent<HingeJoint>();
	}

	protected override void Update()
	{
		base.Update();

		/*if(grappleDistance > grappleTolerance)
		{
			//direction = target - transform.position;
			//direction.Normalize();
			//direction *= grappleSpeed;
			//player.transform.Translate(direction);
			//grappleDistance -= grappleSpeed;
		}
		else
		{
			//TODO: This code is not well written, calling getcomponent every frame is a bad idea
			//player.GetComponent<FPSRigidBodyWalker>().m_GravityMultiplier = 2;
		}*/

		if(isGrappeled)
		{
			if(swingGrapple)
			{
				//Set the position refrence to the swing position
				/*grapplePlayerRef.transform.position = grappleSwingPos;
				//Add velocity to the position
				grappleSwingVel += grappleSwingAcc;

				grappleSwingPos += grappleSwingVel;

				grappleSwingVel /= grappleAirResistance;

				grappleSwingPos += Physics.gravity;*/

				//Make the player's position same as the rigidbody on the swing joint, disable the joint once it's done swinging.

				//First, enable the swing joint, set that object's position to the target pos
				//Next, set the swing joint's pos to the player's current pos
				//When the swing joint hits something, disable it and resume player control


				//Set the player's position to the grappling hook
				//NOTE: UNCOMMENT THIS LINE FOR IT TO WORK
				player.transform.position = grapplePartJoint.transform.position;
				//Get the direction of the player
				ray = new Ray(transform.position, transform.forward);
				Physics.Raycast(ray, out hit);

				playerPhysics.velocity -= Physics.gravity;

				//Add velicoty towards where they are aiming
				grapplePartJoint.GetComponent<Rigidbody>().velocity = playerPhysics.velocity + (ray.direction * 10f);

				if(grappleSwingJoint.connectedAnchor.y > 1.0f)
					grappleSwingJoint.connectedAnchor = new Vector3(0, Mathf.Abs(grappleSwingJoint.connectedAnchor.y) - 0.2f, 0);

				//Eliminate any velocity on the player's physics
				//playerPhysics.velocity = Vector3.zero;
			}
			else
			{
				player.transform.position = Vector3.MoveTowards(transform.position, target, grappleSpeed);
				//playerPhysics.grounded = true;
			}
		}
	}

	protected override void OnSecondaryFire()
	{
		base.OnSecondaryFire();
	}

	//Ray and hit stored here to prevent re-creation of variables every frame
	Ray ray;
	RaycastHit hit;

	protected override void OnSecondaryFireDown()
	{
		//Create a new ray facing the direction of the gun
		ray = new Ray(transform.position, transform.forward);
		hit = new RaycastHit();

		if(Physics.Raycast(ray, out hit, grappleDistance, grappleMask.value))
		{
			if(hit.collider.CompareTag("Grapple_Allowed"))
			{
				//Pull the player over to the grappling point
				//Debug.DrawRay(ray.origin, hit.point - transform.position, Color.green, 15f);

				target = hit.point;
				isGrappeled = true;
				/*player.transform.Translate(ray.direction * grappleSpeed);
				grappleDistance = Vector3.Distance(transform.position, hit.point);//(hit.point - transform.position).magnitude;
				//Should find the vector from the player's pos to the target pos, then move the player along that vector by (speed) units per frame
				direction = hit.point - transform.position;
				direction.Normalize();
				direction *= grappleSpeed;*/


				//Teleports the "hook" to the target pos, then teleports the player to the lowermost part of the "hook"
				//Then swings the hook towards the target


				//grappleSwingPos = player.transform.position;

				//grappleSwingVel = ray.direction;

				//grapplePlayerRef.SetActive(true);

				grapplePartBase.SetActive(true);
				grapplePartBase.transform.position = target;

				//Set the joint pos to the player pos to start the swing
				grapplePartJoint.transform.position = transform.position;

				//Vector3 grappleAnchor = new Vector3(0, (target - transform.position).magnitude, 0);
				//Set the rotation point to the distance from the hook to the player, swing them along it
				grappleSwingJoint.connectedAnchor = new Vector3(0, (target - transform.position).magnitude, 0);

				Vector3 grappleRot = transform.rotation.eulerAngles;
				//Clear rotation values
				grappleRot.x = 0; 
				grappleRot.z = 0;
				//Set grapple box to rotation
				grapplePartBase.transform.rotation = Quaternion.Euler(grappleRot);

				grapplePartJoint.GetComponent<Rigidbody>().AddForce(playerPhysics.velocity);

				//TODO: Add code to lift the player up and down the grappling hook by moving the anchor y distance

				//Set y rotation of grapple thing to be the same as normal position

				//Vector3 swingAxis = (target - transform.position);
				//swingAxis = transform.rotation.eulerAngles;
				//swingAxis.y = 0;
				//grappleSwingJoint.axis = swingAxis;

				//SetHookPartsActive(true);

				//Activate the grapple point object
				//grappleSwingTarget.SetActive(true);
				//grappleSwingTarget.transform.position = target;
				//
				//grapplePartBase.transform.position = target;


				//Make an arc from the player's pos to the hook pos
			}
		}
	}

	protected override void OnSecondaryFireUp()
	{
		if(isGrappeled)
		{
			//Deactivate the grappling hook and all its sub-components
			//grapplePlayerRef.SetActive(false);

			//grappleSwingTarget.SetActive(false);

			//SetHookPartsActive(false);

			grapplePartBase.SetActive(false);

			isGrappeled = false;

			playerPhysics.velocity = grapplePartJoint.GetComponent<Rigidbody>().velocity;
		}
	}

	//Sets the parts of the grappling hook
	void SetHookPartsActive(bool active)
	{
		grapplePartBase.SetActive(active);
		grapplePartJoint.SetActive(active);
		grapplePartSpring.SetActive(active);
	}

	//OPEN GL CODE TO DRAW GRAPPLING GRAPHICS

	protected void DrawWeapon()
	{
		
	}
}
