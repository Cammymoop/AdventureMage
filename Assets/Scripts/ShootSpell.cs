using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace AdventureMage.Actors
{

    [RequireComponent(typeof (Player))]
    public class ShootSpell : MonoBehaviour
    {
        public float spellAngle = 98;

        public float angleVarMin = 6f;
        public float angleVarMax = 6f;
        public float posVariation = 0.5f;
        public int minNum = 3;
        public int maxNum = 5;

        public float deltaX = 2.0f;
        public float deltaY = 0.0f;
        
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
                position.x += sign * deltaX;
                position.y += deltaY;

                Quaternion rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, sign * spellAngle);

                int numParticles = Random.Range(minNum, maxNum);
                Quaternion tempRot = Quaternion.identity;
                for (int i = 0; i < numParticles; i++) {
                    Vector3 tempPos = new Vector3(position.x + Random.Range(-posVariation, posVariation), position.y + Random.Range(-posVariation, posVariation), position.z); 
                    tempRot.eulerAngles = new Vector3(0, 0, rot.eulerAngles.z + Random.Range(angleVarMin, angleVarMax));
                    Object obj = Instantiate(wave, tempPos, tempRot);
                    Transform gObj = obj as Transform;
                    gObj.localScale = new Vector3(sign, 1, 1);
                }

                shoot = false;
            }
        }
    }
}
