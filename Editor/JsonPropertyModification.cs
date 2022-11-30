using System;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedField.Local

namespace Kogane.Internal
{
    [Serializable]
    internal struct JsonPropertyModification
    {
        [SerializeField] private string target;
        [SerializeField] private string propertyPath;
        [SerializeField] private string value;
        [SerializeField] private string objectReference;

        public JsonPropertyModification( PropertyModification modification )
        {
            target          = GetName( modification.target );
            propertyPath    = modification.propertyPath;
            value           = modification.value;
            objectReference = GetName( modification.objectReference );
        }

        private static string GetName( UnityEngine.Object obj )
        {
            if ( obj == null ) return "";
            if ( EditorUtility.IsPersistent( obj ) ) return AssetDatabase.GetAssetPath( obj );
            if ( obj is GameObject gameObject ) return GetHierarchyPath( gameObject );
            if ( obj is Component component ) return GetHierarchyPath( component.gameObject );
            return obj.name;
        }

        private static string GetHierarchyPath( GameObject gameObject )
        {
            var path   = gameObject.name;
            var parent = gameObject.transform.parent;

            while ( parent != null )
            {
                path   = parent.name + "/" + path;
                parent = parent.parent;
            }

            return path;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson( this, true );
        }
    }
}