using System;
using UnityEngine;

namespace Solcery.FSM
{
    public abstract class Bool : Parameter
    {
        [SerializeField] protected bool outcome;
    }
}
