using _Game.Character;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckCharacterInAttackRange", story: "[NavigationModule] Check [Self] In Attack Range With [Character]", category: "Action", id: "f2fb1905eac258c4606fc80976a2edc0")]
public partial class CheckCharacterInAttackRange : Action
{
    [SerializeReference] public BlackboardVariable<Enemy1AI> NavigationModule;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Character;

    float sqrAttackRange;
    float sqrDistance;
    EnemyStats stats;
    protected override Status OnStart()
    {
        stats = NavigationModule.Value.NavParameter.GetStats<EnemyStats>();
        sqrAttackRange = stats.AttackRange * stats.AttackRange;
        Character.Value = NavigationModule.Value.NavParameter.Player.transform;
        sqrDistance = (Character.Value.position - Self.Value.transform.position).sqrMagnitude;

        if(sqrDistance <= sqrAttackRange)
        {
            return Status.Failure;
        }      
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

