﻿using System.Collections;
using UnityEngine;

namespace RPG.Character
{
    [CreateAssetMenu(fileName = "Character Stats", menuName = "RPG/Character Stats SO", order = 0)]
    public class CharacterStatsSO : ScriptableObject
    {
        public float health = 100;
        public float damage = 10;
        public float walkSpeed = 1;
        public float runSpeed = 1.25f;
    }
}
