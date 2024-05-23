using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public TextMeshProUGUI scoreTextRender;
    public int lootScore;
    public float lootDropChance;
    public GameObject loot;
    public AudioClip lootAudio;
    
    private int currentScore;
    public int hp = 3;
    public GameObject hpUI;

    public Button restart;
    public Button home;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreTextRenderGO;
    
    public bool isGameOver = false;


    public float enemySpawnDelay;
    public float enemyWaveSpawnDelay;
    public int maxWaveEnemyNum;
    public Vector3 spawnRange;
    public GameObject enemy;

    public static GameManager Instance
    {
        get{
            if(_instance == null) Debug.Log("Null GM, new one created");
            return _instance;
        }
    }

    private void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        isGameOver = false;
        StartCoroutine(SpawnWaves());
        UpdateScore();
        restart.gameObject.SetActive(false);
        home.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        scoreTextRenderGO.gameObject.SetActive(false);
        scoreTextRender.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) Time.timeScale = 0;
        else if (Input.GetKeyDown(KeyCode.F2)) Time.timeScale = 1;
    }

    public bool loseHp(){
        var child = hpUI.transform.GetChild(hp-1);
        child.gameObject.SetActive(false);
        if(hp == 1)
            return true;
        else
        {
            hp--;
            return false;
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            for (int j = 0; j < maxWaveEnemyNum; j++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange.x, spawnRange.x), spawnRange.y, spawnRange.z);
                Quaternion spawnRotation = Quaternion.Euler(0f, 180f, 0f);
                Instantiate(enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(enemySpawnDelay);
            }

            yield return new WaitForSeconds(enemyWaveSpawnDelay);
            if (isGameOver){break;}

        }

    }

    public void AddScore(int newScoreValue)
    {
        currentScore += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreTextRender.text = "Score: " + currentScore;
        scoreTextRenderGO.text = "Score: " + currentScore;
    }

    public void GameOver()
    {
        restart.gameObject.SetActive(true);
        home.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreTextRenderGO.gameObject.SetActive(true);
        scoreTextRender.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
