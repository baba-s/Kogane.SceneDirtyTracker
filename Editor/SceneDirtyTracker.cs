using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class SceneDirtyTracker
    {
        static SceneDirtyTracker()
        {
            EditorSceneManager.sceneDirtied += OnSceneDirtied;
            Undo.postprocessModifications   += OnPostprocessModifications;
        }

        private static void OnSceneDirtied( Scene scene )
        {
            if ( !SceneDirtyTrackerSetting.instance.IsEnable ) return;

            Debug.Log( $"EditorSceneManager.sceneDirtied: `{scene.name}`" );
        }

        private static UndoPropertyModification[] OnPostprocessModifications( UndoPropertyModification[] modifications )
        {
            if ( !SceneDirtyTrackerSetting.instance.IsEnable ) return modifications;

            Debug.Log
            (
                $@"Undo.postprocessModifications:

```
{string.Join( "\n", modifications.Select( x => new JsonUndoPropertyModification( x ) ) )}
```"
            );

            return modifications;
        }
    }
}