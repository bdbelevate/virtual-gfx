using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumbotron : MonoBehaviour
{
    Animator anim;

    int inLeft = Animator.StringToHash("InLeft");
    int outLeft = Animator.StringToHash("OutLeft");
    int inRight = Animator.StringToHash("InRight");
    int outRight = Animator.StringToHash("OutRight");
    int offscreenLeft = Animator.StringToHash("OffscreenLeft");
    int offscreenRight = Animator.StringToHash("OffscreenRight");

    private int? trigger;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (!onscreenTriggered && anim.GetCurrentAnimatorStateInfo(0).IsName("InScene")) {
        //     onscreenTriggered = true;
        // }

        if (trigger.HasValue) {
            anim.SetTrigger(trigger.Value);
            trigger = null;
        }
    }

    public void InLeft() {
        Debug.Log("InLeft");
        trigger = inLeft;
    }

    public void OutLeft() {
        Debug.Log("OutLeft");
        trigger = outLeft;
    }

    public void InRight() {
        Debug.Log("InRight");
        trigger = inRight;
    }

    public void OutRight() {
        Debug.Log("OutRight");
        trigger = outRight;
    }
}
