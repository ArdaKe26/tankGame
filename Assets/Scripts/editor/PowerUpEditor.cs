using UnityEditor;

namespace Tanks.Complete
{
    [CustomEditor(typeof(Complete.PowerUp))]
    public class PowerUpEditor : Editor
    {
        SerializedProperty powerUpType;
        SerializedProperty durationTime;
        SerializedProperty collectfx;
        SerializedProperty speedBonus, turnSpeedBonus;
        SerializedProperty damageReduce;
        SerializedProperty cooldownReduction;
        SerializedProperty healingAmount;
        SerializedProperty damageMultiplier;

    }
}
