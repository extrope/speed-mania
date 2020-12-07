using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{
    
        public void Next()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Map2");
        }
    
}
