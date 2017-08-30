using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.VR;


public class LeftEye : MonoBehaviour {

    public WebCamTexture webCamLeft;

    public RawImage display;


	// Use this for initialization

	void Start () {

        WebCamDevice device = WebCamTexture.devices[0];

        WebCamTexture webCamLeft = new WebCamTexture(device.name);

        display.texture = webCamLeft;

        webCamLeft.Play();

        }


}