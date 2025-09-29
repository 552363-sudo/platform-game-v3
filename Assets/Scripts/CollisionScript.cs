using UnityEngine;

public class CollisionScript : MonoBehaviour
{
 
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Game Over!");
                // Stop movement or restart level
                Time.timeScale = 0; // Simple pause
            }
        }
   
}
