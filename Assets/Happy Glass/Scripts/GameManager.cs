using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// Happy Glass. This Project was made in August 2018. If you like the project add me on Facebook and follow me on Instagram to never miss and Update. 
/// Credit: Satyam Parkhi
/// Email: satyamparkhi@gmail.com
/// Facebook : https://www.facebook.com/satyamparkhi
/// Instagram : https://www.instagram.com/satyamparkhi/
/// Whatsapp : +91 7050225661
/// </summary>

public class GameManager : MonoBehaviour
{

    [Tooltip("The color of the drawn lines")]
    public Color lineColor;
    public Material lineMaterial;
    public Transform Pencil;
    public Sprite SurpriseGlass;
    public Sprite HappyGlass;
    public Sprite SadGlass;
    public Slider PenCapacity;
    public Text PenPercent;
    [HideInInspector]
    public GameObject[] Hint;

    public Image Star1;
    public Image Star2;
    public Image Star3;
    public GameObject LevComp;
    private GameObject[] Obs;
    private List<GameObject> listLine = new List<GameObject>();
    public List<Vector2> listPoint = new List<Vector2>();
    private GameObject currentLine;
    public GameObject currentColliderObject;
    private GameObject hintTemp;
    private GameObject[] waterTap;
    private GameObject Glass;
    private Vector3 LastMosPos;
    private BoxCollider2D currentBoxCollider2D;
    private LineRenderer lines;
    private LineRenderer currentLineRenderer;
    private bool stopHolding;
    private bool allowDrawing = true;
    [HideInInspector]
    public bool completed;
    int clickCont;
    private List<Rigidbody2D> listObstacleNonKinematic = new List<Rigidbody2D>();
    private GameObject[] obstacles;
    float mosDis;
    bool canCreate;
    RaycastHit2D hit_1;
    RaycastHit2D hit_2;
    RaycastHit2D hit_3;
    GameObject TemLine;
    void Start()
    {
        Pencil.gameObject.SetActive(false);
        waterTap = GameObject.FindGameObjectsWithTag("Interactive");
        Glass = GameObject.FindGameObjectWithTag("GlassParent");
        Glass.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Hint = GameObject.FindGameObjectsWithTag("Hint");
        for (int i = 0; i < Hint.Length; i++)
        {
            Hint[i].SetActive(false);
        }
        lineMaterial.SetColor("_Color", lineColor);
        Obs = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < Obs.Length; i++)
        {
            Obs[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    void Update()
    {
        if (PenCapacity.value <= 0.01f || !Input.GetMouseButton(0))
        {
            Pencil.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;     //Get the button on click
            if (thisButton != null)                                                                             //Is click on button
            {
                allowDrawing = false;
                print("cant drwa");
            }
            else                                                                                                //Not click on button
            {
                allowDrawing = true;
                stopHolding = false;
                listPoint.Clear();
                CreateLine(Input.mousePosition);
                print("draw");
            }
        }
        else if (Input.GetMouseButton(0) && !stopHolding && allowDrawing && PenCapacity.value > 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (LastMosPos != Camera.main.ScreenToWorldPoint(Input.mousePosition))
            {
                if (rayHit.collider == null)
                {

                    Pencil.gameObject.SetActive(true);
                    Pencil.position = new Vector3(LastMosPos.x, LastMosPos.y, 0);
                    if (canCreate == false)
                    {
                        float dist = Vector3.Distance(LastMosPos, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                        Pencil.GetComponent<TrignometricRotation>().enabled = true;
                        PenCapacity.value = PenCapacity.value - dist / 25;
                        PenPercent.text = Mathf.FloorToInt(PenCapacity.value * 100).ToString() + " %";
                        if (Mathf.FloorToInt(PenCapacity.value * 100) < 75)
                        {
                            Star3.gameObject.SetActive(false);
                        }
                        if (Mathf.FloorToInt(PenCapacity.value * 100) < 50)
                        {
                            Star2.gameObject.SetActive(false);
                        }
                        if (Mathf.FloorToInt(PenCapacity.value * 100) < 25)
                        {
                            Star1.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                Pencil.GetComponent<TrignometricRotation>().enabled = false;
            }

                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float ab = Vector2.Distance(LastMosPos, mousePos);
                mosDis = mosDis + ab;

            if (!listPoint.Contains(mousePos) && mosDis > .02f)
            {
                mosDis = 0;
                //Add mouse pos, set vertex and position for line renderer
                if (canCreate == false)
                {
                    if (rayHit.collider == null)
                    {
                        listPoint.Add(mousePos);                               //UnComment Below Lines If you want the line render to be in parent 
                                                                               //currentLineRenderer.positionCount = listPoint.Count;
                                                                               //currentLineRenderer.SetPosition(listPoint.Count - 1, listPoint[listPoint.Count - 1]);
                    }
                }

                //Create collider
                if (listPoint.Count >= 2)
                {
                    if (canCreate == false)
                    {
                        if (rayHit.collider == null)
                        {
                            Vector2 point_1 = listPoint[listPoint.Count - 2];
                            Vector2 point_2 = listPoint[listPoint.Count - 1];

                            currentColliderObject = new GameObject("Collider");
                            currentColliderObject.transform.position = (point_1 + point_2) / 2;
                            currentColliderObject.transform.right = (point_2 - point_1).normalized;
                            currentColliderObject.transform.SetParent(currentLine.transform);

                            currentBoxCollider2D = currentColliderObject.AddComponent<BoxCollider2D>();
                            currentBoxCollider2D.size = new Vector2((point_2 - point_1).magnitude, 0.05f);
                            currentBoxCollider2D.enabled = false;
                            if (currentLine.transform.childCount > 1)
                            {
                                lines = currentColliderObject.AddComponent<LineRenderer>(); //Comment Below Lines If you want the line render to be in parent 
                                lines.sharedMaterial = lineMaterial;
                                lines.positionCount = 2;
                                lines.startWidth = 0.05f;
                                lines.endWidth = 0.05f;
                                lines.startColor = lineColor;
                                lines.endColor = lineColor;
                                lines.useWorldSpace = true;
                                lines.numCapVertices = 90;
                                lines.sortingOrder = -51;
                                currentColliderObject.AddComponent<LineRendParent>();       //Till here
                            }
                        }
                    }
                    Vector2 rayDirection;
                    if (canCreate == false)
                    {
                        rayDirection = currentColliderObject.transform.TransformDirection(Vector2.right);
                    }
                    else
                    {
                        rayDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - currentColliderObject.transform.position;
                    }

                    Vector2 pointDir = currentColliderObject.transform.TransformDirection(Vector2.up);
                    Vector2 rayPoint_1 = ((Vector2)currentColliderObject.transform.position);
                    Vector2 rayPoint_2 = ((Vector2)currentColliderObject.transform.position + pointDir * (currentBoxCollider2D.size.y / 2f));
                    Vector2 rayPoint_3 = ((Vector2)currentColliderObject.transform.position + (-pointDir) * (currentBoxCollider2D.size.y / 2f));

                    float rayLength = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rayPoint_1).magnitude;

                    hit_1 = Physics2D.Raycast(rayPoint_1, rayDirection, rayLength);
                    hit_2 = Physics2D.Raycast(rayPoint_2, rayDirection, rayLength);
                    hit_3 = Physics2D.Raycast(rayPoint_3, rayDirection, rayLength);

                    Debug.DrawRay(rayPoint_1, rayDirection, Color.red, rayLength);
                    Debug.DrawRay(rayPoint_2, rayDirection, Color.green, rayLength);
                    Debug.DrawRay(rayPoint_3, rayDirection, Color.green, rayLength);

                    if (hit_1.collider != null || hit_2.collider != null || hit_3.collider != null)
                    {
                        GameObject hit = (hit_1.collider != null) ? (hit_1.collider.gameObject) : ((hit_2.collider != null) ? (hit_2.collider.gameObject) : (hit_3.collider.gameObject));
                        if (currentColliderObject.transform.parent != hit.transform.parent)
                        {
                            if (canCreate == false)
                            {
                                canCreate = true;

                                GameObject tempOBJ = currentColliderObject.transform.parent.GetChild(currentColliderObject.transform.GetSiblingIndex() - 1).gameObject;
                                Destroy(currentBoxCollider2D.gameObject);
                                listPoint.Remove(listPoint[listPoint.Count - 1]);
                                currentColliderObject = tempOBJ;
                                currentBoxCollider2D = currentColliderObject.GetComponent<BoxCollider2D>();
                                currentColliderObject.AddComponent<LookAtScript>();
                                TemLine = new GameObject("PreDictLine");
                                TemLine.transform.position = currentColliderObject.transform.position;
                                TemLine.transform.right = currentColliderObject.transform.right;
                                lines = TemLine.AddComponent<LineRenderer>();
                                lines.sharedMaterial = lineMaterial;
                                lines.startWidth = 0.05f;
                                lines.endWidth = 0.05f;
                                lines.startColor = new Color(lineColor.r, lineColor.g, lineColor.b, .5f);
                                lines.endColor = new Color(lineColor.r, lineColor.g, lineColor.b, .5f);
                                lines.useWorldSpace = true;
                                lines.numCapVertices = 90;
                                lines.sortingOrder = 2;
                                TemLine.AddComponent<LineRendParent>();

                            }
                        }
                    }
                    else
                    {
                        Destroy(currentColliderObject.GetComponent<LookAtScript>());
                        Destroy(TemLine);
                        canCreate = false;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && !stopHolding && allowDrawing)
        {           
            if (currentLine.transform.childCount > 0)
            {
                for (int i = 0; i < currentLine.transform.childCount; i++)
                {
                    currentLine.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                }
                listLine.Add(currentLine);
                currentLine.AddComponent<Rigidbody2D>().useAutoMass = true;
                float m = currentLine.GetComponent<Rigidbody2D>().mass * 500;
                currentLine.GetComponent<Rigidbody2D>().useAutoMass = false;
                currentLine.GetComponent<Rigidbody2D>().mass = m;
            }
            else
            {
                Destroy(currentLine);
            }
            foreach (Rigidbody2D rigid in listObstacleNonKinematic)
            {
                rigid.isKinematic = false;
            }
            if (clickCont == 0 && listLine.Count > 0)
            {
                if (TemLine != null)
                {
                    Destroy(TemLine);
                }
                Glass.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                for (int i = 0; i < Obs.Length; i++)
                {
                    Obs[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
                for (int i = 0; i < waterTap.Length; i++)
                {
                    waterTap[i].GetComponent<ParticleGenerator>().enabled = true;
                }
                clickCont++;
            }

            Pencil.gameObject.SetActive(false);
        }
        LastMosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void CreateLine(Vector2 mousePosition)
    {
        currentLine = new GameObject("Line");                                               //UnComment Below Lines If you want the line render to be in parent 
        //currentLineRenderer = currentLine.AddComponent<LineRenderer>();
        //currentLineRenderer.sharedMaterial = lineMaterial;
        //currentLineRenderer.positionCount = 0;
        //currentLineRenderer.startWidth = 0.05f;
        //currentLineRenderer.endWidth = 0.05f;
        //currentLineRenderer.startColor = lineColor;
        //currentLineRenderer.endColor = lineColor;
        //currentLineRenderer.useWorldSpace = false;
    }
    int WatrCont;
    public void WaterCheck()
    {
        WatrCont++;
        if (WatrCont == 70)
        {
            Invoke("RelScene", 2);
        }
    }
    public void StopAllPhysics()
    {
        for (int i = 0; i < listLine.Count; i++)
        {
            Rigidbody2D rigid = listLine[i].GetComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Kinematic;
            rigid.simulated = false;
        }       
    }
    void RelScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}

