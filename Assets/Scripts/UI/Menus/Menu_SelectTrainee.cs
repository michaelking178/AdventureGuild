using UnityEngine;

public class Menu_SelectTrainee : MonoBehaviour
{
    [SerializeField]
    private PersonUIScrollView personUIScrollView;

    void Start()
    {
        personUIScrollView.GetAvailableGuildMembers();
    }
}
