using UnityEngine;

/* Script Made By Daniel */
public class ObjectPoolOrnament : ObjectPool
{
    protected override GameObject FindOldestAvailable()
    {
        GameObject objectToReturn = usedObjects[0];
        for (int i = 0; i < usedObjects.Count; i++)
        {
            Ornament ornament = usedObjects[i].GetComponent<Ornament>();
            if (ornament && !ornament.ActiveHand && !ornament.Placement)
            {
                objectToReturn = usedObjects[i];
                break;
            }
        }
        return objectToReturn;
    }
}