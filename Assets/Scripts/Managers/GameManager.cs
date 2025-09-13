using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController player;

    private Door doorExit;

    public bool gameOver;

    public List<Enemy> enemies = new List<Enemy>();//敌人列表


    public void Awake()
    {
        if(instance == null)
        {  
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }   
        //playerController = FindObjectOfType<PlayerController>();
        //doorExit = FindObjectOfType<Door>();
    }

    public void Update()
    {
        if(player != null)
        {
            gameOver = player.isDead;
        }
        UIManager.instance.GameOverUI(gameOver);
    }

    public void IsEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void EnemyDead(Enemy enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            doorExit.OpenDoor();
            SaveData();
        }
    }

    public void IsPlayer(PlayerController controller)
    {
        player = controller;
    }
    public void IsExitDoor(Door door)
    {
        doorExit = door;
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //PlayerPrefs.DeleteKey("playerHealth");//满血复活
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);

    }

    public void ContinueGame()
    {
        if(PlayerPrefs.HasKey("sceneIndex"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        }
        else
        {
            NewGame();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public float LoadHealth()
    {
        if(!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetFloat("playerHealth", 3f);

        }
        float currentHealth = PlayerPrefs.GetFloat("playerHealth");

        //UIManager.instance.UpdateHealth(playerController.health);

        return currentHealth;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("playerHealth",player.health);
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();
    }
}
