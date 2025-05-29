using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextSaver : MonoBehaviour
{
	public InputField inputField;		// To store and pass player name
										// Which file to store to MenuUI or MainManager argh
	private string filePath;			// For saving JSON file

// I want the object attached to this script to persist between scenes
// I need to turn this into a static class
	public static TextSaver Instance;
	
	[System.Serializable]
	public class NameData
	{
		public string savedText;
	}
	
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

	public void SaveText()												// Method to save username
	{
		NameData data = new NameData {savedText = inputField.text};		// Make new instance of NameData
		string json = JsonUtility.ToJson(data);							// Convert to JSON
		File.WriteAllText(filePath, json);								// Write JSON to file
		Debug.Log("Name saved: " + inputField.text);
	}
	
	public void LoadText()												// Method to load username
	{
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);					// Read JSON from file
			NameData data = JsonUtility.FromJson<NameData>(json);		// Convert JSON to NameData
			inputField.text = data.savedText;							// Set InputField text
			Debug.Log("Name Loaded: " + data.savedText);
		}
		else
		{
			Debug.Log("No saved name found.");
		}
	}
	
    // Update is called once per frame
	    void Update()
    {
        
    }
}
