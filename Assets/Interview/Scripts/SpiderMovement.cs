using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpiderMovement : MonoBehaviour
{
    public SpiderAnimation spiderAnimationController;
    public Seeker seeker;
    public float speed;
    private List<Vector3> path;
    private int currentWaypoint = 0;
    private bool canMove = false;
    private float currentSpeed;

    private void OnEnable()
    {
        currentSpeed = Random.Range(speed * 0.7f, speed * 1.3f);
        canMove = false;
        currentWaypoint = 0;
        path = new List<Vector3>();
        FindPath(GameManager.Instance.targetPos);
    }

    private void FindPath(Transform targetPos)
    {
        seeker.StartPath(transform.position, targetPos.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            StopAllCoroutines();
            canMove = true;
            //isStartGame = true;
            p.Claim(this);

            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
            path = p.vectorPath;
        }
    }

    private void ChangeState(StateMove move)
    {
        spiderAnimationController.ChangeState(move);
    }
    private StateMove CalculateNextStep(Vector2 direction)
    {
        StateMove state = StateMove.DOWN;
        //Calculate
        direction = direction.normalized;
        if (direction.x >= 0) state = StateMove.RIGHT;
        if (direction.x < 0) state = StateMove.LEFT;
        if (direction.y > 0) state = StateMove.UP;
        if (direction.y < 0) state = StateMove.DOWN;

        ChangeState(state); 
        return state;
    }

    private void Update()
    {
        if (!canMove) return;
        Moving();
    }

    private void Moving()
    {
        if (currentWaypoint >= path.Count)
        {
            GameManager.Instance.Reach();
            gameObject.SetActive(false);
            canMove = false;
            return;
        }
        Vector3 nextStep = path[currentWaypoint];
        Vector3 direction = nextStep - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextStep, currentSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, nextStep) < 0.1f)
        {
            transform.position = nextStep;
            currentWaypoint++;
        }
        CalculateNextStep(direction);
    }
}
