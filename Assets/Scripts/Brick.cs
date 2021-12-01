using UnityEngine;

public class Brick : MonoBehaviour
{
    // Variables
    #region
    [SerializeField] private int health;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private Sprite[] stateSprites;
    [SerializeField] private bool unbreakable;
    [SerializeField] private int points = 1;
    #endregion

    // Properties
    #region
    public int Health
    {
        get { return health; }
        private set { health = value; }
    }
    public SpriteRenderer MySpriteRenderer
    {
        get { return mySpriteRenderer; }
        private set { mySpriteRenderer = value; }
    }
    public Sprite[] StateSprites
    {
        get { return stateSprites; }
        private set { stateSprites = value; }
    }
    public bool Unbreakable
    {
        get { return unbreakable; }
        private set { unbreakable = value; }
    }
    public int Points
    {
        get { return points; }
        private set { points = value; }
    }
    #endregion

    // Initialization
    #region
    private void Start()
    {
        if (!Unbreakable)
        {
            // Make brick's image reflect its health
            MySpriteRenderer.sprite = stateSprites[Health - 1];
        }
    }
    public void ResetBrick()
    {
        gameObject.SetActive(true);

    }
    #endregion

    // Logic
    #region
    private void HandleBrickHitByBall()
    {
        // Unbreakable bricks dont respond to damage or hits
        if (unbreakable) return;
        
        // Reduce health by 1
        health--;

        // Has the brick lost enough health to be destroyed?
        if(health <= 0)
        {
            // Turn off the brick
            gameObject.SetActive(false);
        }

        // Change the bricks color to reflect it's new health value
        else
        {
            MySpriteRenderer.sprite = stateSprites[health - 1];
        }

        // Increment score + check for victory condition
        GameManager gm = FindObjectOfType<GameManager>();
        if(gm) gm.HandleBrickHitByBall(this);
       

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Was the thing that hit this brick the ball or something else?
        if(collision.gameObject.name == "Ball")
        {
            HandleBrickHitByBall();
        }
    }

    #endregion


}
