using UnityEngine;
using System.Collections;

public class STGLoadScene : MonoBehaviour
{
    public string sceneName;

    public void loadScene()
    {
        //Debug.Log("load");
        Application.LoadLevel(sceneName);
    }

}
