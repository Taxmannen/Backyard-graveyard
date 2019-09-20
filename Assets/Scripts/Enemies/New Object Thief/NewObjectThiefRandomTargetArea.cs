using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefRandomTargetArea : MonoBehaviour
{
    [SerializeField] private Vector2 minDistancePoint;
    [SerializeField] private Vector2 maxDistancePoint;
    [SerializeField] private float midPointOfDeliveryRoomX;
    private float randomRoom;

    private void Start()
    {
        randomRoom = Random.Range(0, 10);
    }

    private void OnEnable()
    {
        randomRoom = Random.Range(0, 10);
    }

    public void SetTargetPositionToPlayArea()
    {
        
        float randomValueX;

        randomValueX = Random.Range(minDistancePoint.x, maxDistancePoint.x);
        if (randomRoom > 5)
        {
            randomValueX += midPointOfDeliveryRoomX;
        }

        
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
