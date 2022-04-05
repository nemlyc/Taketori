using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    private GameObject kaguyabamboo;
    public GameObject moon{get; private set;}
    public Vector3 moonposition = new Vector3 (0, 50, 0);

/*    void Start()
    {
        InitializeMoon();
    }
*/
    public void InitializeMoon()
    {
        kaguyabamboo = FindObjectOfType<KaguyaBamboo>().gameObject;
        this.transform.position = kaguyabamboo.transform.TransformPoint(moonposition);
    }
}
