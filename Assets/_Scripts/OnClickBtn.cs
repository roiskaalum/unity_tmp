using UnityEngine;

public class OnClickBtn : MonoBehaviour
{
    public string linkURL = "https://www.google.com";

    public void OpenLink()
    {
        Application.OpenURL(linkURL);
    }
}
