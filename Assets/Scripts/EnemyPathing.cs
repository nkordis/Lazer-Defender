using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int currentWaypoint = 0;

	
	void Start () {
        transform.position = waypoints[currentWaypoint].transform.position;
	}
	
	
	void Update ()
    {
        Move();
    }

    private void Move()
    {
        if (currentWaypoint <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[currentWaypoint].position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                currentWaypoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
