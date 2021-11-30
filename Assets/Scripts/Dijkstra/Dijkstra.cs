
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dijkstra
{

    public static void FindPath<NodeType>(
        IGraph<NodeType> graph,
        NodeType startNode, NodeType endNode,
        List<NodeType> outputPath)
    {

        NodeType u;

        // openSet is for to mark the visited vertexs.
        // previous Dictionary is for each vertex to save who is parent.(what vertex we came from)
        // distance Dictionary holds every NodeType with it's ditance from the source vertex.
        // openQueue is for iterating on the vertexs (only the one's we can go through).

        HashSet<NodeType> openSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        Dictionary<NodeType, int> distance = new Dictionary<NodeType, int>();
        Queue<NodeType> openQueue = new Queue<NodeType>();

        openQueue.Enqueue(startNode);
        distance.Add(startNode, 0);
        openSet.Add(startNode);

        int count = 0;


        while(openQueue.Count != 0)
        {
            count++;
            NodeType current = openQueue.Dequeue();

            // getting the NodeType with the minimum weight from the source vertex.
            u = MinDistance(distance, current,openSet);

            // mark it as visited
            openSet.Add(u);
          
            // itearting through it's neighbours.
            foreach (var neighbor in graph.Neighbors(u))
            {
                // calculating the neighbour weight
                int weight = graph.Weights(neighbor);
                // checks if this vertex is exists in the dic
                if (distance.ContainsKey(neighbor))
                    {

                        
                        // checking if the current vertex weight + the neibouhr weight is smaller
                        // then the current distance from this vertex to the soure vertex and if so update it.
                        if (!openSet.Contains(neighbor)
                           && distance[u] + weight < distance[neighbor])
                        {
                            previous[neighbor] = u;

                            distance[neighbor] = distance[u] + weight;
                            openQueue.Enqueue(neighbor);
                    }

                }

                else
                {
                    distance.Add(neighbor, distance[u] + weight);
                    previous[neighbor] = u;
                    openQueue.Enqueue(neighbor);

                }

               
            }

         



        }


        // when we have finished the for loop , we will have in distance dictionary all the weights from the source.
        // so we chose to make the path from the source to the target vertex by going back to discover who it's current parent,
        // who we came from until we reach source vertex.
       
        outputPath.Add(endNode);
        NodeType searchFocus = endNode;
        while (previous.ContainsKey(searchFocus))
        {
            searchFocus = previous[searchFocus];
            outputPath.Add(searchFocus);
        }
        outputPath.Reverse();


       

    }

    public static void FindFastestPath<NodeType>(
       IGraph<NodeType> graph,
       NodeType startNode, NodeType endNode,
       List<NodeType> outputPath,int speed)
    {

        NodeType u;

        HashSet<NodeType> openSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        Dictionary<NodeType, int> distance = new Dictionary<NodeType, int>();
        Queue<NodeType> openQueue = new Queue<NodeType>();

        openQueue.Enqueue(startNode);
        distance.Add(startNode, 0);
        openSet.Add(startNode);

        int count = 0;


        while (openQueue.Count != 0)
        {
            count++;
            NodeType current = openQueue.Dequeue();

            u = MinDistance(distance, current, openSet);

            openSet.Add(u);
     
            foreach (var neighbor in graph.Neighbors(u))
            {
                int tailSpeed = graph.Speed(neighbor,speed);
                if (distance.ContainsKey(neighbor))
                {



                    if (!openSet.Contains(neighbor)
                       && distance[u] + tailSpeed < distance[neighbor])
                    {
                        previous[neighbor] = u;

                        distance[neighbor] = distance[u] + tailSpeed;
                        openQueue.Enqueue(neighbor);
                    }

                }

                else
                {
                    distance.Add(neighbor, distance[u] + tailSpeed);
                    previous[neighbor] = u;
                    openQueue.Enqueue(neighbor);

                }


            }





        }


        outputPath.Add(endNode);
        NodeType searchFocus = endNode;
        while (previous.ContainsKey(searchFocus))
        {
            searchFocus = previous[searchFocus];
            outputPath.Add(searchFocus);
        }
        outputPath.Reverse();




    }




    private static NodeType MinDistance <NodeType>(Dictionary<NodeType , int> dic, NodeType start, HashSet<NodeType> openSet )
        // this function gets the dictionary of the node type with it's weight
        // and the openSet that marked the visited vertexs and return the current NodeType with the minimum weight that we haven't
        // visited yet
        // for the dijkstra algorithm.

        {
        int min = 9999;
        NodeType temp = start;

                foreach( var vertex in dic)
        {
            if (vertex.Value < min && !openSet.Contains(vertex.Key))
            {
                min = vertex.Value;
                temp = vertex.Key;
                
            }
        }

        return temp;
        }

        public static List<NodeType> GetPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode

            )
        {
            List<NodeType> path = new List<NodeType>();
            FindPath(graph, startNode, endNode, path);
            return path;
        }


    public static List<NodeType> GetFastestPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode,int speed

        )
    {
        List<NodeType> path = new List<NodeType>();
        FindFastestPath(graph, startNode, endNode, path,speed);
        return path;
    }

}

