using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace AdventureMage.Actors
{

    [RequireComponent(typeof (Player))]
    public class ShootSpell : MonoBehaviour
    {
        public string spellButton = "Fire1";
        public float spellAngle = 98;
		public float delayTime;
		private float lastTimeShot= 0;
        public float angleVarMin = 6f;
        public float angleVarMax = 6f;
        public float posVariation = 0.5f;
        public int minNum = 3;
        public int maxNum = 5;

        public float deltaX = 2.0f;
        public float deltaY = 0.0f;
        
        private Player character;
        private Animator anim;
        [SerializeField] Transform wave;
        private bool shoot;

        private void Awake()
        {
            character = GetComponent<Player>();
            lastTimeShot = delayTime;

            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if(!shoot) {
                shoot = CrossPlatformInputManager.GetButtonDown(spellButton);
            }
        }

        private void FixedUpdate()
        {
            lastTimeShot += Time.deltaTime;
        	if (lastTimeShot < delayTime){
        		shoot = false;
        		return;
        	}
            if (shoot) {
                lastTimeShot = 0;
                anim.SetBool("Shooting", true);
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
                    tempRot.eulerAngles = new Vector3(0, 0, rot.eulerAngles.z + (Random.Range(angleVarMin, angleVarMax) * sign));
                    Object obj = Instantiate(wave, tempPos, tempRot);
                    Transform gObj = obj as Transform;
                    gObj.localScale = new Vector3(sign * gObj.localScale.x, gObj.localScale.y, 1);
                }

                shoot = false;
            }
        }
    }
}
