using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOrientation : MonoBehaviour
{

    public bool potrait;
    public bool landscape;
    public bool rotate;

    private void Start()
    {
        if (potrait == true)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        else if (landscape == true)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if (rotate == true)
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}