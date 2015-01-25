using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace AdventureMage.Actors
{

    [RequireComponent(typeof (Player))]
    public class ShootSpell : MonoBehaviour
    {
        public float spellAngle = 98;
        
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
                rot.eulerAngles = new Vector3(0, 0, spellAngle);

                int numParticles = Random.Range(3, 5);
                Quaternion tempRot = Quaternion.identity;
                for (int i = 0; i < numParticles; i++) {
                    Vector3 tempPos = new Vector3(position.x + Random.Range(-0.8f, 0.8f), position.y + Random.Range(-0.5f, 0.5f), position.z); 
                    tempRot.eulerAngles = new Vector3(0, 0, rot.eulerAngles.z + Random.Range(-6, 6));
                    Instantiate(wave, tempPos, tempRot);
                }

                shoot = false;
            }
        }
    }
}
