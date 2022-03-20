using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    public Camera cam;

    public float DesiredAspectRatio = 16f / 9f;

    // Start is called before the first frame update
    void Start()
    {
        ScaleViewport();
    }

    // Update is called once per frame
    void Update()
    {
        //For testing ScaleViewport
#if UNITY_EDITOR
        ScaleViewport();
#endif
    }

    private void ScaleViewport()
    {
        // determine the game window's current aspect ratio
        float windowaspect = Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / DesiredAspectRatio;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1)
        {
            Rect rect = cam.rect;

            rect.width = 1;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1 - scaleheight) / 2;

            cam.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1 / scaleheight;

            Rect rect = cam.rect;

            rect.width = scalewidth;
            rect.height = 1;
            rect.x = (1 - scalewidth) / 2;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
