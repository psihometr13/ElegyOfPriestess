using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontifCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1.5f);
        _spriteRenderer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {   
                _anim.SetBool("Dead", true);
                StartCoroutine("Dead");
            }
       
    }
}
