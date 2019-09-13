using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitureController : MonoBehaviour
{
    private Animator animator;

    private const string ANIMATION = "Partitura";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void playAnimation()
    {
        animator.Play(Animator.StringToHash(ANIMATION), -1 ,0f);
    }
}
