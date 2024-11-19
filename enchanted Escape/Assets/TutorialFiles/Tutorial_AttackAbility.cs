using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial_AttackAbility : MonoBehaviour
{
    [SerializeField] CharacterController2D CC;
    [SerializeField] int DMG;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask attackLayers;
    bool attacking = false;
    [SerializeField] AudioClip AS;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attacking && CC.isGrounded)
        {
            attacking = true;
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attacking");

        Invoke(nameof(DoneAttack), 0.5f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, 1, attackLayers);

        ManagerMain.Instance.PlayAudioClip(AS);

        foreach (var hit in hits)
        {
            if (hit.gameObject.GetComponent<BossHealth>())
            {
                hit.gameObject.GetComponent<BossHealth>().TakeDamage(DMG);
            }

            if(hit.gameObject.GetComponent<Blocker>())
            {
                hit.gameObject.SetActive(false);
            }
        }

    }

    void DoneAttack()
    {
        attacking = false;
    }
}
