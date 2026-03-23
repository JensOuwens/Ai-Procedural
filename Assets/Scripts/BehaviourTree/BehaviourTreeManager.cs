using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeManager : MonoBehaviour
{
    [SerializeField] private BlackBoard blackboard;
    [SerializeField] private AgentContext agentContext;
    
    public Node root;

    private void Awake()
    {
        // --- leaf nodes ---
        PatrolNode patrol = new PatrolNode(agentContext);
        AttackNode attack = new AttackNode(agentContext);
        PickUpNode pickUp = new PickUpNode(agentContext);
        Node moveToLastKnown = new MoveToLastKnownPositionNode(agentContext, blackboard);

        // --- has weapon -> attack ---
        HasWeaponConditionalNode hasWeaponAttack =
            new HasWeaponConditionalNode(blackboard, attack);

        // --- no weapon -> pick up -> attack ---
        SequenceNode pickUpThenSearch = new SequenceNode(new List<Node>
        {
            pickUp,
            moveToLastKnown,
            attack
        });

        // --- choose between attack OR pick up first ---
        SelectorNode combatSelector = new SelectorNode(new List<Node>
        {
            hasWeaponAttack,
            pickUpThenSearch
        });

        // --- only run combat if we see the player ---
        SeePlayerConditionalNode seePlayerCombat =
            new SeePlayerConditionalNode(blackboard, combatSelector);

        // --- ROOT ---
        root = new SelectorNode(new List<Node>
        {
            seePlayerCombat,
            patrol
        });
    }
}
