﻿using UnityEngine;

namespace AdventureMage.Actors.Spells
{
    public class Fireball : MonoBehaviour
    {
    
        public int damage = 4;

        public float speed = 3.6f;
        public float duration = 3.6f;

        private Animator anim;
        private AdventureMage.DamageType dmg;

        private void Awake()
        {
            Destroy(gameObject, duration);
            anim = GetComponent<Animator>();

            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            dmg = new AdventureMage.DamageType(damage, null, new string[] {"player"});
        }

        private void FixedUpdate() {
            if (!anim.GetBool("Hit")) {
                transform.position += (transform.right * speed) * transform.localScale.x;
            }
        }

        private void OnTriggerEnter2D(Collider2D obj) {
            if (!anim.GetBool("Hit") && obj.gameObject.tag != "Player") {
                obj.gameObject.BroadcastMessage("takeDamage", dmg, SendMessageOptions.DontRequireReceiver);
                if (!obj.isTrigger) {
                    Destroy(gameObject, 0.3f);
                    anim.SetBool("Hit", true);
                }
            }
        }
    }
}
