using UnityEngine;

public class ArtisanLevel : MonoBehaviour
{
    /*
     * DIFFICULTY
     * 0: 10 or less min swipes
     * 1: 11-15 min swipes
     * 2: 16-20 min swipes
     */

    [SerializeField]
    private int difficulty;

    public int Difficulty { get { return difficulty; } }
}
