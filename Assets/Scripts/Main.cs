using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    GameController m_GameController;
    private void Awake()
    {
        m_GameController = new GameController(Contexts.sharedInstance);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_GameController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        m_GameController.Execute();
    }
}
