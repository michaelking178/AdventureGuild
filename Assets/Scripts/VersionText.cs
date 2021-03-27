using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    public TextMeshProUGUI versionText;

    private void Start()
    {
        versionText.text = Application.version.ToString();
    }
}
