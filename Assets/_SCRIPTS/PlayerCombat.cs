using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] SpriteRenderer ArrowGFX;
    [SerializeField] Slider BowPowerSlider;
    [SerializeField] Transform Bow;

    [Range(0,10)]
    [SerializeField] float BowPower;

    [Range(0, 3)]
    [SerializeField] float MaxBowCharge;

    float BowCharge;
    bool CanFire = true;


    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private void Start()
    {
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = MaxBowCharge;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackSword();
        }
        else
        {
            if (Input.GetMouseButton(0) && CanFire)
            {
                ChargeBow();
            }
            else if (Input.GetMouseButtonUp(0) && CanFire)
            {
                AttackBow();
            }
            else
            {
                if (BowCharge > 0f)
                {
                    BowCharge -= 1f * Time.deltaTime;
                }
                else
                {
                    BowCharge = 0f;
                    CanFire = true;
                }

                BowPowerSlider.value = BowCharge;
            }
        }

    }

    void AttackSword()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<EnemyStats>().TakeDamage(50.0f);
            }

        }
    }

    void ChargeBow()
    {
        ArrowGFX.enabled = true;

        BowCharge += Time.deltaTime;

        BowPowerSlider.value = BowCharge;

        if (BowCharge > MaxBowCharge)
        {
            BowPowerSlider.value = MaxBowCharge;
        }
    }


    void AttackBow()
    {
        if (BowCharge > MaxBowCharge)
            BowCharge = MaxBowCharge;

        float ArrowSpeed = BowCharge + BowPower;
        float ArrowDamage = BowCharge * BowPower;

        float angle = Utility.AngleTowardsMouse(Bow.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        Arrow Arrow = Instantiate(ArrowPrefab, Bow.position, rot).GetComponent<Arrow>();
        Arrow.ArrowVelocity = ArrowSpeed;
        Arrow.ArrowDamage = ArrowDamage;

        CanFire = false;
        ArrowGFX.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
