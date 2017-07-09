using UnityEngine;

public class ScrollingTexture : MonoBehaviour {

	public Vector2 speed = Vector2.zero;
	private Vector2 offset = Vector2.zero;

	private Material material;


	void Start () {
		material = GetComponent<Renderer> ().material;

		offset = material.GetTextureOffset ("_MainTex");
	}
	
	void Update () {
        UpdateTexture();
	}

    void UpdateTexture()
    {
        offset += speed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", offset);
    }
}
