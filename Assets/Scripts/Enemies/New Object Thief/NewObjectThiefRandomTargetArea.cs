using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefRandomTargetArea : MonoBehaviour
{
    [SerializeField] private Vector2 minDistancePoint;
    [SerializeField] private Vector2 maxDistancePoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SetTargetPositionToPlayArea();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetTargetPositionToOutOfBounds();
        }
    }

    public void SetTargetPositionToPlayArea()
    {
        float randomValueX = Random.Range(minDistancePoint.x, maxDistancePoint.x);
        float randomValueY = Random.Range(minDistancePoint.y, maxDistancePoint.y);

        transform.position = new Vector3(randomValueX, 0, randomValueY);
    }


    //Fixa så den väljer ett område utanför alla "fyrkanter" utanför play-area. Just nu tar den enbart en.
    public void SetTargetPositionToOutOfBounds()
    {
        float randomValueX = Random.Range(maxDistancePoint.x, maxDistancePoint.x * 10);
        float randomValueY = Random.Range(maxDistancePoint.y, maxDistancePoint.y * 10);

        transform.position = new Vector3(randomValueX, 0, randomValueY);
    }
}
