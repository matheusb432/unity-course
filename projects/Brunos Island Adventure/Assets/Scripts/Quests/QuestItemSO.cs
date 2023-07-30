using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "QuestItem", menuName = "RPG/Quest Item", order = 1)]
    public class QuestItemSO : ScriptableObject
    {
        public string itemName;
    }
}
