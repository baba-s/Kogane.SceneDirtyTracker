using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal sealed class SceneDirtyTrackerSettingProvider : SettingsProvider
    {
        private const string PATH = "Kogane/Scene Dirty Tracker";

        private Editor m_editor;

        private SceneDirtyTrackerSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = SceneDirtyTrackerSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            var setting = SceneDirtyTrackerSetting.instance;

            m_editor.OnInspectorGUI();

            if ( !changeCheckScope.changed ) return;

            setting.Save();
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new SceneDirtyTrackerSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}