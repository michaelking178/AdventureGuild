using System.Collections.Generic;
using UnityEngine;

public class ArtisanTrainingManager : MonoBehaviour
{
    [SerializeField]
    private List<ArtisanLevel> artisanLevels = new List<ArtisanLevel>();

    public void CompleteLevel()
    {
        // Offer to play another level or end training
        Debug.Log("Level Complete!");
    }
}
