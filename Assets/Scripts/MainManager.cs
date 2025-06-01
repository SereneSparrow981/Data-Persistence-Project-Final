using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This is MainManager for the Data Persistence Project

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;				// Assigned to Gameover Text object in Inspector
	public TextMeshProUGUI playerName;
	public TextMeshProUGUI highScoreText;		// A
										
    private bool m_Started = false;
    private int m_Points;  
    private bool m_GameOver = false;
    
	public static MainManager Instance;		// Static class to persist from the Main game scene to the Menu scene
	
    // Start is called before the first frame update
    void Start()
    {        
		playerName.text = "Name: " + TextSaver.Instance.SavedName;	// A
		highScoreText.text = $"High Score: {TextSaver.Instance.SavedName} - {TextSaver.Instance.SavedHighScore}";	// A
		
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }	
    }
	
	private void Awake()				// Singleton for MainManager to persist into the menu scene
	
	{		if (Instance != null)		// Prevent duplicates of MainManager
		{
			Destroy(gameObject);
			return;
		}
		
		Instance = this;
		DontDestroyOnLoad(gameObject);		
	}

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

	// I want to display the saved name from the JSON file here somehow

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
		
		if (m_Points > TextSaver.Instance.SavedHighScore)		// Save new high score if greater than old score
		{
			string currentName = TextSaver.Instance.SavedName;
			TextSaver.Instance.SaveHighScore(currentName, m_Points);
			Debug.Log("New High Score!");
		}
    }
}
