using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public Animator animator;

    private bool facingRight = true;
    private bool isPunching = false;

    private float punchCount = 0f;
    private float timeSinceFirstPunch = 0f;
    private float timeSinceLastPunch = 0f;
    private float punchWindow = 0.3f;
    private bool firstHit = true;

    public AudioSource source;
    public AudioClip comboClap;
    public AudioClip comboHit;

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        controller.Move(Vector3.forward * hInput * speed * Time.deltaTime);
        animator.SetFloat("WalkSpeed", Mathf.Abs(hInput));

        if (hInput < 0f && facingRight)
            FlipCharacter(false);
        else if(hInput > 0f && !facingRight)
            FlipCharacter(true);

        if (Input.GetKeyDown(KeyCode.Space))
            TriggerPunchSystem();

        
        if (Time.time - timeSinceFirstPunch > punchWindow && firstHit == false && punchCount < 3 && punchCount > 1)
        {
            TriggerDouble();
        }
        else if (Time.time - timeSinceFirstPunch > punchWindow && firstHit == false && punchCount < 3)
        {
            TriggerPunch();
        }
    }

    private void FlipCharacter(bool right)
    {
        facingRight = right;
        if (right)
            animator.transform.rotation = Quaternion.identity;
        else
            animator.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    private void TriggerPunchSystem()
    {

        if(Time.time - timeSinceLastPunch <= punchWindow)
        {
            punchCount++;
            source.PlayOneShot(comboClap);
            firstHit = false;
        }
        else if(firstHit == true)
        {
            punchCount++;
            source.PlayOneShot(comboClap);
            firstHit = false;
            timeSinceFirstPunch = Time.time;
        }
        else
        {
            TriggerPunch();
        }

        timeSinceLastPunch = Time.time;

        if (punchCount == 3)
        {
            punchCount = 0;
            TriggerCombo();
        }

    }

    private void TriggerCombo()
    {
        isPunching = true;
        source.PlayOneShot(comboHit);
        animator.SetTrigger("Combo");
        firstHit = true;
        StartCoroutine(ResetPunch());
    }

    private void TriggerDouble()
    {
        isPunching = true;
        animator.SetTrigger("DoublePunch");
        firstHit = true;
        punchCount = 0;
        StartCoroutine(ResetPunch());
    }

    private void TriggerPunch()
    {
        isPunching = true;
        animator.SetTrigger("Punch");
        firstHit = true;
        punchCount = 0;
        StartCoroutine(ResetPunch());
    }

    private System.Collections.IEnumerator ResetPunch()
    {
        float punchDuration = animator.GetCurrentAnimatorStateInfo(0).length * 2;
        yield return new WaitForSeconds(punchDuration);

        isPunching = false;
    }
}

