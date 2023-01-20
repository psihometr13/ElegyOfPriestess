using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SisterCheck : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    public GameObject _gameObject;
    public bool check = false;
    [SerializeField] GameObject wall;
    [SerializeField] public GameObject spike;
    [SerializeField] public GameObject elevator;

    
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //wall = GameObject.Find("NOEnterBasement");
        //spike = GameObject.Find("Spike");
        //elevator = GameObject.Find("point1_10");
     

    }
    IEnumerator Diss()
    {
        yield return new WaitForSeconds(1f);
      
        _spriteRenderer.enabled = false;
        transform.gameObject.tag = "NoOne";
      
    }
    IEnumerator Vid()
    {
        yield return new WaitForSeconds(3f);
        SaveSystem.SetBool("issister", true);
        SaveSystem.SetBool("doorOpened", true);
        SoundManager.Instance.Stop();
        Upd_PlayerControl.Instance.SaveGame();
        SceneManager.LoadScene(4);
    }
    public void dias()
    {
        check = true;

    }
    public void diasNo(GameObject _gameObject)
    {
        _gameObject.transform.gameObject.tag = "NoOne";
        SaveSystem.SetBool("doorOpened", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (check == true)
        {
            if (collision.CompareTag("Player"))
            {
                Upd_PlayerControl.Instance.countOfKilledBosses += 1;
                Destroy(wall);
                Destroy(spike);
                elevator.transform.position = new Vector3(-94, -127, 0);
                _anim.SetBool("Diss", true);
                StartCoroutine("Diss");
               
                StartCoroutine("Vid");
            }
        }
    }
}
