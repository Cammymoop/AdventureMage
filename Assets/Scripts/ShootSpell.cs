using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace AdventureMage.Actors
{

    [RequireComponent(typeof (Player))]
    public class ShootSpell : MonoBehaviour
    {
        public float spellAngle = 90;
        
        private Player character;
        [SerializeField] Transform wave;
        private bool shoot;

        private void Awake()
        {
            character = GetComponent<Player>();
        }

        private void Update()
        {
            if(!shoot) {
                shoot = CrossPlatformInputManager.GetButtonDown("Fire1");
            }
        }

        private void FixedUpdate()
        {
            if (shoot) {
                Vector3 position = character.getPosition();
                float sign = (character.getFacingRight() ? 1f : -1f);
                position.x = position.x + sign * 2;

                Quaternion rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, sign * spellAngle);

                Instantiate(wave, position, rot);

                shoot = false;
            }
        }
    }
}
