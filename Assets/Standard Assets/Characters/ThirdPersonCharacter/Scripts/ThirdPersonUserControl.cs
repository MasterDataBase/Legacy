using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        [SerializeField] private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        #region MDBVariable
        public GameObject followTransform;
        public float rotationLerp;
        public Quaternion nextRotation;
        public Vector3 nextPosition;
        public float aimValue;
        public float speed = 1f;
        public bool dialogueRunning = false;
        #endregion

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                ///Disable by MDB to not jump
                ///m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            ///MDB was here
            if (dialogueRunning) { return; }
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float v = CrossPlatformInputManager.GetAxis("Vertical");
            float v = CrossPlatformInputManager.GetAxis("Horizontal");
            float h = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            if(h < 0) { 
                h = 0f; 
            }

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                //m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                //m_Move = v * m_CamForward + h * m_Cam.right;

                ///MDB was here
                #region MDB

                #region Player Based Rotation
                //Move the player based on the X input on the controller
                //transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
                //-1 * value.Get<Vector2>();
                //transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 1f, Vector3.up);
                #endregion

                #region Follow Transform Rotation

                //Rotate the Follow Target transform based on the input
                //followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);
                //followTransform.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 1f, Vector3.up);

                #endregion

                #region Vertical Rotation
                //followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);
                //followTransform.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 1f, Vector3.right);

                var angles = followTransform.transform.localEulerAngles;
                angles.z = 0;

                var angle = followTransform.transform.localEulerAngles.x;

                //Clamp the Up/Down rotation
                if (angle > 180 && angle < 340)
                {
                    angles.x = 340;
                }
                else if (angle < 180 && angle > 40)
                {
                    angles.x = 40;
                }


                followTransform.transform.localEulerAngles = angles;
                #endregion
                
                nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

                //if (h == 0 && v == 0)
                //{
                //    nextPosition = transform.position;

                //    if (aimValue == 1)
                //    {
                //        Set the player rotation based on the look transform
                //        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //        reset the y rotation of the look transform
                //        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                //    }

                //    return;
                //}

                //float moveSpeed = speed / 100f;
                //Vector3 position = (transform.forward * h * moveSpeed) + (transform.right * v * moveSpeed);
                //nextPosition = transform.position + position;

                //float moveSpeed = speed / 100f;
                float moveSpeed = 100f;
                m_Move = (transform.forward * h * moveSpeed) + (transform.right * v * moveSpeed);
                //Debug.Log(m_Move + " Value h: " + h + " Value v: " + v + " forward: " + transform.forward + " right: " + transform.right);

                //Set the player rotation based on the look transform
                transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

#endregion
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }

}