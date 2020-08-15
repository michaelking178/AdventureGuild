using UnityEngine;

public class BackgroundPanel : MonoBehaviour
{
    private TrainingSword trainingSword;

    private void Start()
    {
        trainingSword = FindObjectOfType<TrainingSword>();
    }
    public void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (!FindObjectOfType<TrainingManager>().GameOver && hit.collider != null)
        {
            trainingSword.Swing(hit.point);
            trainingSword.WooshSound();
        }
    }
}
