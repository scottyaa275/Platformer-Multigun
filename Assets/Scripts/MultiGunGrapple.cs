using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiGunGrapple : MultiGun 
{
	/**The target where the gun is pulling the player to*/
	public Vector3 target;
	/**The direction from the player to the target*/
	public Vector3 direction;
	/**The player, this is what the script moves*/
	public GameObject player;
	/**How fast does the player move towards the grappling point*/
	public float grappleSpeed = 0.1f;
	/**How much distance is left for the player to move to the grappling point*/
	private float grappleDistance = 0.0f;
	/**How close does the player need to be to the grapple point before it stops working*/
	public float grappleTolerance = 0.05f;

	protected override void Update()
	{
		base.Update();

		if(grappleDistance > grappleTolerance)
		{
			//direction = target - transform.position;
			//direction.Normalize();
			//direction *= grappleSpeed;
			player.transform.Translate(direction);
			grappleDistance -= grappleSpeed;
			//TODO: This code is not well written, calling getcomponent every frame is a bad idea
			player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_GravityMultiplier = 0;
		}
		else
		{
			//TODO: This code is not well written, calling getcomponent every frame is a bad idea
			player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_GravityMultiplier = 2;
		}
	}

	protected override void OnSecondaryFireDown()
	{
		//Create a new ray facing the direction of the gun
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit = new RaycastHit();

		if(Physics.Raycast(ray, out hit))
		{
			if(hit.collider.CompareTag("Grapple_Allowed"))
			{
				//Pull the player over to the grappling point
				Debug.DrawRay(ray.origin, hit.point - transform.position, Color.green, 15f);

				target = hit.point;
				player.transform.Translate(ray.direction * grappleSpeed);
				grappleDistance = Vector3.Distance(transform.position, hit.point);//(hit.point - transform.position).magnitude;
				//Should find the vector from the player's pos to the target pos, then move the player along that vector by (speed) units per frame
				direction = hit.point - transform.position;
				direction.Normalize();
				direction *= grappleSpeed;
			}
		}
	}
}
