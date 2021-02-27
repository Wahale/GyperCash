using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField]
    Joystick Fixedjoystick; // (Joystick Pack/prefabs/Fixed Joystick)

    public static int hp;
    private int maxHP = 10;

    public static float speedPlayer;
    private float minSpeed = 5f;
    private float maxSpeed = 10f;

    private Vector3 moveVector;
    private float gravityForce;

    #region Lantern(фонарь)

    [SerializeField]
    Light lantern;

    // Диапазон свечения фонаря
    public static float rangeLantern;
    private float minRange = 10f;
    private float maxRange = 20f;
    // Угол свечения фонаря
    public static float spotAngle;
    private float minAngle = 30f;
    private float maxAngle = 100f;

    #endregion

    /// <summary>
    /// После рекламы
    /// </summary>
    public bool ads;
    /// <summary>
    /// До рекламы
    /// </summary>
    public bool noAds;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Fixedjoystick.GetComponent<Joystick>();

        hp = maxHP;
        speedPlayer = minSpeed;
        rangeLantern = minRange;
        lantern.spotAngle = minAngle;
    }

    private void Update()
    {
        Movement();
        Gravity();

        if (ads)
        {
            ADSPlayer();
            ads = false;
        }
        if (noAds)
        {
            DefoultPlayer();
            noAds = false;
        }
    }

    private void Movement()
    {
        moveVector = Vector3.zero;

        moveVector.x = Fixedjoystick.Horizontal * speedPlayer;
        moveVector.z = Fixedjoystick.Vertical * speedPlayer;

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

    /// <summary>
    /// Без улучшения
    /// </summary>
    private void DefoultPlayer()
    {
        speedPlayer = minSpeed;
        rangeLantern = minRange;
        lantern.spotAngle = minAngle;
    }

    /// <summary>
    /// С улучшением
    /// </summary>
    private void ADSPlayer()
    {
        speedPlayer = maxSpeed;
        rangeLantern = maxRange;
        lantern.spotAngle = maxAngle;
    }
}