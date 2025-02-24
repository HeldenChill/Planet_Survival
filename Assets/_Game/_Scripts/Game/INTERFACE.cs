using System.Collections;
using System.Collections.Generic;

namespace _Game
{
    using Base;
    using System;
    using UnityEngine;
    using Utilities.Core.Data;
    public interface IDamageable
    {
        float TakeDamage(float damage, object source = null);
        CharacterStats Stats { get; }
        Type Type { get; }
        Transform Tf { get; }
        void AttachVFXEffect(EFFECT effect, float time = -1);
        void DetachVFXEffect(EFFECT effect);
        void SpawnHitImpact(HIT_IMPACT_TYPE type);
    }
}
