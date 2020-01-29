using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public string AccidentCase = "";
    public bool VHFDSC_Check = false;
    public bool badending = false;
    public int acc=0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        AccidentCase = "";
        VHFDSC_Check = false;
        badending = false;
    }
}
