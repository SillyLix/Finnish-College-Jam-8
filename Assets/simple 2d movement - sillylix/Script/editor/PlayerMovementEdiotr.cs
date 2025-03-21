#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerMovement2D))]
public class PlayerMovementEditor : Editor
{
    private bool showMovementSettings = true;
    private bool showJumpSettings = false;
    private bool showDashSettings = false;

    public override void OnInspectorGUI()
    {
        PlayerMovement2D playerMovement2D = (PlayerMovement2D)target;
        SerializedObject serializedObject = new SerializedObject(playerMovement2D);

        // Movement Settings
        showMovementSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showMovementSettings, "Movement Settings");
        if (showMovementSettings)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("horizontalMovementNeeded"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("verticalMovementNeeded"));

            if (playerMovement2D.verticalMovementNeeded)
            {
                playerMovement2D.jumpNeeded = false;
                playerMovement2D.doubleJumpNeeded = false;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("playerSpeed"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // Jump Settings
        showJumpSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showJumpSettings, "Jump Settings");
        if (showJumpSettings)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpNeeded"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("doubleJumpNeeded"));

            if (playerMovement2D.jumpNeeded || playerMovement2D.doubleJumpNeeded)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpForce"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("gravityScale"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("groundDetectorLength"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("groundLayer"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("drawJumpDetector"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpKey"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpSound"));

                if (playerMovement2D.groundLayer == 0)
                {
                    EditorGUILayout.HelpBox("Ground Layer is not assigned. Assign a layer to 'Ground Layer' for jumping to work.", MessageType.Warning);
                }
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // Dash Settings
        showDashSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showDashSettings, "Dash Settings");
        if (showDashSettings)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dashEnabled"));

            if (playerMovement2D.dashEnabled)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("dashSpeed"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("dashDuration"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("dashKey"));
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
