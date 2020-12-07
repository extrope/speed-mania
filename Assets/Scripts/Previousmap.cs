using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Previousmap : MonoBehaviour
{
    
        public void Prev()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Map1");
        }
    
}
