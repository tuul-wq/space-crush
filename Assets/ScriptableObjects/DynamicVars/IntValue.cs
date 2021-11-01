using System;
using UnityEngine;

namespace ScriptableObjects.DynamicVars
{
    [CreateAssetMenu(menuName = "Int value")]
    public class IntValue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private int initialValue;

        public int RuntimeValue { get; set; }

        private void Awake()
        {
            ResetRuntimeValue();
        }

        public int GetInitValue()
        {
            return initialValue;
        }
        
        public void ResetRuntimeValue()
        {
            RuntimeValue = initialValue;
        }

        public void OnAfterDeserialize()
        {
            ResetRuntimeValue();
        }
        
        public void OnBeforeSerialize() { }
    }
}