using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Generator : MonoBehaviour
{
    public List<GameObject> labirints;
    public GameObject gas;
    public Text textTimer;


    public int distance;
    public float maxTime, timeStart;

    GameObject newPipe;

    float timer = 0;
    int distance1 = 0;

    private void Start()
    {
        for(int i = 0; i <= 1; i++)
        {
            newPipe = Instantiate(labirints[Random.Range(0, 3)]);
            newPipe.transform.position = new Vector2(distance1, 0);
            distance1 += distance;
            Destroy(newPipe, 15);
        }


        textTimer.text = timeStart.ToString("F1");
    }

    void Update()
    {
        if (timer > maxTime)
        {
            if(timeStart <= 60)
                newPipe = Instantiate(labirints[Random.Range(0, 3)]);
            newPipe.transform.position = new Vector2(distance1, 0);
            Destroy(newPipe, 15);
            distance1 += distance;
            timer = 0;
        }

        gas.transform.Translate(5 * Time.deltaTime, 0, 0);
        timer += Time.deltaTime;

        timeStart += Time.deltaTime;
        textTimer.text = timeStart.ToString("F1");
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
