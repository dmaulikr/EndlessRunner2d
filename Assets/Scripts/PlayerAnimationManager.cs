using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

    private bool isRunning;
	private Animator animator;
    private Player player;


	void Awake()
    {
        player = GetComponent<Player>();
		animator = GetComponent<Animator> ();
	}

	void Update()
    {

        isRunning = player.IsRunning();
		animator.SetBool ("Running", isRunning);
	}
}
