using UnityEditor;

namespace Tanks.Complete
{
    [CustomEditor(typeof(Complete.PowerUp))]
    public class PowerUpEditor : Editor
    {
        SerializedProperty powerUpType;
        SerializedProperty durationTime;
        SerializedProperty collectFX;
        SerializedProperty speedBonus, turnSpeedBonus;
        SerializedProperty damageReduce;
        SerializedProperty cooldownReduction;
        SerializedProperty healingAmount;
        SerializedProperty damageMultiplier;

        private void OnEnable()
        {
            powerUpType = serializedObject.FindProperty("m_PowerUpType");
            durationTime = serializedObject.FindProperty("m_DurationTime");
            collectFX = serializedObject.FindProperty("m_CollectFX");
            speedBonus = serializedObject.FindProperty("m_SpeedBonus");
            turnSpeedBonus = serializedObject.FindProperty("m_TurnSpeedBonus");
            damageReduce = serializedObject.FindProperty("m_DamageReduce");
            cooldownReduction = serializedObject.FindProperty("m_CooldownReduction");
            healingAmount = serializedObject.FindProperty("m_HealingAmount");
            damageMultiplier = serializedObject.FindProperty("m_DamageMultiplier");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(powerUpType);
            EditorGUILayout.PropertyField(collectFX);

            PowerUp.PowerUpType selectedType = (PowerUp.PowerUpType)powerUpType.enumValueIndex;

            switch (selectedType)
            {
                case PowerUp.PowerUpType.Speed:
                    EditorGUILayout.PropertyField(durationTime);
                    EditorGUILayout.PropertyField(speedBonus);
                    EditorGUILayout.PropertyField(turnSpeedBonus);
                    break;

                case PowerUp.PowerUpType.DamageReduction:
                    EditorGUILayout.PropertyField(durationTime);
                    EditorGUILayout.PropertyField(damageReduce);
                    break;

                case PowerUp.PowerUpType.ShootingBonus:
                    EditorGUILayout.PropertyField(durationTime);
                    EditorGUILayout.PropertyField(cooldownReduction);
                    break;

                case PowerUp.PowerUpType.Healing:
                    EditorGUILayout.PropertyField(healingAmount);
                    break;

                case PowerUp.PowerUpType.damageMultiplier:
                    EditorGUILayout.PropertyField(damageMultiplier);
                    break;

                case PowerUp.PowerUpType.Invincibility:
                    EditorGUILayout.PropertyField(durationTime);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
}
