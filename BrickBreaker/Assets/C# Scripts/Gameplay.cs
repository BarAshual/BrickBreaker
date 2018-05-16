using UnityEngine;
using System.Linq;

public class Gameplay : MonoBehaviour
{
    public const float RESET_DELAY = 1.5f;
    public const string SCENE_NAME = "BrickBreakerGame";
    public int brickAmount = 56;
    public int lives = 3;
    public int currentLevel = 1;
    public GameObject Won;
    public GameObject Lost;
    public GameObject brickTemplate;
    public GameObject basePaddle;
    public GameObject resettingPaddle;

    public static Gameplay Instance = null;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        Destroy(GameObject.Find("Lev1Bricks"));
        Destroy(GameObject.Find("Lev2Bricks"));
        Setup();
    }

    public void Setup()
    {
        //BoxCollider2D[] colliders;
        resettingPaddle = Instantiate(basePaddle, transform.position, Quaternion.identity) as GameObject;
        brickTemplate = GameObject.Find("Lev" + currentLevel+ "Bricks");
        brickTemplate.GetComponentsInChildren<BoxCollider2D>().Select(x => x.enabled = true);
        Instantiate(brickTemplate, transform.position, Quaternion.identity);
    }

    private void CheckIfWon() 
    {
        if (brickAmount < 1)
        {
            Won.SetActive(true);
            currentLevel++;
            Invoke("ResetLevel", RESET_DELAY);
        }
    }

    private void CheckIfLost()
    {
        if (lives < 1)
        {
            Lost.SetActive(true);
            Invoke("ResetLevel", RESET_DELAY);
        }
    }

    private void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SCENE_NAME);
    }

    public void LoseLife()
    {
        switch (lives)
        {
            case 3:
                GameObject.Find("Heart 3").SetActive(false);
                break;
            case 2:
                GameObject.Find("Heart 2").SetActive(false);
                break;
            case 1:                
                GameObject.Find("Heart").SetActive(false);
                break;
        }
        lives--;
        Destroy(resettingPaddle);
        CheckIfLost();
        Invoke("ResetPaddleAfterDeath", RESET_DELAY);
    }

    private void ResetPaddleAfterDeath()
    {
        resettingPaddle = Instantiate(basePaddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        brickAmount--;
        CheckIfWon();
    }
}