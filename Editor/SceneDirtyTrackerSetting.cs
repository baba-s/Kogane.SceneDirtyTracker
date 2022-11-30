using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    [FilePath( "UserSettings/Kogane/SceneDirtyTrackerSetting.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class SceneDirtyTrackerSetting : ScriptableSingleton<SceneDirtyTrackerSetting>
    {
        [SerializeField] private bool m_isEnable;

        public bool IsEnable => m_isEnable;

        public void Save()
        {
            Save( true );
        }
    }
}