using System;
using System.Collections;
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
        private bool P1Attacking = false;
        private bool P2Attacking = false;

        public Animator anim;

        //GameObject player attack colliders
        public GameObject P1Punch, P1Kick, P2Punch, P2Kick;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            if (!Player1)
            {
                m_Character.Flip();
            }
        }

        void Start()
        {
            Time.timeScale = 1f;
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
            if(Player1)
            {
                if (Input.GetKeyDown(KeyCode.N) && (!P1Attacking))
                {
                    StartCoroutine(P1PunchAttack());
                    P1Attacking = true;
                    anim.SetBool("Punch", P1Attacking);
                    Debug.Log("Attacking");
                    StartCoroutine(PunchAttackDelay());
                    StartCoroutine(EndLag());
                }
                else if (Input.GetKeyDown(KeyCode.M) && (!P1Attacking))
                {
                    StartCoroutine(P1KickAttack());
                    P1Attacking = true;
                    anim.SetBool("Kick", P1Attacking);
                    StartCoroutine(KickAttackDelay());
                    StartCoroutine(EndLag());
                }
            }
            else if (!Player1)
            {
                if (Input.GetKeyDown("[2]") && (!P2Attacking))
                {
                    StartCoroutine(P2PunchAttack());
                    P2Attacking = true;
                    StartCoroutine(StrikeAttackDelay());
                    StartCoroutine(EndLag());
                }
                else if (Input.GetKeyDown("[3]") && (!P2Attacking))
                {
                    StartCoroutine(P2KickAttack());
                    P2Attacking = true;
                    StartCoroutine(KickAttackDelay());
                    StartCoroutine(EndLag());
                }
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

        //ienumerator player attack functions and collider delay function
        //wait for seconds .5 for player attack and 2 for collider delay

        IEnumerator P1PunchAttack()
        {
            //Double Hit, weaker damage per punch
            yield return new WaitForSeconds(.2f);
            P1Punch.SetActive(true);
            yield return new WaitForSeconds(.06f);
            P1Punch.SetActive(false);
            yield return new WaitForSeconds(.09f);
            P1Punch.SetActive(true);
            yield return new WaitForSeconds(.03f);
            P1Punch.SetActive(false);
        }

        IEnumerator P1KickAttack()
        {
            //Stronger attack, wind up, single hit
            yield return new WaitForSeconds(.6f);
            P1Kick.SetActive(true);
            yield return new WaitForSeconds(.2f);
            P1Kick.SetActive(false);
        }

        IEnumerator P2PunchAttack()
        {
            //trigger anim, set below gameobject to empty colliders sized to the anim
            P2Punch.SetActive(true);
            yield return new WaitForSeconds(.33f);
            P2Punch.SetActive(false);
        }

        IEnumerator P2KickAttack()
        {
            //trigger anim, set below gameobject to empty coliders sized to the anim
            P2Kick.SetActive(true);
            yield return new WaitForSeconds(.33f);
            P2Kick.SetActive(false);
        }

        IEnumerator PunchAttackDelay()
        {
            Debug.Log("Waiting");
            yield return new WaitForSeconds(.38f);
            Debug.Log("Waited");

                P1Attacking = false;
                anim.SetBool("Punch", P1Attacking);
                Debug.Log("Not Attacking");
        }

        IEnumerator StrikeAttackDelay()
        {
            Debug.Log("Waiting");
            yield return new WaitForSeconds(.38f);
            Debug.Log("Waited");

            P2Attacking = false;
            anim.SetBool("Punch", P1Attacking);
            Debug.Log("Not Attacking");
        }

        IEnumerator KickAttackDelay()
        {
            Debug.Log("Waiting");
            yield return new WaitForSeconds(.33f);
            Debug.Log("Waited");

            if (Player1)
            {
                P1Attacking = false;
                anim.SetBool("Kick", P1Attacking);
                Debug.Log("Not Attacking");
            }
            else if (!Player1)
            {
                P2Attacking = false;
                anim.SetBool("Kick", P2Attacking);
                Debug.Log("Not Attacking");
            }
        }

        IEnumerator EndLag()
        {
            Debug.Log("Waiting");
            P1Attacking = true;
            yield return new WaitForSeconds(.5f);
            P1Attacking = false;
            Debug.Log("Waited");
        }

        //weird bug where sometimes the attack doesn't register as hit, I think it has to do with it being triggered in ontrigger2d instead of under update, need something more
        //consistently checking, but not sure how to implement it
        //try using Raycast2D and using the distance of the collider as the distance to check for
    }
}
