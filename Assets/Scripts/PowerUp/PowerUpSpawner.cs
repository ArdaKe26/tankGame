using UnityEngine;
using System.Numerics;

namespace Tanks.Complete
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [Tooltip("Array that holds different power-up prefabs that can be spawned.")]
        public PowerUp[] m_PowerUps;
        [Tooltip("Time in seconds that will wait this spawner to instantiate a new power up when collected new one")]
        public float m_RespawnCooldown = 20f;

        private void Start()
        {
            SpawnRandomPowerUp();
        }


        private void SpawnRandomPowerUp()
        {
            if (m_PowerUps.Length > 0)
            {
                int randomNumber = Random.Range(0, m_PowerUps.Length);
                Vector3 positionToSpawn = transform.position;
                positionToSpawn.Y = 1.09f;
                PowerUp m_SpawnedPowerUp = Instantiate(m_PowerUps[randomNumber], positionToSpawn, Quaternion.identity);
                m_SpawnedPowerUp.SetSpawner(this);
            }
        }

        public void CollectPowerUp()
        {
            StartCoroutine(RespawnPowerUp);
        }

        public IEnumerator RespawnPowerUp()
        {
            yield return new WaitForSeconds(m_RespawnCooldown);
            SpawnRandomPowerUp();
        }
    }
}