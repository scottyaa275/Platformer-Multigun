using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour 
{
	/**How long after the game starts will the player be teleported to spawn*/
	public float timer = 1.0f;
	/**The player object*/
	public GameObject player;

	//Use a coroutine to spawn the player, this will make the game wait untill it reaches <timer> seconds and then it will run
	void Start()
	{
		StartCoroutine(spawnPlayer(timer));
	}

	IEnumerator spawnPlayer(float time)
	{
		yield return new WaitForSeconds(time);
		player.transform.position = transform.position;
	}

}
