using UnityEngine;

[System.Serializable]
public class WeightedToy
{
    public SpriteRenderer prefab;
    public float weight = 1f;
}

public class ToySpawner : MonoBehaviour
{
    public Transform EdgeLeft;
    public Transform EdgeRight;
    public Transform EdgeUp;
    public BoxCollider2D CollidableArea;

    Rect GameArea;

    public float InitialSpawnTick = 3;
    public float TickSpeedMultiplier = 0.8f;

    float MultiplierTick;
    float SpawnTick;
    int MultiplierCount = 0;

    public WeightedToy[] ToyPrefabs;
    public WeightedToy[] TrapPrefabs;
    public WeightedToy[] BonusPrefabs;

    void Start()
    {
        SpawnTick = InitialSpawnTick;
        MultiplierTick = 5;

        CalculateGameArea();
    }

    void Update()
    {
        SpawnTick -= Time.deltaTime;
        MultiplierTick -= Time.deltaTime;

        if (SpawnTick <= 0)
        {
            SpawnTick = InitialSpawnTick * Mathf.Pow(TickSpeedMultiplier, MultiplierCount);
            Spawn();
        }

        if (MultiplierTick <= 0)
        {
            MultiplierCount++;
            MultiplierTick = 5;
        }
    }

    public void CalculateGameArea()
    {
        float xmin = EdgeLeft.position.x;
        float ymin = EdgeLeft.position.y - (EdgeUp.position.y - EdgeLeft.position.y);

        GameArea = new(
            new Vector2(xmin, ymin),
            new Vector2(
                EdgeRight.position.x - EdgeLeft.position.x,
                EdgeUp.position.y - ymin
            )
        );

        CollidableArea.size = new(Screen.width / 80f, Screen.height / 80f);
    }

    void Spawn()
    {
        Vector2 SpawnPosition = Vector2.zero;
        Vector2 TargetPosition = Vector2.zero;

        switch (Random.Range(0, 3))
        {
            case 0:
                SpawnPosition.x = GameArea.xMin;
                SpawnPosition.y = GameArea.center.y + Random.Range(0, GameArea.height / 2);

                TargetPosition.x = GameArea.center.x + Random.Range(0, GameArea.width / 2);
                TargetPosition.y = GameArea.yMin + Random.Range(0, GameArea.height);
                break;

            case 1:
                SpawnPosition.x = GameArea.xMin + GameArea.width;
                SpawnPosition.y = GameArea.center.y + Random.Range(0, GameArea.height / 2);

                TargetPosition.x = GameArea.center.x - Random.Range(0, GameArea.width / 2);
                TargetPosition.y = GameArea.yMin + Random.Range(0, GameArea.height);
                break;

            case 2:
                SpawnPosition.y = GameArea.yMin + GameArea.height;
                SpawnPosition.x = GameArea.xMin + Random.Range(0, GameArea.width);

                TargetPosition.x = GameArea.xMin + Random.Range(0, GameArea.width);
                TargetPosition.y = GameArea.center.y + Random.Range(0, GameArea.height / 2);
                break;
        }

        SpriteRenderer prefab;
        float luck = Random.Range(0, 100);
        byte _t = 0;
        if (luck < 15)
        {
            _t = 1;
        }
        else if (luck < 30)
        {
            _t = 2;
        }
        switch (_t)
        {
            default :
                prefab = GetRandomToyPrefab(ToyPrefabs);
            break;
            case 1 : 
                prefab = GetRandomToyPrefab(TrapPrefabs);
            break;
            case 2 :
                prefab = GetRandomToyPrefab(BonusPrefabs);
            break;
        }
        var Spawned = Instantiate(prefab, SpawnPosition, Quaternion.identity);

        var rig = Spawned.GetComponent<Rigidbody2D>();
        float xx = (TargetPosition - SpawnPosition).x;

        rig.AddForce(
            new Vector2(
                Mathf.Min(Mathf.Abs(xx), GameArea.width * 0.7f) * Mathf.Sign(xx),
                0
            ),
            ForceMode2D.Impulse
        );

        rig.AddForce((GameArea.height / 4f) * Vector2.up, ForceMode2D.Impulse);
    }

    SpriteRenderer GetRandomToyPrefab(WeightedToy[] list)
    {
        float totalWeight = 0f;
        foreach (var toy in list)
            totalWeight += toy.weight;

        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var toy in list)
        {
            cumulative += toy.weight;
            if (randomValue <= cumulative)
                return toy.prefab;
        }

        return list[0].prefab; // fallback
    }
}
