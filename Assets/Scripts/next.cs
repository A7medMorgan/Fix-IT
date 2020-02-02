using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class next : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nextt());
    }

    IEnumerator nextt()
    {
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
