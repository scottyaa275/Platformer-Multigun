using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour 
{
	/**Jump to the next level*/
	public string nextLevelName = "Error";
	/**The text to show the loading percent*/
	[SerializeField]
	private Text levelLoadingText;
	/**The image to show the loading time*/
	[SerializeField]
	private Image levelLoadingImage;

	public bool testLoad = false;

	void Start()
	{
		//Keep this object on all scenes
		DontDestroyOnLoad(this.gameObject);
	}

	void Update()
	{
		if(testLoad)
		{
			StartCoroutine(LoadScene());
			testLoad = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			//End and jump to the next level
			//SceneManager.LoadScene(nextLevelName);
		}
	}

	//Loads a scene and shows the loading amount in the UI
	IEnumerator LoadScene()
	{
		AsyncOperation loading = SceneManager.LoadSceneAsync(nextLevelName);

		while(!loading.isDone)
		{
			levelLoadingText.text = "Loading... (" + (loading.progress * 100f).ToString("###") + "%)";
			levelLoadingImage.fillAmount = loading.progress;
			yield return null;
		}
	}
}
