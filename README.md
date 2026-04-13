Boids:

boids is a flocking algorythm following three simple rules:
1. seperation
    no two boids can occupy the same space
2. alingment
   all boids must face the general same direction, being the average of all directions
3. cohesion
   all boids must stay together, flocking towards the average position of all boids combined.

these three rules tell the boids how to move in relation to eachother.

i make all of the boids at runtime, and add them to a List. i loop through the list to determine the averages of every boid (direction and velocity), after wich i apply the 3 rules + an extra rule i added, to keep the boids from leaving a certain area.

[Back to main](https://github.com/JensOuwens/Ai-Procedural)
