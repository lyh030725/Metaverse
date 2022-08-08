using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers s_instance;
    public static Managers Instance {get { init(); return s_instance;}}

    InputManager _input = new InputManager();
    SceneManagerEx _scene = new SceneManagerEx();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    PoolManager _pool = new PoolManager();
    DataManager _data = new DataManager();



    public static InputManager Input { get { return Instance._input; } }
    public static SceneManagerEx Scene { get { return Instance._scene;  } }
    public static ResourceManager Resource {  get { return Instance._resource; } }
    public static SoundManager Sound {  get { return Instance._sound;  } }
    public static UIManager UI { get { return Instance._ui; } }
    public static PoolManager Pool {  get { return Instance._pool; } }
    public static DataManager Data {  get { return Instance._data;  } }
     
    void Start()
    {
        init();
        
    }

    void Update()
    {
        Managers.Input.OnUpdate();
    }

    static void init(){

        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {  
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
            s_instance._pool.Init();
        }
    }

    public static void Clear()
    {
        Scene.Clear();
        UI.Clear();
        Sound.Clear();
        Input.Clear();
        Pool.Clear();
    }
}
