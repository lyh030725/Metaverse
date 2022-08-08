using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Scene : Base_Scene
{

    public override void Clear()
    {


    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();
        gameObject.GetOrAddComponent<CursorController>();

    }




}
