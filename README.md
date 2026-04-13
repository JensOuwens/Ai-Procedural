Astar:

a star is a widely used pathfinding algorithm in the game industry. 

for my implementation, i grabbed a maze generator online, and implemented an Astar into it. my implementation is grid based

after we get neighbours from the current cell in the openlist, i calcualte the f score and h score of the cell, being the distance away from the start position, and the distance away from the target cell respectivly. i do not check diagonal cells, and before the cells get calculated, i first check if theres a wall in between the cells or not, so that you cant go through walls
