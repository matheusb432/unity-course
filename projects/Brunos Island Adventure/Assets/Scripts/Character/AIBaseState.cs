using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Character
{
    // TODO refactor - to interface?
    public abstract class AIBaseState
    {
        public abstract void EnterState(EnemyController enemy);
        public abstract void UpdateState(EnemyController enemy);
    }
}
