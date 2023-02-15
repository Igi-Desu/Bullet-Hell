using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Defines a behaviour of scripts that deal damage
/// </summary>
public interface IDamager
{
    public void DealDamage(IDamagable damagable);
}
