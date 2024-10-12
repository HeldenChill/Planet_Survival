using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Core.Data;

namespace _Game
{
    public interface IDamageable
    {
        float TakeDamage(float damage, object source);
        CharacterStats Stats { get; }
        Type Type { get; }
    }
}
