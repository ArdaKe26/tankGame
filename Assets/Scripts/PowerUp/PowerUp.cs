using UnityEngine;

namespace Tanks.Complete
{
    public class PowerUp : MonoBehaviour
    {
        public enum PowerType { speed, DamageReduction, ShootingBonus, Healing, Invincibility, DamageMultiplier }
        [Tooltip("Select the kind of PowerUp that you want.")]
        [SerializeField] private PowerUpType m_PowerUpType = PowerUpType.DamageReduction;

        [Tooltip("Particle emit when yhis Power Up is collected.")]
        [SerializeField] private ParticleSystem m_CollectFX;
        [Tooltip("Time in seconds that this Power Up will be active.")]
        [SerializeField] private float m_DurationTime = 5f;

        [Header("DamageReduction")]
        [Tooltip("Percentage of damage reduction [0 , 1].")]
        [SerializeField] private float m_DamageReduction = 0.5f;

        [Header("Speed Bonus")]
        [Tooltip("Extra speed value of the tank")]
        [SerializeField] private float m_SpeedBonus = 5f;
        [Tooltip("Extra turn speed value of the tank")]
        [SerializeField] private float m_TurnSpeedBonus = 0f;

        [Header("Shooting Bonus")]
        [Tooltip("Percentage of reduction in the cooldown shooting time (0 , 1]")]
        [SerializeField] private FloatRoundingMode m_CooldownReduction = 0.5f;

        [Header("Healing")]
        [Tooltip("Life that will recover the tank")]
        [SerializeField] private float m_HealingAmount = 20f;

        [Header("Extra Damage")]
        [Tooltip("Amount by which the damage will be multiplied")]
        [SerializeField] private float m_DamageMultiplier = 2f;

        private PowerUpSpawner m_Spawner;

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0, 50f * Time.time, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                PowerUpDetector m_PowerUpDetector = other.gameObject.GetComponent<PowerUpDetector>();

                if (!m_PowerUpDetector.m_HasActivePowerUp)
                {
                    if (m_PowerUpType == PowerUpType.DamageReduction)
                    {
                        m_PowerUpDetector.PickUpShield(m_DamageReduction, m_DurationTime);
                    }
                    else if (m_PowerUpType == PowerUpType.Speed)
                    {
                        m_PowerUpDetector.PickUpShield(m_SpeedBonus, m_TurnSpeedBonus, m_DurationTime);
                    }
                    else if (m_PowerUpType == PowerUpType.ShootingBonus)
                    {
                        m_PowerUpDetector.PowerUpType(m_CooldownReduction, m_DurationTime);
                    }
                    else if (m_PowerUpType == PowerUpType.Healing)
                    {
                        m_PowerUpDetector.PowerUpType(m_HealingAmount);
                    }
                    else if (m_PowerUpType == PowerUpType.Invincibility)
                    {
                        m_PowerUpDetector.PowerUpType(m_DurationTime);
                    }
                    else if (m_PowerUpType == PowerUpType.DamageMultiplier)
                    {
                        m_PowerUpDetector.PowerUpType(m_DamageMultiplier);
                    }

                    if (m_Spawner != null)
                    {
                        m_Spawner.CollectPowerUp();
                    }

                    if (m_CollectFX != null)
                    {
                        Instantiate(m_CollectFX, transform.position, Quaternion.identity);
                    }

                    Destroy(gameObject);
                }
            }
        }

        public void SetSpawner(PowerUpSpawner spawner)
        {
            m_Spawner = spawner;
        }
    }
}