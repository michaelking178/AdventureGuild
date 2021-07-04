using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private ArtisanTrainingManager trainingManager;
    private PlayerBlock player;

    private void Start()
    {
        trainingManager = FindObjectOfType<ArtisanTrainingManager>();
        player = FindObjectOfType<PlayerBlock>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == player.gameObject)
        {
            playerRb = col.GetComponent<Rigidbody2D>();
            trainingManager.CompleteLevel();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject == player.gameObject)
        {
            playerRb.velocity *= 0.1f;
        }
    }
}
