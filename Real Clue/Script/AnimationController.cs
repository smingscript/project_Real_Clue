using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        //Front
	    if (Input.GetKey(KeyCode.W)== true)
	    {
	        animator.SetFloat("Walk", 1f, 0.1f, Time.deltaTime);
	    }

//	    if (Input.GetMouseButtonDown())
//	    {
//	        animator.SetFloat("Walk", 1f, 0.1f, Time.deltaTime);
//	    }

        //Left
        else if (Input.GetKey(KeyCode.A) == true)
	    {
            animator.SetFloat("Direction",-1f,0.1f,Time.deltaTime);
	    }
        //Right
	    else if (Input.GetKey(KeyCode.D) == true)
	    {
	        animator.SetFloat("Direction", 1f, 0.1f, Time.deltaTime);
        }
	    else
	    {
	        animator.SetFloat("Walk", 0f, 0.1f, Time.deltaTime);
            animator.SetFloat("Direction", 0f, 0.1f, Time.deltaTime);
        }
    }
}
