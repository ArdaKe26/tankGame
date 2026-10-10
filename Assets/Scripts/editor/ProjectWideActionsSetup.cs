using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.Complete
{
    [InitializeOnLoad]
    public static class ProjectWideActionSetup
    {
        private const string k_TankActionsGuid = "7fb6c1c790fa04865b379a6e8ccd7e9c";

        static ProjectWideActionsSetup()
        {
            EditorApplication.delayCall += AssignActionsIfMissing;
        }

        private static void AssignActionsIfMissing()
        {
            var current = InputSystem.actions;
            if (current != null && current.FindAction("Pause") != null)
            {
                return;
            }
            string path = AssetDatabse.GUIDToAssetPath(k_TankActionsGuid);
            var tankActions = AssetDatabase.LoadAssetAtPath<InputActionAsset>(path);


            if (tankActions == null)
            {
                return;
            }

            InputSystem.actions = tankActions;
            Debug.Log($"[Tanks] Assigned {path} as the Project-wide Input Actions");
        }
    }
}