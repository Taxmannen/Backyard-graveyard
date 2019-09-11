using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefEmptyStateForTesting : NewObjectThiefState
{
    public override void Enter(NewObjectThief objectThief)
    {
    }

    public override void Exit(NewObjectThief objectThief)
    {
    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
