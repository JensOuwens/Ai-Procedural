Procedural dungeon:

i looked at the game Crpyt of the necrodancer, and really like the way it handled its level generation, so i decided to try and make something like it for myself. i used a random walk algorithm combined with Unity's math.random functions to make seeding easy.

<img width="1786" height="523" alt="ProceduralDungeonClassDiagram" src="https://github.com/user-attachments/assets/faadde14-336f-4952-b574-4bf34255943f" />

the generator takes the following steps:
- generate the grid, the grid is a 2d array holding the Cell baseclass
- it makes the edges of the grid indistructable, so the player is unable to 
- it runs the random walk algorithm over the grid, and turns everything it walked into a path. the algorithm has a bias towards unvisited squares. the bias is variable and adjustable
- it tries to place rooms around the grid, only being placed if it touches a walked path
- it assings the start and exit room of the dungeon, placing the exit door and start door
- it will go through each cell wich is uninhabited, and run a random chance to place different types of content, enemies or treasure. it also determines spawn chances based on the kind of room it is
- after this, it displays the entire dungeon, instantiating gameobjects in the positions of the grid, where content is placed


[Back to main](https://github.com/JensOuwens/Ai-Procedural)

<img width="631" height="819" alt="image" src="https://github.com/user-attachments/assets/a93e1a57-f4bf-4d09-a901-d73788cc40db" />
<img width="703" height="781" alt="image" src="https://github.com/user-attachments/assets/3054d903-aa02-4ee9-a3c9-e0d4dc85e869" />
