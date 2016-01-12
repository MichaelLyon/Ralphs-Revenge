using UnityEngine;
using System.Collections;

public class ElephantChose : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (ElephantSelect.ElephantSelected == "" || ElephantSelect.ElephantSelected == "Ralph")
        {
            animator.runtimeAnimatorController = Resources.Load("ElephantAnimationControllers/male") as RuntimeAnimatorController;
        }
        else if (ElephantSelect.ElephantSelected == "Ellie")
        {
            animator.runtimeAnimatorController = Resources.Load("ElephantAnimationControllers/female") as RuntimeAnimatorController;
        }
    }
}
