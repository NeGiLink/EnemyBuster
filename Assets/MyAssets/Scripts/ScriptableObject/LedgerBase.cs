using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    public class LedgerBase<T> : ScriptableObject
    {
        [SerializeField]
        private T[] values;
        public T[] Values { get => values; }
        public T this[int i] { get => values[i]; }
        public int Count { get => values.Length; }
    }
}
