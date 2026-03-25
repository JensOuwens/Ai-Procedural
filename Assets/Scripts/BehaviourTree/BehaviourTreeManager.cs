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
        PatrolNode patrol = new PatrolNode(agentContext, this);
        AttackNode attack = new AttackNode(agentContext, this);
        PickUpNode pickUp = new PickUpNode(agentContext, this);
        Node moveToLastKnown = new MoveToLastKnownPositionNode(agentContext, blackboard, this);
        
        HasWeaponConditionalNode hasWeaponAttack =
            new HasWeaponConditionalNode(blackboard, attack);
        
        SequenceNode pickUpThenSearch = new SequenceNode(new List<Node>
        {
            pickUp,
            moveToLastKnown,
            attack
        });
        
        SelectorNode combatSelector = new SelectorNode(new List<Node>
        {
            hasWeaponAttack,
            new NeedsWeaponInvertNode(blackboard, patrol)
        });
        
        SeePlayerConditionalNode seePlayerCombat =
            new SeePlayerConditionalNode(blackboard, combatSelector);
        
        NeedsWeaponConditionalNode needWeapon = 
            new NeedsWeaponConditionalNode(blackboard, pickUpThenSearch);

        HasLastKnownPositionConditionalNode lastKnown =
            new HasLastKnownPositionConditionalNode(blackboard, moveToLastKnown);
        
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
