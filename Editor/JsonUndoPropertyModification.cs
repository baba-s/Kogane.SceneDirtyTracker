using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedField.Local

namespace Kogane.Internal
{
    [Serializable]
    internal struct JsonUndoPropertyModification
    {
        [SerializeField] private JsonPropertyModification previousValue;
        [SerializeField] private JsonPropertyModification currentValue;
        [SerializeField] private bool                     keepPrefabOverride;

        public JsonUndoPropertyModification( UndoPropertyModification modification )
        {
            previousValue      = new( modification.previousValue );
            currentValue       = new( modification.currentValue );
            keepPrefabOverride = modification.keepPrefabOverride;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson( this, true );
        }
    }
}