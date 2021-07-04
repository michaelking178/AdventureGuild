using UnityEngine;

public class BackgroundPanel : MonoBehaviour
{
    private TrainingSword trainingSword;
    private CombatTrainingManager trainingManager;

    private void Start()
    {
        trainingSword = FindObjectOfType<TrainingSword>();
        trainingManager = FindObjectOfType<CombatTrainingManager>();
    }
    public void OnMouseDown()
    {
        if (!trainingManager.GamePaused)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!FindObjectOfType<CombatTrainingManager>().GameOver && hit.collider != null)
            {
                trainingSword.Swing(hit.point);
                trainingSword.WooshSound();
            }
        }
    }
}
