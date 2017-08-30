using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.VR;


public class RightEye : MonoBehaviour {

    public WebCamTexture webCamRight;

    public RawImage display;


	// Use this for initialization

	void Start () {

        WebCamDevice device = WebCamTexture.devices[1];

        WebCamTexture webCamRight = new WebCamTexture(device.name);

        display.texture = webCamRight;

        webCamLeft.Play();

        }


}