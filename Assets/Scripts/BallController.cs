using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float thrust = 150;
    [SerializeField] float wallDistance = 5;
    Vector2 lastMousePos = Vector2.zero;
    public float minCamDist = 3f;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isWin)
        {
            Vector2 deltaPos = Vector2.zero;
            if (Input.GetMouseButton(0))
            {
                Vector2 currentMousePos = Input.mousePosition;
                if (lastMousePos == Vector2.zero)
                    lastMousePos = currentMousePos;
                deltaPos = currentMousePos - lastMousePos;
                lastMousePos = currentMousePos;
                Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;
                rb.AddForce(force);
            }
            else
            {
                lastMousePos = Vector2.zero;
            }
            AfterUpdate();
        }
    }
    private void AfterUpdate()
    {
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Clamp(pos.x, -wallDistance, wallDistance);
        float dist = Camera.main.transform.position.z + minCamDist;
        if (transform.position.z < dist)
        {
            pos.z = dist;
        }
        //if (transform.position.x > wallDistance)
        //{
        //    pos.x = wallDistance;
        //}
        //else if (transform.position.x < -wallDistance)
        //{
        //    pos.x = -wallDistance;
        //}
        transform.position = pos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Enemy")
        {
            //Debug.LogError("GameOver");
            //Time.timeScale = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag=="Color Changer")
        //{
        //    if (this.tag == "Player")
        //    {
        //        this.tag = "Enemy";
        //        this.GetComponent<MeshRenderer>().sharedMaterial = gm.enemyMat;
        //    }
        //    else if (this.tag == "Enemy")
        //    {
        //        this.tag = "Player";
        //        this.GetComponent<MeshRenderer>().sharedMaterial = gm.playerMat;
        //    }
        //}//else
        if (other.tag == "Win")
        {
            //Debug.LogError("Go To Next Level");
            gm.Win();
        }
    }
}
