using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingle<T> : MonoBehaviour where T: GenericSingle<T> ,new ()//where后面是对T的约束所以两个继承不会冲突
{
    private static T instance;

    public static T Instance {
        get
        {

            if(instance == null)
            {
                //查询有没有这个组件
               
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    //‘"Single of"+typeof(T).ToString()’是给这个命名
                    GameObject gameObject = new GameObject("Single of " + typeof(T).ToString(),typeof(T));
                    instance = gameObject.GetComponent<T>();

                }
                else
                {
                    instance.gameObject.name = "Single of " + typeof(T).ToString();
                }
                instance.Init();
                //保证场景切换，这个组件不会被销毁
                GameObject.DontDestroyOnLoad(instance.gameObject);
            }
           
            return instance;
        }
        set => instance = value;

    }
    //初始化场景
    public virtual void Init()
    {

    }
    //初始化查询物体--防止一开始就挂上脚本
    private void Awake()
    {

        if (instance = null)
        {
            instance = this as T;
            instance.Init();
            GameObject.DontDestroyOnLoad(instance.gameObject);
        }
    }

    //场景退出，销毁实例
    private void OnApplicationQuit()
    {
        instance = null;
    }
}
