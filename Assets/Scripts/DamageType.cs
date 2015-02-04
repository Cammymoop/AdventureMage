using UnityEngine;

namespace AdventureMage {
    public class DamageType
    {
        public int damage;
        public string[] types;
        public string[] immune;

        public DamageType(int dmg, string[] dmgTypes = null, string[] dmgImmune = null) {
            damage = dmg;
            types = dmgTypes;
            immune = dmgImmune;
        }
    }
}
