using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables
    #region
    [SerializeField] private int score = 0;
    [SerializeField] private int lives = 3;
    [SerializeField] private int level = 1;
    #endregion

    // Properties
    #region
    public Ball CurrentBall
    {
        get; private set;
    }
    public Paddle CurrentPaddle
    {
        get; private set;
    }
    public Brick[] AllBricks
    {
        get; private set;
    }
    #endregion

    // Initialization
    #region
    private void Awake()
    {
        // Make sure this game object is not detroyed when changing scenes
        DontDestroyOnLoad(gameObject);

        // Subscribe 'OnLevelLoaded' to scene load hook.
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        // Reset variables
        score = 0;
        lives = 3;

        // Load the first level
        LoadLevel(1);
    }

    #endregion

    // Logic
    #region
    private void LoadLevel(int levelIndex)
    {
        level = levelIndex;

        // Note: scenes should always be named "Level X" where X is the level number. Otherwise, the function will not work.
        SceneManager.LoadScene("Level " + levelIndex);
    }
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and cache the new ball, paddle and bricks
        CurrentBall = FindObjectOfType<Ball>();
        CurrentPaddle = FindObjectOfType<Paddle>();
        AllBricks = FindObjectsOfType<Brick>();

        // Just incase, reset all brick health/colouring values
        foreach(Brick brick in AllBricks)
        {
            brick.ResetBrick();
        }
    }
    private void ResetLevel()
    {
        CurrentBall.ResetBall();
        CurrentPaddle.ResetPaddle();
    }
    private void HandleGameOver()
    {
        // TO DO IN FUTURE: This should load a 'game over' scene or bring up some kind of game over UI, not just instantly restart the game.
        NewGame();
    }
    public void HandleBrickHitByBall(Brick brick)
    {
        // Increase score
        score += brick.Points;

        // Has the player completed the level?
        if (AreAllBricksDestroyed())
        {
            // They did, load the next level
            LoadLevel(level + 1);
        }
    }
    public void HandleBallHitMissZone()
    {
        // Lose 1 life
        lives--;

        // Restart level if player is not dead
        if(lives > 0)
        {
            ResetLevel();
        }

        // Else, if player just lost their last life, restart the whole game
        else
        {
            HandleGameOver();
        }
    }
    private bool AreAllBricksDestroyed()
    {
        foreach(Brick b in AllBricks)
        {
            if(gameObject.activeInHierarchy == false && b.Unbreakable == false)
            {
                return false;
            }
        }

        return true;
    }
    #endregion
}
