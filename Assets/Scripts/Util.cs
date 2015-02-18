using UnityEngine;

namespace AdventureMage.Util
{
    public class Math {
        public static float angleFromVector(Vector2 vec) {
            float angle = Mathf.Atan(vec.y / vec.x) * Mathf.Rad2Deg;
            if (vec.x < 0) {
                angle += 180;
            }
            return angle;
        }
    }
}
