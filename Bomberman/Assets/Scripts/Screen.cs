using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour {

  
  public static Screen Instance;
    public List<string> imageNames;
    public List<string> scoreText;


    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    int a ; 
   

   
    private void Awake()
    {
        if (!Instance)
            Instance = this;

        myCamera = gameObject.GetComponent<Camera>();
    }

  
    private void Start()
    {
        a = 1;

    }

  
   
	public void OnPostRender()
	{
        if (takeScreenshotOnNextFrame)
        {

            string scoreName;

            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32,false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            string screenName = "/CameraScreenshot" + a + ".png"; 
           // System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            System.IO.File.WriteAllBytes(Application.dataPath + screenName, byteArray);
            // Debug.Log("Saved CameraScreenshot.png");
            Debug.Log("Saved" + screenName);

            imageNames.Add(screenName);

            scoreName=  "Player1: " + UIBehaviour.Instance.scorePlayer1 +  "\n" + "Player2: " + UIBehaviour.Instance.scorePlayer2;
            scoreText.Add(scoreName);
                  
            a = a + 1;
            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
	}
    }

    private void TakeScreenshot (int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static (int width, int height)
    {
        Instance.TakeScreenshot(width, height);
    }

 

}
