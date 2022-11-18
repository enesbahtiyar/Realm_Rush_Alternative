using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);

    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }


    private void RecalculatePath(bool resetpath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetpath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.getCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void finishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startposition = transform.position;
            Vector3 endposition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelpercent = 0f;

            transform.LookAt(endposition);

            while (travelpercent < 1f)
            {
                travelpercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startposition, endposition, travelpercent);
                yield return new WaitForEndOfFrame();
            }
        }

        finishPath();
    }
}
