using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// DO NOT DELETE

public class TextSaver : MonoBehaviour
{
	public InputField inputField;		// To store and pass player name
	private string filePath;			// For saving JSON file
	public static TextSaver Instance;	// Object attached to this script to persist between scenes, therefore needs to be static class
	
	[System.Serializable]
	public class NameData
	{
	 	public string savedText;
	}
	
	[System.Serializable]								// A
	public class SaveData								// A
	{
		public string playerName;						// A
		public int highScore;							// A
	}

	public string SavedName { get; private set; }		// A
	public int SavedHighScore { get; private set; }		// A
	
    // Start is called before the first frame update
    private void Start()
    {
		filePath = Path.Combine(Application.persistentDataPath, "savedText.json");
		LoadText();						// Load text at start if it exists
       
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

	public void SaveHighScore(string playerName, int score)	// A
	{
		SaveData data = new SaveData {playerName = playerName, highScore = score};
		string json = JsonUtility.ToJson(data);
		File.WriteAllText(filePath, json);
		Debug.Log("High Score saved: " + playerName + " - " + score);
	}

	public void SaveText()												// Method to save username
	{
		NameData data = new NameData {savedText = inputField.text};		// Make new instance of NameData
		string json = JsonUtility.ToJson(data);							// Convert to JSON
		File.WriteAllText(filePath, json);								// Write JSON to file
		Debug.Log("Name saved: " + inputField.text);
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
			inputField.text = data.playerName;

			Debug.Log("Loaded Name: " + data.playerName + " | High Score: " + data.highScore);
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