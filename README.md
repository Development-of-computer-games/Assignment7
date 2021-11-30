# Assignment7

### In this assignment we choose to execute :
1. Dijkstra with weights - each tile has a fixed weight (for example the grass is 4, the swamp is 5, the hills is 6 and so on)
2. Dijkstra with speed - on each tile the player walks with diffrent speed ( for example on the swamps the player walks very slow, on the grass very fast and so on)
3. Mining mountains into grass - the player cannot walk on mountains but the player can turn them into grass by minning the mountains. pressing the arrow key on the direction of the mountain (long press) and then pressing X on the keyboard. This process will take some time as in the real world it takes time to mine

# Explaination :
We created a Dijkstra script that will execute our algorithm. We extended the graph interface that will return us also the wieghts and the speed in order to determine each time the cost of passing each tile.
[link to the dijksta](https://github.com/Development-of-computer-games/Assignment7/blob/main/Assets/Scripts/Dijkstra/Dijkstra.cs)
