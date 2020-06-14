using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private IMoveVelocity moveVelocity;
    private IMovePosition movePosition;
    private ICharacterAim aim;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    // hit animation
    SpriteRenderer[] spriteRenderers;
    Material matDefault;
    Material matWhite;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        moveVelocity = GetComponent<IMoveVelocity>();
        movePosition = GetComponent<IMovePosition>();
        aim = GetComponent<ICharacterAim>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(); // grab all sprite renderers
        matDefault = spriteRenderer.material;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
    }

    protected virtual void Update()
    {
        if (moveVelocity.GetVelocity() == Vector3.zero)
            PlayIdleAnimation();
        else
            PlayWalkAnimation();
    }

    public Vector3 GetVelocity()
    {
        return moveVelocity.GetVelocity();
    }

    public void SetVelocity(Vector3 velocity)
    {
        moveVelocity.SetVelocity(velocity);
    }

    public void MoveToPosition(Vector3 position)
    {
        movePosition.SetMovePosition(position);
    }

    public void Stop()
    {
        moveVelocity.SetVelocity(Vector3.zero);
    }

    public Vector3 GetAimDir()
    {
        return aim.GetAimDir();
    }

    protected float GetAimAngle()
    {
        return aim.GetAimAngle();
    }

    public void AimAt(Vector3 position)
    {
        aim.SetTarget(position);
    }

    // animation

    public void PlayIdleAnimation()
    {
        animator.SetBool("Walking", false);
    }

    public void PlayWalkAnimation()
    {
        animator.SetBool("Walking", true);
    }


    public void Flash()
    {
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.material = matWhite;
        }
        Invoke("ResetMaterial", .1f);
    }

    private void ResetMaterial()
    {
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.material = matDefault;
        }
    }
}
