using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login_Scene : Base_Scene
{
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }

        
    }


    public override void Clear()
    {

        Debug.Log("Game Scene Clear!");
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
        
    }
}
