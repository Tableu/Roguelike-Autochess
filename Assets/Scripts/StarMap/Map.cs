﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace StarMap
{
    [CreateAssetMenu(fileName = "Map", menuName = "Map/StarMap", order = 0)]
    public class Map : ScriptableObject
    {
        public List<StarSystem> StarSystems;
        public List<SystemPair> SystemPairs;

        [Serializable]
        public struct SystemPair
        {
            public StarSystem System1;
            public StarSystem System2;
        }
    }
}