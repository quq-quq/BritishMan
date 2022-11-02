using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Generator : MonoBehaviour
{
    public List<GameObject> labirints;
    public GameObject gas, player;
    public Text textTimer, endTextTimer;
    public int lifeTime;
    public float maxTime, timeStart, distance, speedGas;

    GameObject newPipe;

    float timer = 0, distance1 = 0;

    private void Start()
    {
        speedGas = Mathf.Abs(player.GetComponent<Player>().speed);
        newPipe = Instantiate(labirints[Random.Range(0, 3)]);
        newPipe.transform.position = new Vector2(distance1, 0);
        distance1 += distance;
        Destroy(newPipe, lifeTime);
        textTimer.text = timeStart.ToString("F1");
    }

    void Update()
    {
        gas.transform.position = new Vector2(gas.transform.position.x, player.transform.position.y);

        if (timer > maxTime)
        {
            if(timeStart <= 60)
                newPipe = Instantiate(labirints[Random.Range(0, labirints.Count)]);
            newPipe.transform.position = new Vector2(distance1, 0);
            Destroy(newPipe, lifeTime);
            distance1 += distance;
            timer = 0;
        }

        if(timeStart <= 60)
            gas.transform.Translate(0.3f * speedGas * Time.deltaTime, 0, 0);
        else if(timeStart <= 120)
            gas.transform.Translate(0.5f * speedGas * Time.deltaTime, 0, 0);
        else if(timeStart <= 180)
            gas.transform.Translate(0.7f * speedGas * Time.deltaTime, 0, 0);
        else
            gas.transform.Translate(speedGas * Time.deltaTime, 0, 0);

        timer += Time.deltaTime;

        timeStart += Time.deltaTime;
        textTimer.text = timeStart.ToString("F1");

        if(player.GetComponent<Player>().dead)
            endTextTimer.text = "You survived: " + (timeStart - 9f).ToString("F1");      
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
