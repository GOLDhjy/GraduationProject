using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyService;
public class PlayerToe : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            AudioSource AudioSource = transform.GetComponentInParent<AudioSource>();
            //AudioSource = gameObject.GetComponentInParent<AudioSource>();
            AudioService.Instance.PlayWithAS(AudioSource, AudioEnum.FootStep, "FootstepTile2");
            if(this.gameObject.tag == "lefttoe")
            {
                Debug.Log("lefttoe");
            }
            else
                Debug.Log("not lefttoe");
        }
    }
}
