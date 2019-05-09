using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyService;
public class ShowDescribe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        MyService.MyEventSystem.Instance.Invoke(DescribeObjectArgs.Id, this, new DescribeObjectArgs() { GameObject = this.gameObject });
    }

}
