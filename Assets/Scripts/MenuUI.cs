using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// Required if loading scenes in Unity

#if UNITY_EDITOR
using UnityEditor;		// Namespace for conditional code for quit application
#endif

// DO NOT DELETE
// Script for Data Persistence Project

[DefaultExecutionOrder(1000)]	// Sets this script later than the other default scripts

public class MenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void StartNew()
	{
		SceneManager.LoadScene(1);
		// This is for the Start Button to load the game
		// Remember to configure the Button OnClick
		// Assign MenuUI script to canvas
		// Click on Add symbol in the Button OnClick in Inspector
		// Drag in the Canvas game object
		// Assign function MenuUI to call StartNew method
		
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
		// This is to return to the main menu from the game
	}
	
	public void Exit()
	{
		// conditional code
		
		#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
		#else
			Application.Quit();		// This is to quit the game
		#endif
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
