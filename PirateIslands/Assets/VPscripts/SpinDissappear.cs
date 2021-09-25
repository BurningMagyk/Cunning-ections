using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinDissappear : MonoBehaviour
{
    public Text myText;
    public float startRotation;
    private float startAlpha = 1f;
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, startRotation));
        rotSpeed = Random.Range(-45f, 45f);
    }

    // Update is called once per frame
    void Update()
    {
        startRotation += Time.deltaTime * rotSpeed;
        startAlpha -= Time.deltaTime * 1.5f;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, startRotation));
        Color c = myText.color;
        c.a = startAlpha;
        myText.color = c;
        if(startAlpha <= -0.2f)
        {
            Destroy(gameObject);
        }
    }
}
