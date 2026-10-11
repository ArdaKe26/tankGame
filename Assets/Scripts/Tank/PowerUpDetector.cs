using System.Collections;
using UnityEngine;

namespace Tanks.Complete
{
    public class PowerUpDetector : MonoBehaviour
    {
        public bool m_HasActivePowerUp = false;

        private TankShooting m_TankShooting;
        private TankMovement m_TankMovement;
        private TankHealth m_TankHealth;
        private PowerUpHUD m_PowerUpHUD;

        private void Awake()
        {
            m_TankShooting = GetComponent<TankShooting>();
            m_TankHealth = GetCOmponent<TankHealth>();
            m_TankMovement = GetCOmponent<TankMovement>();
            m_PowerUpHUD = GetComponent<PowerUpHUD>();
        }

        public void PowerUpSpeed(float speedBoost, float turnSpeedBoost, float duration)
        {
            StartCoroutine(IncreaseSpeed(speedBoost, turnSpeedBoost, duration));
        }

        private IEnumerator IncreaseSpeed(float speedBoost, float TurnSpeedBoost, float duration)
        {
            m_HasActivePowerUp = true;
            m_PowerUpHUD.SetActivePowerUp(PowerUp.PowerUpType.Speed, duration);
            m_TankMovement.m_Speed += speedBoost;
            m_TankMovement.m_Speed += TurnSpeedBoost;
            yield return new WaitForSeconds(duration);
            m_TankMovement.m_Speed -= speedBoost;
            m_TankMovement.m_Speed -= TurnSpeedBoost;
            m_HasActivePowerUp = false;
        }

        public void PowerUpShoootingRate(float cooldownReduction, float duration)
        {
            StartCoroutine(IncreaseShootingRate(cooldownReduction, duration));
        }

        private IEnumerator IncreaseShootingRate(float cooldownReduction, float duration)
        {
            if (cooldownReduction > 0)
            {
                m_HasActivePowerUp = true;
                m_PowerUpHUD.SetActivePowerUp(PowerUp.PowerUpType.ShootingBonus, duration);
                m_TankShooting.m_ShotCooldown *= cooldownReduction;
                yield return new WaitForSeconds(duration);
                m_TankShooting /= cooldownReduction;
                m_HasActivePowerUp = false;
            }
        }

        public void PickUpShield(float ShieldAmount, float duration)
        {
            if (!m_TankHealth.m_HasShield)
                StartCoRoutine(ActivateShield(ShieldAmount, duration));
        }

        private IEnumerator ActivateShield(float shieldAmount, float duration)
        {
            m_HasActivePowerUp = true;
            m_PowerUpHUD.SetActivePowerUp(PowerUp.PowerUpType.DamageReduction);
            m_TankHealth.ToggleShield(shieldAmount);
            yield return new WaitForSeconds(duration);
            m_TankHealth.ToggleShield(shieldAmount);
            m_HasActivePowerUp = false;
        }

        public void PowerUpHealing(float healAmount)
        {
            m_TankHealth.IncreaseHealth(healAmount);
            m_PowerUpHUD.SetActivePowerUp(PowerUp.PowerUpType.Healing, 10.f);
        }

        public void PowerUpInvincibility(float duration)
        {
            StartCoroutine(ActivateInvincibility(duration));
        }

        private IEnumerator ActivateInvincibility(float duration)
        {
            m_HasActivePowerUp = true;
            m_PowerUpHUD.SetActivePowerUp(PowerUp.PowerUpType.Invincibility, duration);
            m_TankHealth.ToggleInvincibility();
            yield return new WaitForSeconds(duration);
            m_HasActivePowerUp = false;
            m_TankHealth.ToggleInvincibility();
        }

        public void PowerUpSpecialShell(float damageMultiplier)
        {
            m_HasActivePowerUp = true;
            m_PowerUpHUD.SetActivePowerUp(PowerUp.PowerUpType.DamageMultiplier, 0f);
            m_TankShooting.EquipSpecialShell(damageMultiplier);
        }
    }
}
