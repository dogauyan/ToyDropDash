using UnityEngine;

[System.Serializable]
public class WeightedToy
{
    public Toy prefab;
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
    public WeightedToy[] RecoveryPrefabs;


    public Gradient blankGradient;

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

        GameArea = new Rect(
            new Vector2(xmin, ymin),
            new Vector2(
                EdgeRight.position.x - EdgeLeft.position.x,
                EdgeUp.position.y - ymin
            )
        );

        CollidableArea.size = new Vector2(
            Screen.width / 80f,
            Screen.height / 80f
        );
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

        Toy prefab;
        float luck = Random.Range(0f, 100f);
        byte _t = 0;

        // 0 = normal, 1 = trap, 2 = bonus, 3 = recovery

        if (ScoreManager.Instance.Misses > 0 && luck < 3f)
        {
            _t = 3; // recovery
        }
        else if (luck < 15f)
        {
            _t = 1; // trap
        }
        else if (luck < 30f)
        {
            _t = 2; // bonus
        }


        switch (_t)
        {
            default:
                prefab = GetRandomToyPrefab(ToyPrefabs);
                break;

            case 1:
                prefab = GetRandomToyPrefab(TrapPrefabs);
                break;

            case 2:
                prefab = GetRandomToyPrefab(BonusPrefabs);
                break;

            case 3:
                prefab = GetRandomToyPrefab(RecoveryPrefabs);
                break;
        }

        var Spawned = Instantiate(prefab, SpawnPosition, Quaternion.identity);
        if (Random.Range(0f,100f) <= 50)
        {
            Spawned.causesMissOnCatch = false;
            Spawned.causesMissOnExit = false;
            Spawned.isRecovery = false;
            Spawned.scoreValue = 1;

            Spawned.blankBubble.gameObject.SetActive(true);
            Spawned.GetComponent<SpriteRenderer>().color = new Color(1,1,1, .5f);
            Spawned.GetComponent<TrailRenderer>().colorGradient = blankGradient;
        };

        var rig = Spawned.GetComponent<Rigidbody2D>();

        float xx = (TargetPosition - SpawnPosition).x;

        rig.AddForce(
            new Vector2(
                Mathf.Min(Mathf.Abs(xx), GameArea.width * 0.7f) * Mathf.Sign(xx),
                0
            ),
            ForceMode2D.Impulse
        );

        rig.AddForce(
            (GameArea.height / 4f) * Vector2.up,
            ForceMode2D.Impulse
        );
    }

    Toy GetRandomToyPrefab(WeightedToy[] list)
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


        return list[0].prefab;
    }
}