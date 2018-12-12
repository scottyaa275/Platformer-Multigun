using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiGun : MonoBehaviour 
{
	// Update is called once per frame
	protected virtual void Update () 
	{
		//Check to see if the player is holding any mouse button, checked in this order
		//to prevent multiple calls to GetMouseButton(x)

		//Primary Fire
		if(Input.GetMouseButtonDown(0))
			OnPrimaryFireDown();
		else if(Input.GetMouseButtonUp(0))
			OnPrimaryFireUp();
		else if(Input.GetMouseButton(0))
			OnPrimaryFire();

		//Secondary Fire
		if(Input.GetMouseButtonDown(1))
			OnSecondaryFireDown();
		else if(Input.GetMouseButtonUp(1))
			OnSecondaryFireUp();
		else if(Input.GetMouseButton(1))
			OnSecondaryFire();
		
	}

	//Methods called when the primary or secondary fire is clicked
	//These are empty by default
	protected virtual void OnPrimaryFireDown()
	{
		
	}

	protected virtual void OnPrimaryFire()
	{
		
	}

	protected virtual void OnPrimaryFireUp()
	{
		
	}

	protected virtual void OnSecondaryFireDown()
	{

	}

	protected virtual void OnSecondaryFire()
	{
		
	}

	protected virtual void OnSecondaryFireUp()
	{

	}
}
