using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        public bool Player1;

        //GameObject player attack colliders

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            if (!Player1)
            {
                m_Character.Flip();
            }
        }


        private void Update()
        {
            if (!m_Jump && Player1)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            else if (!m_Jump && !Player1)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump2");
            }

            //if press this button then start coroutine ienumerator player attack functions
        }


        private void FixedUpdate()
        {
            if (Player1)
            {
                // Read the inputs.
                bool crouch = Input.GetKey(KeyCode.S);
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                // Pass all parameters to the character control script.
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }
            else if (!Player1)
            {
                bool crouch = Input.GetKey(KeyCode.DownArrow);
                float h = CrossPlatformInputManager.GetAxis("Horizontal2");
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }
        }

        //ienumerator player attack functions and collider delay function
        //wait for seconds .5 for player attack and 2 for collider delay

        //player attack collider true or animation triggered
        //yield return new waitforseconds()
        //player attack collider false

        //make player attacks deal damage lol, this will probably be an ontrigger2d function application
    }
}
