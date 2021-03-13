using TMPro;
using UnityEngine;

public class Menu_Training : Menu
{
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public override void Open()
    {
        base.Open();
        timeText.text = "";
        scoreText.text = "";
    }
}
