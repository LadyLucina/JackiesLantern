using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineRendererTest : MonoBehaviour
{
    [SerializeField]
    private Target Prefab;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private LineRenderer Path;
    [SerializeField]
    private float PathHeightOffSet = 1.25f;
    [SerializeField]
    private float PathUpdateSpeed = 0.25f;

    private Target ActiveInstance;
    private NavMeshTriangulation Triangulation;
    private Coroutine DrawPathCoroutine;

    private void Awake()
    {
        Triangulation = NavMesh.CalculateTriangulation();
    }
    private void Start()
    {
        PathToPrefab();
    }

    private void PathToPrefab() //Draws line between objects
    {
        ActiveInstance = Prefab;

        if (DrawPathCoroutine != null)
        {
            StopCoroutine(DrawPathCoroutine);
        }
        DrawPathCoroutine = StartCoroutine(DrawPathToTarget());
    }
    private IEnumerator DrawPathToTarget() // Tracks position of Player
    {
        WaitForSeconds Wait = new WaitForSeconds(PathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();

        while (ActiveInstance != null)
        {
            if (NavMesh.CalculatePath(Player.position, ActiveInstance.transform.position, NavMesh.AllAreas, path))
            {
                Path.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                {
                    Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffSet);
                }
            }
            else
            {
                Debug.Log($"Unable to calculate a path");
            }
            yield return Wait;
        }
    }

}
