using UnityEngine;

public class BackgroundPanel : MonoBehaviour
{
    public void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (!FindObjectOfType<TrainingManager>().GameOver && hit.collider != null)
        {
            FindObjectOfType<TrainingSword>().Swing(hit.point);
        }
    }
}
