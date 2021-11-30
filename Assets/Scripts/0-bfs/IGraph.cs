using System.Collections.Generic;
using UnityEngine;

/**
 * An abstract graph.
 * It does not use any memory.
 * It only has a single abstract function Neighbors, that returns the neighbors of a given node.
 * T = type of node in graph.
 * @author Erel Segal-Halevi
 * @since 2020-12
 */
public interface IGraph<T> {
    IEnumerable<T> Neighbors(T node);



    // this abstract function will return the weights of every neighbour.
    int Weights(T node);

    // this abstract function will return the speed on every neighbour tile.
    int Speed(T node , int speed);


}
