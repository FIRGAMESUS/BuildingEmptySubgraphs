using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGraphVertex : MonoBehaviour
{
    public GameObject Panel, GraphVertex, GraphLine, Lines, SpawnGraphButton;
    Text GraphVertexNumber;
    int limit, indexGraph, indexLine;
    float x, y;
    float[,] GraphsPos;
    bool CheckPlace, isSpawnGraphButtonActive;
    GameObject[] graphs;

    private Vector3 MousePos;


    void Start()
    {
        isSpawnGraphButtonActive = false;
        limit = 25;
        graphs = new GameObject[limit];
        indexGraph = 0;
        GraphVertexNumber = GraphVertex.gameObject.GetComponentInChildren<Text>();
        GraphsPos = new float[limit, 2];
    }

    public void PressSpawnGraphButton()
    {
        isSpawnGraphButtonActive = !isSpawnGraphButtonActive;
        Debug.Log(isSpawnGraphButtonActive);
        if (isSpawnGraphButtonActive == true) SpawnGraphButton.GetComponent<Image>().color = SpawnGraphButton.GetComponent<Button>().colors.pressedColor;
        else SpawnGraphButton.GetComponent<Image>().color = SpawnGraphButton.GetComponent<Button>().colors.normalColor;
    }
    public void SpawnGraph()
    {
        if (isSpawnGraphButtonActive == true)
        {
            if (indexGraph > limit - 1) { return; }
            do
            {
                MousePos = Input.mousePosition;
                //x = Random.Range(3*Screen.width/10, Screen.width-100);
                //y = Random.Range(100, Screen.height - 100);
                x = MousePos.x;
                y = MousePos.y;
                GraphsPos[indexGraph, 0] = x;
                GraphsPos[indexGraph, 1] = y;
                for (int i = 0; i < indexGraph; i++)
                {
                    if ((GraphsPos[i, 0] == x) && (GraphsPos[i, 1] == x))
                    {
                        CheckPlace = false;
                        Debug.Log(CheckPlace);
                        if (CheckPlace == false) continue;
                    }
                }
            } while (CheckPlace == true);
            indexGraph++;
            Vector3 pos = new Vector3(x, y, 0);
            GraphVertexNumber.text = indexGraph.ToString();
            GameObject ver = Instantiate(GraphVertex, pos, transform.rotation, Panel.transform.Find("Graphs"));
            graphs[indexGraph - 1] = ver;
    }
        
        
    }

    public void SpawnLine()
    {
        for (int i = 1; i < indexGraph; i++)
        {
            float deltaX = graphs[i - 1].transform.localPosition.x - graphs[i].transform.localPosition.x;
            float deltaY = graphs[i - 1].transform.localPosition.y - graphs[i].transform.localPosition.y;
            float dist = (float)System.Math.Sqrt(System.Math.Pow(deltaX, 2) + System.Math.Pow(deltaY, 2));

            GameObject lin = Instantiate(GraphLine, graphs[i - 1].transform.position, transform.rotation, Lines.transform);
            lin.transform.GetChild(0).localScale = new Vector3(1, dist / 100, 1);
            lin.transform.LookAt(graphs[i].transform);
            lin.transform.rotation *= Quaternion.AngleAxis(-90, Vector3.forward);
        }
    }

    public void test(int ind)
    {
        Debug.Log(ind);
    }

}
