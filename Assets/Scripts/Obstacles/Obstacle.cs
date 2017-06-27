using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour, IRecyclable {

    public Vector2 velocity = Vector2.zero;

	public Sprite[] sprites;
	public Vector2 colliderOffset = Vector2.zero;

    private Rigidbody2D obstacleBody;


    private void Awake()
    {
        obstacleBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        obstacleBody.velocity = velocity;
    }

    public void Revive()
    {
        ResetSprite();
        ResetColliderOffset();
	}

	public void Suspend(){
        // pass
	}

    void ResetSprite()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    void ResetColliderOffset()
    {
        Vector3 size = GetComponent<SpriteRenderer>().bounds.size;
        size.y = colliderOffset.y;

        var collider = GetComponent<BoxCollider2D>();
        collider.size = size;
        collider.offset = new Vector2(-colliderOffset.x, collider.size.y / 2 - colliderOffset.y);
    }
}
