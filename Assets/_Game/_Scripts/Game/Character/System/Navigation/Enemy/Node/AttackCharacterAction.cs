using _Game.Character;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackCharacter", story: "[NavigationModule] Attack [Character]", category: "Action", id: "56423b87f64f12c80cad45730db5e9ee")]
public partial class AttackCharacterAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy1AI> NavigationModule;
    [SerializeReference] public BlackboardVariable<Transform> Character;

    protected override Status OnStart()
    {
        NavigationModule.Value.NavData.MoveDirection = Vector2.zero;
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

