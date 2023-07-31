using UnityEngine;

public class test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().enabled = false;
        }
    }
}
