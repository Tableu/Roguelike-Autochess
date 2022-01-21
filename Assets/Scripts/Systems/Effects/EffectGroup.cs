﻿using System.Collections.Generic;
using UnityEngine;

namespace Systems.Effects
{
    [CreateAssetMenu(fileName = "New Effect Group", menuName = "Modifiers/Effect Group", order = 0)]
    public class EffectGroup : ScriptableObject
    {
        public List<IEffect> Effects;
    }
}