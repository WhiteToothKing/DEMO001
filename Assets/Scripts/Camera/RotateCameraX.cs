using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    // 围绕旋转的目标物体

    public Transform target;

    // 设置旋转角度

    public float x = 0f, y = 0f, z = 0f;

    public bool xFlag = false, yFlag = false;

    // 旋转速度值

    public float xSpeed = 10f, ySpeed = 10f, mSpeed = 5f;

    // y轴角度限制，设置成一样则该轴不旋转

    public float yMinLimit = -50, yMaxLimit = 80;

    // x轴角度限制，同上 

    public float leftMax = -365, rightMax = 365;

    // 距离限制，同上

    public float distance = 3f, minDistance = 0.5f, maxDistance = 6f;

    // 阻尼设置

    public bool needDamping = true;

    public float damping = 3f;

    public float initX;

    public float initY;

    // 改变中心目标物体

    public void SetTarget(GameObject go)

    {
        target = go.transform;

    }

    void Start()

    {
        Vector3 angles = transform.eulerAngles;

        x = angles.y;

        y = angles.x;



        //pers();

    }

    void LateUpdate()

    {
        if (target)

        {
            if (Input.GetMouseButton(2))

            {
                // 判断是否需要反向旋转

                if ((y > 90f && y < 270f) || (y < -90 && y > -270f))

                {
                    x -= Input.GetAxis("Mouse X") * xSpeed * 0.02f;

                }

                else

                {
                    x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;

                }

                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;



                x = ClampAngle(x, leftMax, rightMax);

                y = ClampAngle(y, yMinLimit, yMaxLimit);

            }



            distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;

            distance = Mathf.Clamp(distance, minDistance, maxDistance);



            Quaternion rotation = Quaternion.Euler(y, x, z);

            Vector3 disVector = new Vector3(0.0f, 0.0f, -distance);

            Vector3 position = rotation * disVector + target.position;



            // 阻尼感

            if (needDamping)

            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);

                transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);

            }

            else

            {
                transform.rotation = rotation;

                transform.position = position;

            }



        }

    }

    // 对数值进行限制；

    static float ClampAngle(float angle, float min, float max)

    {
        if (angle < -360)

            angle += 360;

        if (angle > 360)

            angle -= 360;

        return Mathf.Clamp(angle, min, max);

    }

    // 初始

    public void pers()

    {
        this.x = initX;

        this.y = initY;

    }

    // 正视图

    public void front()

    {
        this.x = 0f;

        this.y = 0f;

    }

    // 后视图

    public void back()

    {
        this.x = 180f;

        this.y = 0f;

    }

    // 左视图

    public void left()

    {
        this.x = 90f;

        this.y = 0f;

    }

    // 右视图

    public void right()

    {
        this.x = 270f;

        this.y = 0f;

    }

    // 俯视图

    public void top()

    {
        this.x = 0f;

        this.y = 90f;

    }

    // 仰视图

    public void bottom()

    {
        this.x = 0f;

        this.y = -90f;

    }

}

