using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(Done),3);
    }

    void Done()
    {
        ManagerMain.Instance.LoadScene(1);
    }
}
