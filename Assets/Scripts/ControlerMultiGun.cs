using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerMultiGun : MonoBehaviour 
{
	/**Which gun mode is selected*/
	public int selectedMode = 0;
	/**The objects that are the multigun, these should be child objects of this one*/
	public GameObject[] gunObjects;

	void Update()
	{
		if(Input.GetAxis("Gun Mode") > 0)
		{
			switchMode(selectedMode + 1);
		}
		else if(Input.GetAxis("Gun Mode") < 0)
		{
			switchMode(selectedMode - 1);
		}
	}

	/**Switch the current gun mode*/
	private void switchMode(int newMode)
	{
		//Disable the prevously enabled gun mode
		gunObjects[selectedMode].SetActive(false);

		//Prevent overflows with the gun object array
		if(newMode < 0)
		{
			selectedMode = gunObjects.Length - 1;
		}
		else if(newMode > gunObjects.Length)
		{
			selectedMode = 0;
		}

		//Enable the currently selected gun
		gunObjects[selectedMode].SetActive(true);
	}
}
