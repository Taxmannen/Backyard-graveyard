using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTester : MonoBehaviour
{
    [SerializeField] private NewObjectThief objectThief;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //objectThief.Die();
            objectThief.randomTargetArea.SetTargetPositionToPlayArea();
        }
    }
}
