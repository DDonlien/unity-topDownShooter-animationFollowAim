using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 0.5f;
    public float deceleration = 1f;
    Vector3 mouseDirectionVertical;
    Vector3 mouseDirectionHorizontal;
    Vector3 moveDirection;
    Vector3 playerDirection;
    Vector3 playerDirection90;
    float playerRotation;
    Vector3 playerFront;
    Vector3 playerSide;
    int frontIsSame;
    int sideIsSame;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //VelocityHash = Animator.StringToHash("Velocity X");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //绘制朝向矢量
        playerRotation = transform.localEulerAngles.y;
        //print (playerRotation);
        playerDirection = Quaternion.AngleAxis(playerRotation, Vector3.up) * Vector3.forward;
        playerDirection90 = Quaternion.AngleAxis(90, Vector3.up) * playerDirection;
        Debug.DrawLine(Vector3.zero, playerDirection, Color.white);
        Debug.DrawLine(Vector3.zero, playerDirection90, Color.yellow);

        //绘制移动方向矢量
        moveDirection = new Vector3(CharactorController.circle.x, 0, CharactorController.circle.y);
        moveDirection = Quaternion.AngleAxis(45, Vector3.up) * moveDirection;
        Debug.DrawLine(Vector3.zero, moveDirection, Color.red);

        //投影运动方向到面朝方向
        playerFront = Vector3.Project(moveDirection, playerDirection);
        playerSide = Vector3.Project(moveDirection, playerDirection90);
        Debug.DrawLine(Vector3.zero, playerFront, Color.blue);
        Debug.DrawLine(Vector3.zero, playerSide, Color.green);

        //判断投影与鼠标朝向是否同向
        //前向
        float angleFront = Vector3.Angle(playerFront, playerDirection);
        //print(angle);
        if (angleFront < 90)
        {
            frontIsSame = 1;
        }
        else
        {
            frontIsSame = -1;
        }
        print ("Front" + frontIsSame);
        //侧向
        float angleSide = Vector3.Angle(playerSide, playerDirection90);
        //print(angle);
        if (angleSide < 90)
        {
            sideIsSame = 1;
        }
        else
        {
            sideIsSame = -1;
        }
        print ("Side" + sideIsSame);

        //赋值投影的长度到动画机控制变量
        velocityX = 2 * (playerSide).magnitude * sideIsSame;
        velocityZ = 2 * (playerFront).magnitude * frontIsSame;

        animator.SetFloat("Velocity X", velocityX); //将velocityX赋值到动画机
        animator.SetFloat("Velocity Z", velocityZ); //将velocityX赋值到动画机
    }
}
