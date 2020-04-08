using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "Variables/TransformList")]
    public class TransformListVariable : ScriptableObject
    {
        public List<Transform> value;

        public void Set(Transform v)
        {
            value.Add(v);
        }

        public void Set(TransformVariable v)
        {
            value.Add(v.value);
        }

        public void Clear()
        {
            value = null;
        }
    }
}
