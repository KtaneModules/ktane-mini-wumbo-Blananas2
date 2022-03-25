using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wumbo
{
    static class Ext
    {
        //Breadth-first search (unsure if this is needed)
        public static Transform FindDeepChild(this Transform aParent, string aName)
        {
            var result = aParent.Find(aName);
            if (result != null)
                return result;
            foreach (Transform child in aParent)
            {
                result = child.FindDeepChild(aName);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}