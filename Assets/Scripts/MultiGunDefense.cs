using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiGunDefense : MultiGun
{
	/**The object the player fires from the gun*/
	public GameObject fireObject;
	/**What force the object is fired at*/
	public float firePower = 100f;

	//Required for unity to update the script
	protected override void Update()
	{
		base.Update();
	}

	protected override void OnPrimaryFireDown()
	{
		GameObject fired = Instantiate<GameObject>(fireObject, transform.position, transform.rotation);
		fired.GetComponent<Rigidbody>().AddForce(transform.forward * firePower);
	}
}
