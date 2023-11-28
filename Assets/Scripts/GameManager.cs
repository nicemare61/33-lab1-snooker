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
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
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
        Rigidbody rd = cueBall.GetComponent<Rigidbody>();
        // ไปเอาค่า Rigidbogy ของ Cueball มา
        rd.AddRelativeForce(Vector3.forward * force , ForceMode.Impulse);
        //ForceMode.Impulse คือ พุ่งไปเลยแบบรวดเร็ว
        ballLine.SetActive(false);
        //ถ้าเป็นถูกจะแสดง Line ในการยิง
    }
}
