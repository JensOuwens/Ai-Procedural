using UnityEngine;

public class GuardAgent : MonoBehaviour, IAgent
{
    [SerializeField] private BehaviourTreeManager behaviourTreeManager;
    public bool HasWeapon { get; set; }

    private void Update()
    {
        behaviourTreeManager.CurrentState = "";
        behaviourTreeManager.root.Execute();
    }
}
