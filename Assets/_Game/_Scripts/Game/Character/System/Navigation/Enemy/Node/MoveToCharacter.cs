using _Game.Character;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToCharacter", story: "[Navigation] Move [Agent] To [Character]", category: "Action/Blackboard", id: "3fd6775851b5117f44cc9357ff4be880")]
public partial class MoveToCharacterAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy1AI> Navigation;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Character;
    protected override Status OnStart()
    {
        Character.Value = Navigation.Value.NavParameter.Player.Tf; 
        return Character.Value != null ? Status.Running : Status.Failure;
    }

    protected override Status OnUpdate()
    {
        Vector2 rawDirection = Character.Value.position - Agent.Value.transform.position;
        Vector2 moveDirection = Vector3.ProjectOnPlane(rawDirection, Character.Value.up).normalized;
        Navigation.Value.NavData.MoveDirection = moveDirection;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

