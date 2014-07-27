using UnityEngine;
using System.Collections;

public class WebcamTestBehaviourScript : MonoBehaviour {
 

    IEnumerator Start ()
    {
            //Permission dialog appears. I wait until the user reacts
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            //Using the WebCamTexture permission if you can roll
            if (Application.HasUserAuthorization (UserAuthorization.WebCam)) {
                    WebCamTexture w = new WebCamTexture ();
                    //Material Paste the texture to
                    renderer.material.mainTexture = w;
                    //Playback
                    w.Play ();
            }
    }
}
