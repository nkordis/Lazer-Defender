using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Enemy pathing is regulated by WaveConfig.cs
 * This is from where the enemy gets its waypoints.
 */
public class EnemyPathing : MonoBehaviour {

    WaveConfig waveConfig;
    List<Transform> waypoints;
    int currentWaypoint = 0;

	
	void Start () {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[currentWaypoint].transform.position;
	}
	
	
	void Update ()
    {
        Move();
    }

    /* It is called by the EnemySpawner just after the instantiation
     * of an Enemy GameObject;
     */
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (currentWaypoint <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[currentWaypoint].position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
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
