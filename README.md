Behaviour Tree:

this is a small behaviour tree i made in unity, it has the following states and behaviour. here is a small state diagram i made to show the workings of the behaviour tree

<img width="791" height="651" alt="BehaviourTreeStateDiagram" src="https://github.com/user-attachments/assets/f427d268-6519-46a9-83cc-4d2744f1ca6b" />

the tree has 4 branches and updates every frame. it walks through them left to right to decide the ai behaviour, it checks if it sees the player and has a weapon, in wich case it switches to attack behaviour, if it needs a weapon, it will find the closest weapon. if it needs to go to the last seen player position, either because it lost sight or because it was picking up a weapon, it does so in the thrid branch, and the default state is the patrol behaviour.


here is the class diagram showing the structure of the code:

<img width="2382" height="1165" alt="BehaviourTreeClassDiagram" src="https://github.com/user-attachments/assets/a969f744-5a52-4485-ad68-fae8ebb62e3a" />

i have one node baseclass that all nodes inherit, so i can easily group them up or reference them, ever node has a status an execute function and a reset function. the behaviour tree manager holds all of the nodes, so i can customize it however i want it. the blackboard holds all the sensors. agent context holds all references, so i only need to pass one object to the nodes that need it, and also reroutes the leaf nodes to the actual behaviours.


[Back to main](https://github.com/JensOuwens/Ai-Procedural)
