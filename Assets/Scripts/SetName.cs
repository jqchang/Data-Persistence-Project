using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour
{
    private DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = DataManager.Instance;
    }

    // Make sure to set Dynamic string in Unity editor
    public void SetPlayerName(string arg0)
    {
        Debug.Log(arg0);
        dataManager.playerName = arg0;
    }
}
