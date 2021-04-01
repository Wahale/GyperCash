using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator; 
    CharacterController characterController;
    [SerializeField]
    private Joystick joystick; // Fixed Joystick (Joystick Pack / prefabs / Fixed Joystick)

    private float speedPlayer = 5f;
    private Vector3 moveVector;
    private float gravityForce;

    #region Lantern
    [SerializeField]
    Light lantern;

    private float minRange = 10f;
    private float maxRange = 20f;

    private float minSpotAngle = 30f;
    private float maxSpotAngle = 100f;

    /// <summary>
    /// До рекламы
    /// </summary>
    public bool noAds;
    /// <summary>
    /// Поcле рекламы
    /// </summary>
    public bool ads;
    #endregion

    /// <summary>
    /// Игра началась
    /// </summary>
    private bool game;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        joystick.GetComponent<Joystick>();
        joystick.gameObject.SetActive(false);

        lantern.GetComponent<Light>();

        BasicVisibility();
    }

    private void Update()
    {
        if (game)
        {
            Movement();
            Gravity();
        }

        //
        if (noAds)
        {
            BasicVisibility();
            noAds = false;
        }
        if (ads)
        {
            ImprovedVisibility();
            ads = false;
        }
    }

    private void Movement()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * speedPlayer;
        moveVector.z = joystick.Vertical * speedPlayer;

        if (moveVector.x != 0 || moveVector.y != 0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

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
    private void BasicVisibility()
    {
        lantern.range = minRange;
        lantern.spotAngle = minSpotAngle;

        GetComponent<FieldOfView>().Def(minRange,minSpotAngle);
    }

    /// <summary>
    /// После рекламы
    /// </summary>
    private void ImprovedVisibility()
    {
        lantern.range = maxRange;
        lantern.spotAngle = maxSpotAngle;

        GetComponent<FieldOfView>().Def(maxRange, maxSpotAngle);
    }

    // Доступность управления
    public void Game()
    {
        game = true;
        joystick.gameObject.SetActive(true);
    }

    public void NoGame()
    {
        game = false;
        joystick.gameObject.SetActive(false);
    }

    public void Finish()
    {
        game = false;
        joystick.gameObject.SetActive(false);
        animator.SetTrigger("Finish");
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<NPC>().Lost();
        }

        GameObject managerS = GameObject.FindGameObjectWithTag("ManagerScene");
        managerS.GetComponent<SceneManagement_Script>().GameOverVictory();
    }

    public void Dead()
    {
        game = false;
        joystick.gameObject.SetActive(false);
        animator.SetTrigger("Dead");

        GameObject managerS = GameObject.FindGameObjectWithTag("ManagerScene");
        managerS.GetComponent<SceneManagement_Script>().GameOverLost();
    }
}