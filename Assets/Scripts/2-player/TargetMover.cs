using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover: MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.

   

    public void SetTarget(Vector3 newTarget) {
        if (targetInWorld != newTarget) {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget() {
        return targetInWorld;
    }

    private TilemapGraph tilemapGraph = null;
    private float timeBetweenSteps;

    protected virtual void Start() {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        timeBetweenSteps = 1/ speed;
        StartCoroutine(MoveTowardsTheTarget());
       
    }

    IEnumerator MoveTowardsTheTarget() {
        for(;;) {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget() {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
  
 
        Vector3Int endNode = targetInGrid;
        //List<Vector3Int> shortestPath = BFS.GetPath(tilemapGraph, startNode, endNode, maxIterations);
       // List<Vector3Int> shortestPath = Dijkstra.GetPath(tilemapGraph, startNode, endNode);
        List<Vector3Int> shortestPath = Dijkstra.GetFastestPath(tilemapGraph, startNode, endNode,(int)speed);
        Debug.Log("shortestPath = " + string.Join(" , ",shortestPath));

  

        if (shortestPath.Count >= 2)
        {
            Vector3Int nextNode = shortestPath[1];
          
      
           timeBetweenSteps = getTileSpeed(tilemap.GetTile(nextNode).name);
            
    
            transform.position = tilemap.GetCellCenterWorld(nextNode);
        }
        else
        {
            atTarget = true;
        }
    }

    private float getTileSpeed(string name)
    {
        switch (name)
        {
            case "bushes":
                return TilemapGraph.bushesSpeed / speed;
                break;

            case "grass":
                return TilemapGraph.grassSpeed / speed;
                break;

            case "swamp":
                return TilemapGraph .swampSpeed/ speed;
                break;

            case "hills":
                return TilemapGraph.hillsSpeed / speed;
                break;

            default:
                return 1 / speed;
                break;
        }

       
    }
}
