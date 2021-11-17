using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    // Χ����ת��Ŀ������

    public Transform target;

    // ������ת�Ƕ�

    public float x = 0f, y = 0f, z = 0f;

    public bool xFlag = false, yFlag = false;

    // ��ת�ٶ�ֵ

    public float xSpeed = 10f, ySpeed = 10f, mSpeed = 5f;

    // y��Ƕ����ƣ����ó�һ������᲻��ת

    public float yMinLimit = -50, yMaxLimit = 80;

    // x��Ƕ����ƣ�ͬ�� 

    public float leftMax = -365, rightMax = 365;

    // �������ƣ�ͬ��

    public float distance = 3f, minDistance = 0.5f, maxDistance = 6f;

    // ��������

    public bool needDamping = true;

    public float damping = 3f;

    public float initX;

    public float initY;

    // �ı�����Ŀ������

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
                // �ж��Ƿ���Ҫ������ת

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



            // �����

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

    // ����ֵ�������ƣ�

    static float ClampAngle(float angle, float min, float max)

    {
        if (angle < -360)

            angle += 360;

        if (angle > 360)

            angle -= 360;

        return Mathf.Clamp(angle, min, max);

    }

    // ��ʼ

    public void pers()

    {
        this.x = initX;

        this.y = initY;

    }

    // ����ͼ

    public void front()

    {
        this.x = 0f;

        this.y = 0f;

    }

    // ����ͼ

    public void back()

    {
        this.x = 180f;

        this.y = 0f;

    }

    // ����ͼ

    public void left()

    {
        this.x = 90f;

        this.y = 0f;

    }

    // ����ͼ

    public void right()

    {
        this.x = 270f;

        this.y = 0f;

    }

    // ����ͼ

    public void top()

    {
        this.x = 0f;

        this.y = 90f;

    }

    // ����ͼ

    public void bottom()

    {
        this.x = 0f;

        this.y = -90f;

    }

}

