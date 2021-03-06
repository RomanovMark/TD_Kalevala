using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType
{
    public enum EnemyArmorType
    {
        Physical,
        Magical,
        True
    };

    public enum EnemyElement
    {
        None,
        Fire,
        Water,
        Light,
        Earth,
        Nature,
        Wind
    };
}

public class MoveEnemy : MonoBehaviour
{
    [HideInInspector] public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;
    public float armor = 0.1f;
    public float attack = 1.0f;
    private GameManager gameManager;
    [SerializeField] private int killingPrice;
    public EnemyType.EnemyArmorType enemyTypeArmor;
    public EnemyType.EnemyElement enemyElement;
    

    public int KillingPrice
    {
        get { return killingPrice; }
    }

    void Start()
    {
        lastWaypointSwitchTime = Time.time;
        gameManager = GameObject.Find("GameManagerBehaviour").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.gameOver)
        {
            Vector3 startPosition = waypoints[currentWaypoint].transform.position;
            Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

            
            float pathLength = Vector2.Distance(startPosition, endPosition);
            float totalTimeForPath = pathLength / speed;
            float currentTimeOnPath = Time.time - lastWaypointSwitchTime;

            gameObject.transform.position =
                Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

            if (gameObject.transform.position.Equals(endPosition))
            {
                if (currentWaypoint < waypoints.Length - 2)
                {
               
                    currentWaypoint++;
                    lastWaypointSwitchTime = Time.time;

                    //RotateIntoMoveDirection();
                }
                else
                {

                    Destroy(gameObject);

                    //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                    //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                    GameManager gameManager =
                        GameObject.Find("GameManagerBehaviour").GetComponent<GameManager>();
                    gameManager.Health -= 1;
                }
            }
        }
    }

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }
}
