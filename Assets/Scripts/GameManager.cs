using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI totalScore;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;
    public GameObject enemies;
    public GameObject indblocks;
    public GameObject desblocks;
    public Button restartButton;
    private int enemyCount;
    //private int wavenumber = 1;
    private float xRange = 7.25f;
    private float yRange = 4.5f;
    public int score;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(5);
        SpawnDestructibleBlocks(10);
        SpawnIndestructibleBlocks(10);
    }

    // Update is called once per frame
    void Update()
    {   
            enemyCount = FindObjectsOfType<Enemy>().Length;
            if (enemyCount == 0)
            {
                GameWin();
            }      
    }
    void SpawnEnemies(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemies, GenerateSpawnPosition(), enemies.transform.rotation);
        }
    }

    void SpawnIndestructibleBlocks(int indblocksToSpawn)
    {
        for(int i=0; i < indblocksToSpawn; i++)
        {
            Instantiate(indblocks, GenerateSpawnPosition(), indblocks.transform.rotation);
        }
    }

    void SpawnDestructibleBlocks(int desblocksToSpawn)
    {
        for(int i=0; i< desblocksToSpawn; i++)
        {
            Instantiate(desblocks, GenerateSpawnPosition(), desblocks.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {

        float spawnPosX = Mathf.Round(Random.Range(-xRange, xRange));
        float spawnPosY = Mathf.Round(Random.Range(-yRange,yRange)) + 0.5f;

        Vector2 randomPos = new Vector2(spawnPosX, spawnPosY);

        return randomPos;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreBoard.text = "Score: " + score;
    }

    public void GameOver()
    {
        totalScore.text = scoreBoard.text;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        totalScore.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void GameWin()
    {
        winText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
