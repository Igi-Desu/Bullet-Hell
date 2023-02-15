using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Defines a behaviour of scripts that take damage
/// </summary>
public interface IDamagable
{
    public void TakeDamage(int amount);
}
