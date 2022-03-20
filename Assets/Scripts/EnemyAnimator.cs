using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator anim;
    public EnemyAi enemyAi;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        enemyAi = gameObject.GetComponentInParent<EnemyAi>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void fire()
    {
        enemyAi.attackBool = true;
    }
    void hit() 
    {
        anim.SetBool("Hit",false);
    }
        
    
}
