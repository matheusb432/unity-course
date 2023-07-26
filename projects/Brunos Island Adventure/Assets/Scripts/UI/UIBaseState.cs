using RPG.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public abstract class UIBaseState
    {
        public abstract void EnterState();
        public abstract void UpdateState();
    }
}
