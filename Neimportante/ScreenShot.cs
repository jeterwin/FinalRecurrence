using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenShot : MonoBehaviour
{
    private string screenshotName = "sample";
    private string screenshotName2 = ".png";
    int i = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        i += 2;
        if(Input.GetKeyDown(KeyCode.K))
        {
            screenshotName = screenshotName + i.ToString() + screenshotName2;
            ScreenCapture.CaptureScreenshot(screenshotName);
        }
    }
}
