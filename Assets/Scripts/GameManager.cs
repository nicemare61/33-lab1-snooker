using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField ]private int playerScore;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject[] ballPositions;

    [SerializeField] private GameObject cueBall;
    [SerializeField] private GameObject ballLine;

    [SerializeField] private float xInput;
    [SerializeField] private float force;

    [SerializeField] private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        camera = Camera.main.gameObject;
        CameraBehindBall();

        //set balls on the table
        SetBalls(BallColors.Red,0);
        SetBalls(BallColors.Yellow,1);
        SetBalls(BallColors.Green,2);
        SetBalls(BallColors.Brown,3);
        SetBalls(BallColors.Blue,4);
        SetBalls(BallColors.Pink,5);
        SetBalls(BallColors.Black,6);
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateBall();

        if (Input.GetKeyDown(KeyCode.Space)) // เอาไว้รับเวลาเรากด Space bar
        {
            ShootBall();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StopBall();
        }
    }

    void SetBalls(BallColors color, int pos)
    {
        GameObject ball = Instantiate(ballPrefab, ballPositions[pos].transform.position, Quaternion.identity);
        Ball b = ball.GetComponent<Ball>();
        b.SetColorsAndPoints(color);
    }

    void RotateBall()
    {
        xInput = Input.GetAxis("Horizontal");
        cueBall.transform.Rotate(new Vector3(0f,xInput/10,0f));
    }

    void ShootBall()
    {
        camera.transform.parent = null; //เอากล้องออกตอนยิง
        Rigidbody rd = cueBall.GetComponent<Rigidbody>();
        // ไปเอาค่า Rigidbogy ของ Cueball มา
        rd.AddRelativeForce(Vector3.forward * force , ForceMode.Impulse);
        //ForceMode.Impulse คือ พุ่งไปเลยแบบรวดเร็ว
        ballLine.SetActive(false);
        //ถ้าเป็นถูกจะแสดง Line ในการยิง
    }

    void CameraBehindBall()
    {
        camera.transform.parent = cueBall.transform; // เอา Camera Main มาเป็นลูกของ CueBall ในรูปแบบโค้ด
        camera.transform.position = cueBall.transform.position + new Vector3(0f,20f,-15f); 
        // เอาตำแหน่งของ camera มาอยู่ด้านหลังของลูกบอลให้ถอยหลังมาเพื่อให้เห็นบอล
    }

    void StopBall() // เมื่อยิงเสร็จแล้วกดปุ่มอะไรก็ตามให้เรากลับมาพร้อมยิงใหม่
    {
        Rigidbody rd = cueBall.GetComponent<Rigidbody>();
        rd.velocity = Vector3.zero; // Vector3zero ทำให้ความเร็วทุกแกนเป็น 0
        rd.angularVelocity = Vector3.zero; // angularVelocity แปลว่า ความเร็วเชิงมุม
        
        cueBall.transform.eulerAngles = Vector3.zero; //ช่วยเซ็ตบอลเป็นตำแหน้งตั้งต้น
        CameraBehindBall();
        camera.transform.eulerAngles = new Vector3(40f,0f,0f);
        ballLine.SetActive(true); // เอา Ball line กลับมา
    }
}
