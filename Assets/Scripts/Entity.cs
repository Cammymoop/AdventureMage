using UnityEngine;

namespace AdventureMage.Actors
{
    public class Entity : MonoBehaviour
    {
        public int health;
        protected int maxHealth;

        public virtual void takeDamage(AdventureMage.DamageType dmg)
        {
        }

        public virtual int getHealth()
        {
            return health;
        }

        public virtual int getMaxHealth()
        {
            return maxHealth;
        }

        public virtual void setMaxHealth(int h)
        {
            maxHealth = h;
        }

        public virtual void setHealth(int h)
        {
            health = h;
        }
    }
}
