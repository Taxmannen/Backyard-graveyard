using UnityEngine;

/* Script Made By Daniel */
public enum EnemyType { Zombie, OrnamentTheif }

public static class PoolManager
{
    public static void ReturnEnemy(GameObject enemy, EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Zombie:
                ZombiePool.GetInstance().ReturnToPool(enemy);
                break;
            case EnemyType.OrnamentTheif:
                OrnamentThiefPool.GetInstance().ReturnToPool(enemy);
                break;
            default:
                Debug.LogError("Something Went Wrong");
                break;
        }
    }

    public static bool ReturnOrnament(Ornament ornament, bool skipRemoveFromList = false)
    {
        if (ornament)
        {
            switch (ornament.GetOrnamentType())
            {
                case OrnamentType.Flower:
                    FlowerPool.GetInstance().ReturnToPool(ornament.gameObject, skipRemoveFromList);
                    break;
                case OrnamentType.Candle:
                    CandlePool.GetInstance().ReturnToPool(ornament.gameObject, skipRemoveFromList);
                    break;
                case OrnamentType.Heart:
                    HeartPool.GetInstance().ReturnToPool(ornament.gameObject, skipRemoveFromList);
                    break;
                case OrnamentType.Statue:;
                    StatuePool.GetInstance().ReturnToPool(ornament.gameObject, skipRemoveFromList);
                    break;
                default:
                    Debug.LogError("Something Went Wrong");
                    break;
            }
            return true;
        }
        else return false;
    }
}