using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
   public static void ttt(float t)
    {
        t = 1.0f;
    }
    public static void Move(AIData data)
    {
        if(data.m_bMove == false)
        {
            return;
        }
        Transform myobj = data.mGoGo.transform;
        Vector3 myPos = data.mGoGo.transform.position;
        Vector3 vr = myobj.right;
        Vector3 vec = myobj.forward;
        Vector3 vf = data.mCurrentVec;

        if(data.m_fTempTurnForce > data.m_fMaxRotate)
        {
            data.m_fTempTurnForce = data.m_fMaxRotate;
        }
        else
        {
            data.m_fTempTurnForce = -data.m_fMaxRotate;
        }

        vf = vf + vr * data.m_fTempTurnForce;
        vf.Normalize();
        myobj.forward = vf;

        data.m_Speed = data.m_Speed + data.m_fMoveForce * Time.deltaTime;
        if(data.m_Speed < 0.01f)
        {
            data.m_Speed = 0.01f;
        }
        else if(data.m_Speed > data.m_fMaxSpeed)
        {
            data.m_Speed = data.m_fMaxSpeed;
        }

        myobj.forward = vec;

        //if (data.m_Speed < 0.02f)
        //{
        //    if (data.m_fTempTurnForce > 0)
        //    {
        //        myobj.forward = vr;
        //    }
        //    else
        //    {
        //        myobj.forward = -vr;
        //    }
        //}

        myPos = myPos + myobj.forward * data.m_Speed;
        myobj.position = myPos;
    }
    public static bool Seek(AIData data)
    {
        Vector3 myPos = data.mGoGo.transform.position; //�_�l&�Y�ɪ����m
        Vector3 vec = data.mTarget - myPos; //�_�l&�Y�ɦV�q
        vec.y = 0.0f;  

        float fDist = vec.magnitude;  //fDist��vec��length
        if (fDist < data.m_Speed + 0.001f) //DeadZone
        {
            Vector3 vGoal = data.mTarget;  //��ؼЮy�ХᵹGoal
            vGoal.y = myPos.y;  //�ư���y
            data.mGoGo.transform.position = vGoal;  //����I�P����y�й��
            data.m_fMoveForce = 0.0f; //�X�O�k�s
            data.m_fTempTurnForce = 0.0f;//�����k�s
            data.m_Speed = 0.0f;
            data.m_bMove = false;  //bool move = false
            return false;
            
        }

        Vector3 vf = data.mGoGo.transform.forward; //�⪫�󪺥[�t�V�q�ᵹvf
        Vector3 vr = data.mGoGo.transform.right;  //�⪫�󪺱���V�q�ᵹvr
        data.mCurrentVec = vf; //�⥿���]���Y�ɤ�V
        vec.Normalize(); //vec���� length = 1���V�q
        float fDotF = Vector3.Dot(vf, vec); //vf�bvec�W��v������ length = 1
        if(fDotF > 0.96f) //���b //vf & vec length = 1 ����u��cos��  //�Ycos���X�G��0
        {
            fDotF = 1.0f;  
            data.mCurrentVec = vec;  //��Y�ɤ�V�令�ؼФ�V
            data.m_fTempTurnForce = 0.0f; //�����V�q�k�s
            data.m_Rotate = 0.0f; //�L����
        }
        else
        {
            if (fDotF < -1.0f) //���b
            {
                fDotF = -1.0f; 
            }

            float fDotR = Vector3.Dot(vr, vec); //vr�bvec�W��v������

            if (fDotF < 0.0f)  //�Y���󥿻P�ؼФϦV
            {

                if (fDotR > 0.0f)  //�u�nvf != vec �N�������ΰf��1����V�q
                {
                    fDotR = 1.0f;
                }
                else
                {
                    fDotR = -1.0f;
                }
            }
            if(fDist < 3.0f)  //�Y�Z���b3�H��
            {
                fDotR *= (fDist / 3.0f + 1.0f);  //fDotR�h���Z���Ϥ�
            }
            data.m_fTempTurnForce = fDotR;
        }

        if(fDist < 3.0f)
        {
            Debug.Log(data.m_Speed);
            if(data.m_Speed > 0.1f)
            {
                data.m_fMoveForce = -(1.0f - fDist / 3.0f) * 5.0f;
            }
            else
            {
                data.m_fMoveForce = fDotF * 100.0f;
            }
        }
        else
        {
            data.m_fMoveForce = 100.0f;
        }
        data.m_bMove = true;
        return true;
    }
}
