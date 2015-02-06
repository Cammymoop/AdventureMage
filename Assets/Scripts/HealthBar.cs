using UnityEngine;
using UnityEngine.UI;

namespace AdventureMage.Utility
{
    public class HealthBar : MonoBehaviour
    {
        public AdventureMage.Actors.Entity target;

        private Slider slider;

        private void Awake() {
            if (!target) {
                target = GameObject.FindWithTag("Player").GetComponent<AdventureMage.Actors.Entity>();
            }
            slider = GetComponent<Slider>();
        }

        private void Update() {
            int health = target.getHealth();
            int maxHealth = target.getMaxHealth();
            slider.value = (float) health / maxHealth;
        }
    }
}
