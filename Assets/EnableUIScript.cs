using UnityEngine;
using UnityEngine.UI;

public class EnableUIScript : MonoBehaviour
{
    public Canvas canvas; // Keeps your original name
    //public Camera mainCamera; // To assign the main camera

    private void Awake()
    {
        if (canvas == null)
        {
            // Find the Canvas GameObject by tag and get its Canvas component
            canvas = GameObject.FindWithTag("UIElement")?.GetComponent<Canvas>();
        }

        if (canvas == null)
        {
            Debug.LogError("EnableUIScript: No Canvas found with tag 'UIElement'!");
        }
        canvas.enabled = true;

        // If the main camera isn't assigned, try finding it automatically
        //if (mainCamera == null)
        //{
        //    mainCamera = Camera.main;
        //}
    }

    private void OnEnable()
    {
        if (canvas != null)
        {
            // Switch to World Space and assign the main camera
            //canvas.renderMode = RenderMode.WorldSpace;
            //canvas.worldCamera = mainCamera;

            canvas.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }
}
