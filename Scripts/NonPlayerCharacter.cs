using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    float displayTime = 4.0f;
    float timer;
    //要 public ,不然面板没有
    public GameObject dialog;

    // Start is called before the first frame update
    void Start()
    {
        timer = displayTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            dialog.SetActive(false);
        }
    }

    public void DisplayDialog()
    {
        timer = displayTime;
        dialog.SetActive(true);
    }
}
