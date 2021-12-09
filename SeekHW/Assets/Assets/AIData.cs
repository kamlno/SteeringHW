using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIData
{
    public GameObject mGoGo;
    public Vector3 mTarget;
    public Vector3 mCurrentVec;
    public Vector3 Goal;
    public float m_fMoveForce;
    public float m_fTempTurnForce;
    public float m_Speed;
    public bool m_bMove;
    public float m_Rotate;
    public float m_fMaxSpeed;
    public float m_fMaxRotate;
}
