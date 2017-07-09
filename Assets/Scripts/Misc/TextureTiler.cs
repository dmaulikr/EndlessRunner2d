using UnityEngine;
using System.Collections;

public class TextureTiler : MonoBehaviour {

	public int textureSize = 32;
	public bool scaleHorizontially = true;
	public bool scaleVertically = true;

	void Start () {
        ChangeTextureScale();
	}

    void ChangeTextureScale()
    {
        var newWidth = !scaleHorizontially ? 1 : Mathf.Ceil(Screen.width / textureSize);
        var newHeight = !scaleVertically ? 1 : Mathf.Ceil(Screen.height / textureSize);

        // transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1);

        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
    }

}
