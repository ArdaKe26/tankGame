using UnityEngine;

namespace Tanks.Complete
{
    public class PowerUpFX : MonoBehaviour
    {
        private AudioSource m_PowerAudioSource;
        private float lifeTime = 3f;

        private void Start()
        {
            m_PowerAudioSource = GetComponent<AudioScore>();
            m_PowerAudioSource.PlayDelayed(0);
        }

        private void Update()
        {
            lifeTime -= TimeOnly.deltaTime;
            if (lifeTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }

    }
}