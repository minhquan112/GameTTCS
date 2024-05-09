using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Action_IDLE : MonoBehaviour
{
   
    // Start is called before the first frame update
   public float moveSpeed ;
   private Rigidbody2D  rb;
    public Animator animator;
   public Vector3 moveInput;
    private Vector2 targetPos;
    
   private void Start() {
        moveSpeed=0.1f;
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
    }

   public void Move(Vector3 position){
    animator.SetFloat("Speed", 1);
    this.transform.localScale = new Vector3(this.transform.position.x > position.x ? -1:1, 1, 1);
    this.transform.DOMove(position,1).OnComplete(()=>{animator.SetFloat("Speed", 0);});
   }

   

}
