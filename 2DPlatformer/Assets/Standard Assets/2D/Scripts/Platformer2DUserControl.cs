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

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
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
    }
}
