using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BehaviourTreeManager : MonoBehaviour
{
    [SerializeField] private BlackBoard blackboard;
    [SerializeField] private AgentContext agentContext;
    [SerializeField] private TMP_Text stateText;
    
    public string CurrentState;
    
    public Node root;

    private void Awake()
    {
        // --- leaf nodes ---
        PatrolNode patrol = new PatrolNode(agentContext, this);
        AttackNode attack = new AttackNode(agentContext, this);
        PickUpNode pickUp = new PickUpNode(agentContext, this);
        Node moveToLastKnown = new MoveToLastKnownPositionNode(agentContext, blackboard, this);

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
            new NeedsWeaponInvertNode(blackboard, patrol)
        });

        // --- only run combat if we see the player ---
        SeePlayerConditionalNode seePlayerCombat =
            new SeePlayerConditionalNode(blackboard, combatSelector);
        
        NeedsWeaponConditionalNode needWeapon = 
            new NeedsWeaponConditionalNode(blackboard, pickUpThenSearch);

        HasLastKnownPositionConditionalNode lastKnown =
            new HasLastKnownPositionConditionalNode(blackboard, moveToLastKnown);

        // --- ROOT ---
        root = new SelectorNode(new List<Node>
        {
            seePlayerCombat,
            needWeapon,
            lastKnown,
            patrol
        });
    }
    
    public void SetState(string state)
    {
        if (!string.IsNullOrEmpty(CurrentState)) return;

        CurrentState = state;

        if (stateText != null)
            stateText.text = state;
    }
}
