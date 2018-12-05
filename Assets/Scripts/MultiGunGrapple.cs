using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiGunGrapple : MultiGun 
{
	/**The target where the gun is pulling the player to*/
	public Vector3 target;
	/**The player, this is what the script moves*/
	public GameObject player;

	protected override void Update()
	{
		base.Update();
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
				Debug.DrawRay(ray.origin, ray.direction * hit.point.magnitude, Color.green, 30f);
			}
			else
			{
				Debug.DrawRay(ray.origin, ray.direction * hit.point.magnitude, Color.red, 30f);
				Debug.Log(ray.origin + " origin, " + ray.direction + " direction");
			}
				
		}

		Debug.DrawRay(ray.origin, ray.direction, Color.blue, 30f);
	}
}
