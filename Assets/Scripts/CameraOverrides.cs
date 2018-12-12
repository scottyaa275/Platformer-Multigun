using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverrides : MonoBehaviour 
{
	/**The main camera to use*/
	public Camera mainCam;
	/**Shader applied to the camera*/
	public Shader shaderOverride;
	/**The RT where the camera will render too*/
	//public RenderTexture dest;

	// Use this for initialization
	void Start () 
	{
		mainCam.SetReplacementShader(shaderOverride, "");
	}
	/*
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		
	}

	void Update()
	{
		Graphics.Blit(null, dest);
	}*/
}
