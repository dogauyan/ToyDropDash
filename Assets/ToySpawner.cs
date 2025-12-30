using UnityEngine;

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
    public SpriteRenderer ToyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnTick = InitialSpawnTick;
        MultiplierTick = 5;

        CalculateGameArea();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTick -= Time.deltaTime;
        MultiplierTick -= Time.deltaTime;
        if(SpawnTick <= 0)
        {
            SpawnTick = InitialSpawnTick * Mathf.Pow(TickSpeedMultiplier, MultiplierCount);
            Spawn();
        }
        if(MultiplierTick <= 0)
        {
            MultiplierCount++;
            MultiplierTick = 5;
        }
    }
    public void CalculateGameArea()
    {
        float xmin = EdgeLeft.position.x;
        float ymin = EdgeLeft.position.y - (EdgeUp.position.y - EdgeLeft.position.y);
        GameArea = new( new Vector2(xmin , ymin), new Vector2(EdgeRight.position.x - EdgeLeft.position.x , EdgeUp.position.y - ymin) );

        CollidableArea.size = new(Screen.width / 80, Screen.height / 80);
    }
    void Spawn()
    {
        Vector2 SpawnPosition = new();
        Vector2 TargetPosition = new();

        switch (Random.Range(0, 3))
        {
            case 0:
            SpawnPosition.x = GameArea.xMin;
            SpawnPosition.y = GameArea.center.y + Random.Range(0 , GameArea.height / 2);
            
            TargetPosition.x = GameArea.center.x + Random.Range(0 , GameArea.width / 2);
            TargetPosition.y = GameArea.yMin + Random.Range(0, GameArea.height);
            break;

            case 1:
            SpawnPosition.x = GameArea.xMin + GameArea.width;
            SpawnPosition.y = GameArea.center.y + Random.Range(0 , GameArea.height / 2);

            TargetPosition.x = GameArea.center.x - Random.Range(0 , GameArea.width / 2);
            TargetPosition.y = GameArea.yMin + Random.Range(0 , GameArea.height);
            break;

            case 2:
            SpawnPosition.y = GameArea.yMin + GameArea.height;
            SpawnPosition.x = GameArea.xMin + Random.Range(0 , GameArea.width);

            TargetPosition.x = GameArea.xMin + Random.Range(0 , GameArea.width);
            TargetPosition.y = GameArea.center.y + Random.Range(0 , GameArea.height / 2);
            break;
        }
        
        var Spawned = Instantiate(ToyPrefab , SpawnPosition , Quaternion.identity);

        var rig = Spawned.GetComponent<Rigidbody2D>();
        float xx = (TargetPosition - SpawnPosition).x;
        rig.AddForce(1 * new Vector2( Mathf.Min( Mathf.Abs(xx), GameArea.width * 0.7f) * Mathf.Sign(xx) , 0), ForceMode2D.Impulse);
        rig.AddForce((GameArea.height / 4) * Vector2.up, ForceMode2D.Impulse);
    }
}
