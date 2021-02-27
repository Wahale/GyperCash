using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField]
    Joystick joystick; // Fixed Joystick (Joystick Pack / prefabs / Fixed Joystick)

    private float speedPlayer = 5f;
    private Vector3 moveVector;
    private float gravityForce;

    #region Lantern

    [SerializeField]
    Light lantern;

    private float defValue = 30f;
    private float adsValue = 100f;

    /// <summary>
    /// Полсе реламы
    /// </summary>
    public bool ads;
    /// <summary>
    /// До рекламы
    /// </summary>
    public bool noAds;

    #endregion

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        joystick.GetComponent<Joystick>();

        lantern.GetComponent<Light>();

        DefoultLantern();
    }

    private void Update()
    {
        Movement();
        Gravity();

        //
        if (ads)
        {
            DefoultLantern();
            ads = false;
        }
        if (noAds)
        {
            ADSLantern();
            noAds = false;
        }
    }

    private void Movement()
    {
        moveVector = Vector3.zero;

        moveVector.x = joystick.Horizontal * speedPlayer;
        moveVector.z = joystick.Vertical * speedPlayer;

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedPlayer, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        moveVector.y = gravityForce;
        characterController.Move(moveVector * Time.deltaTime);
    }

    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            gravityForce -= 20f * Time.deltaTime;
        }
        else
        {
            gravityForce = -1f;
        }
    }

    // фонарик
    /// <summary>
    /// До рекламы
    /// </summary>
    private void DefoultLantern()
    {
        lantern.GetComponent<Light>().spotAngle = defValue;
    }

    /// <summary>
    /// После рекламы
    /// </summary>
    private void ADSLantern()
    {
        lantern.GetComponent<Light>().spotAngle = adsValue;
    }
}