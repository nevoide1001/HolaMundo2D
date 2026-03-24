using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private Player player;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        anim.SetBool("Run", horizontal != 0);
        anim.SetBool("Fly", vertical > 0);
    }

    public void SpikesHurt()
    {
        anim.SetTrigger("Hurt");
    }
}