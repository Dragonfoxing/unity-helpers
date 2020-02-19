using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragonfoxing.UnityCSharp
{
    public class GameEntity : MonoBehaviour, iGameEntity
    {
        public bool Paused { get; set; } = false;
    }
}