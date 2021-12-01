using UnityEngine;

public class MissZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Was the thing that hit this the ball or something else?
        if(collision.gameObject.name == "Ball")
        {
            // Ball hit miss zone, handle player loses 1 health
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm) gm.HandleBallHitMissZone();
        }
    }
}
