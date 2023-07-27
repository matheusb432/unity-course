using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Util
{
    public static class Utils
    {
        public static int ToIndex(int index, int count) => Mathf.Clamp(index, 0, count - 1);
    }
}
