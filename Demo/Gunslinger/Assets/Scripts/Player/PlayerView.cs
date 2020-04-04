using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Animator anim;
    public List<Sprite> sprites;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayIdleAnim()
    {
        anim.SetBool("Walking", false);
    }

    public void PlayMoveAnim(Vector3 moveDir)
    {
        anim.SetBool("Walking", true);
        anim.SetFloat("MoveX", moveDir.x);
        anim.SetFloat("MoveY", moveDir.y);
    }

    public void TurnBackward()
    {
        spriteRenderer.sprite = sprites[3];
        spriteRenderer.sortingOrder = 3;
    }

    public void TurnForward()
    {
        spriteRenderer.sprite = sprites[0];
        spriteRenderer.sortingOrder = 0;

    }

    public void TurnLeft()
    {
        spriteRenderer.sprite = sprites[1];
        spriteRenderer.sortingOrder = 3;
    }

    public void TurnRight()
    {
        spriteRenderer.sprite = sprites[2];
        spriteRenderer.sortingOrder = 0;

    }
}
