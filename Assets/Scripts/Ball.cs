using UnityEngine;

public class Ball : MonoBehaviour
{
    // Variables
    #region
    [SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private float speed = 500f;
    #endregion

    // Properties
    #region
    public Rigidbody2D MyRigidBody
    {
        get { return myRigidBody; }
        private set { myRigidBody = value; }
    }
    #endregion

    // Initialization
    #region
    private void Start()
    {
        ResetBall();
    }
    #endregion

    // Logic
    #region
    private void SetRandomTrajectory()
    {
        Vector2 startForce = Vector2.zero;

        // Randomize whether ball should shoot off left or right
        startForce.x = Random.Range(-1f, 1f);

        // Ball should move downwards to start
        startForce.y = -1f;

        // Move the ball
        myRigidBody.AddForce(startForce.normalized * speed);
    }
    public void ResetBall()
    {
        // Mive ball to screen centre
        transform.position = Vector2.zero;

        // Remove any speed/momentum from the the ball
        MyRigidBody.velocity = Vector2.zero;

        // Wait 1 second, then shoot ball off
        Invoke(nameof(SetRandomTrajectory), 1f);
    }
    #endregion

}
