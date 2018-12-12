using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiGunGrab : MultiGun 
{

	[SerializeField]
	/**The place where the grabbed object will be stuck when it's picked up by the player*/
	private GameObject grabPoint;
	[SerializeField]
	/**How much force does the player have to grab the object they are looking at*/
	private float grabForce = 1.0f;

	//Required to allow primary and secondary fire methods to work
	protected override void Update()
	{
		base.Update();
	}

	/**A ray out from the camera's pos*/
	Ray ray;
	/**The place where the grab gun hits*/
	RaycastHit hit;
	/**The mask of what objects the grab gun can hit*/
	[SerializeField]
	LayerMask grabMask;

	//Grab an object in front of the camera
	protected override void OnPrimaryFireDown()
	{
		base.OnPrimaryFireDown();

	}

	//Launch the held object
	protected override void OnSecondaryFireDown()
	{
		base.OnSecondaryFireDown();
	}

	protected virtual void LaunchObject()
	{
		
	}
}
