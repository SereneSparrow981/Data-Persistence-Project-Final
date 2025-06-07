using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// DO NOT DELETE
// TextSaver for Data Persistence Project

public class TextSaver : MonoBehaviour
{
	public InputField inputField;		// To store and pass player name
	private string filePath;			// For saving JSON file
	public static TextSaver Instance;	// Object attached to this script to persist between scenes, therefore needs to be static class
	public TextMeshProUGUI highScoreText;
		
	[System.Serializable]								// A
	public class SaveData								// A
	{
		public string playerName;						// A
		public int highScore;							// A
	}

	public string CurrentPlayerName { get; private set; } = "";					//A
	public string SavedName { get; private set; }		// A
	public int SavedHighScore { get; private set; }		// A
	
    // Start is called before the first frame update
    private void Start()
    {
		filePath = Path.Combine(Application.persistentDataPath, "savedText.json");
		LoadText();						// Load text at start if it exists
 		highScoreText.text = $"High Score: {TextSaver.Instance.SavedName} - {TextSaver.Instance.SavedHighScore}";
    }
	
	private void Awake()
	{
		if (Instance != null)			// If TextSaver exists, destroy duplicates, aka Singleton
		{
			Destroy(gameObject);
			return;
		}
		
		Instance = this;
		DontDestroyOnLoad(gameObject);	// Allows TextSaver to persist between scenes
	}

	public void SaveText()												// Method to save username
	{
		if (inputField != null)
		{
		CurrentPlayerName = inputField.text;
		Debug.Log("Welcome, player " + CurrentPlayerName);
		}
	}

	public void SaveHighScore(int score)	// A
	{
		SaveData data = new SaveData {playerName = CurrentPlayerName, highScore = score};
		
		string json = JsonUtility.ToJson(data);
		File.WriteAllText(filePath, json);
		
		SavedName = CurrentPlayerName;
		SavedHighScore = score;
		Debug.Log("High Score saved: " + CurrentPlayerName + " : " + score );
	}
	
	public void LoadText()
	{
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			SaveData data = JsonUtility.FromJson<SaveData>(json);
			SavedName = data.playerName;
			SavedHighScore = data.highScore;

		if (inputField != null)
			inputField.text = "";	// Start blank
			CurrentPlayerName = "";	// Don't assume current player is same as previous best
			Debug.Log("Loaded Name: " + SavedName + " | High Score: " + SavedHighScore);
		}
		
		else
		{
			SavedName = "";
			SavedHighScore = 0;
			Debug.Log("No saved data found.");
		}
	}	
    // Update is called once per frame
	    void Update()
    {
        
    }
}