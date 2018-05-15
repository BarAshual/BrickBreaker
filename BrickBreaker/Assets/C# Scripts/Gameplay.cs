using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public const float RESET_DELAY = 1.5f;
    public int numOfBricks = 56;
    public int lives = 3;
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

        Setup();
    }

    public void Setup()
    {
        resettingPaddle = Instantiate(basePaddle, transform.position, Quaternion.identity) as GameObject;
        Instantiate(brickTemplate, transform.position, Quaternion.identity);
    }

    private void CheckIfWon()
    {
        if (numOfBricks < 1)
        {
            Won.SetActive(true);
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("BrickBreakerGame");
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
        numOfBricks--;
        CheckIfWon();
    }
}