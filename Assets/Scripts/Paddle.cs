using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Variables
    #region
    [SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float maxBounceAngle = 75f;
    #endregion

    // Properties
    #region
    public Rigidbody2D MyRigidBody
    {
        get { return myRigidBody; }
        private set { myRigidBody = value; }
    }
    public Vector2 Direction
    {
        get; private set;
    }
    #endregion

    // Logic
    #region
    private void Update()
    {
        // Move left
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Direction = Vector2.left;
        }

        // Move right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Direction = Vector2.right;
        }

        // No input detected, stop movement
        else
        {
            Direction = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        // Apply constant + regular force to the ball
        if(Direction != Vector2.zero)
        {
            MyRigidBody.AddForce(moveSpeed * Direction);
        }
    }
    private void OnCollisionEnter2D(Collision2D collisionData)
    {
        Ball ball = collisionData.gameObject.GetComponent<Ball>();
        if (ball == null) return;

        Vector3 paddlePosition = gameObject.transform.position;
        Vector3 contactPoint = collisionData.GetContact(0).point;

        float offset = paddlePosition.x - contactPoint.x;
        float width = collisionData.otherCollider.bounds.size.x / 2;

        float currentBallAngle = Vector2.SignedAngle(Vector2.up, ball.MyRigidBody.velocity);
        float bounceAngle = (offset / width) * maxBounceAngle;

        float newAngle = Mathf.Clamp(currentBallAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

        Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);

        ball.MyRigidBody.velocity = rotation * Vector2.up * ball.MyRigidBody.velocity.magnitude;



    }
    public void ResetPaddle()
    {
        transform.position = new Vector2(0, transform.position.y);
        MyRigidBody.velocity = Vector2.zero;
    }
    #endregion

}
