using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    public float duration;
    protected Animator animator;
    //Comboya devam etmeli mi
    protected bool shouldCombo;
    //Saldýrý sýrasý
    protected int attackIndex;
    
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        animator = GetComponent<Animator>();
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetMouseButtonDown(0))
        {
            shouldCombo = true;
        }
    }
}
