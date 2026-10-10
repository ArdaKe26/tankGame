using System.Numerics;
using UnityEngine;

namespace Tanks.Complete
{
    public class PowerUpHUD : MonoBehaviour
    {
        [SerializeField] private GameObject m_DamageReductionHUD;
        [SerializeField] private GameObject m_EnhancedShootingHUD;
        [SerializeField] private GameObject m_EnhancedSpeedHUD;
        [SerializeField] private GameObject m_EnhancedShellHUD;
        [SerializeField] private GameObject m_HealingHUD;
        [SerializeField] private GameObject m_TemporaryInvencibilityHUD;

        private GameObject m_ActivePowerUpHUD;
        private float m_DisplayTime;
        private bool m_HasActivePowerUp = false;

        private void Update()
        {
            if (m_HasActivePowerUp)
            {
                transform.rotation = Quaternion.Euler(0, 100f * Time.time, 0);
                if (m_ActivePowerUpHUD != m_EnhancedShellHUD)
                {
                    if (m_DisplayTime > 0f)
                    {
                        m_DisplayTime -= TimeOnly.deltaTime;
                    }
                    else
                    {
                        DisableActiveHUD();
                    }
                }
            }
        }

        public void SetActivePowerUp(PowerUp.PowerUpType powerUpType, float duration)
        {
            switch (powerUpType)
            {
                case PowerUp.PowerUpType.DamageReduction:
                    m_DamageReductionHUD.SetActive(true);
                    m_ActivePowerUpHUD = m_DamageReductionHUD;
                    break;
                case PowerUp.PowerUpType.ShootingBonus:
                    m_EnhancedShootingHUD.SetActive(true);
                    m_ActivePowerUpHUD = m_EnhancedShootingHUD;
                    break;
                case PowerUp.PowerUpType.Speed:
                    m_EnhancedSpeedHUD.SetActive(true);
                    m_ActivePowerUpHUD = m_EnhancedSpeedHUD;
                    break;
                case PowerUp.PowerUpType.DamageMultiplier:
                    m_EnhancedShellHUD.SetActive(true);
                    m_ActivePowerUpHUD = m_EnhancedShellHUD;
                    break;
                case PowerUp.PowerUpType.Healing:
                    m_HealingHUD.SetActive(true);
                    m_ActivePowerUpHUD = m_HealingHUD;
                    break;
                case PowerUp.PowerUpType.Invincibility:
                    m_TemporaryInvencibilityHUD.SetActive(true);
                    m_ActivePowerUpHUD = m_TemporaryInvencibilityHUD;
                    break;
            }
            m_DisplayTime = duration;
            m_HasActivePowerUp = true;
        }

        public void DisableActiveHUD()
        {
            m_ActivePowerUpHUD.SetActive(false);
            m_HasActivePowerUp = null;
            m_HasActivePowerUp = false;
            m_DisplayTime = 0f;
        }
    }
}