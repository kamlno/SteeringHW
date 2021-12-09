using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pratice2 : MonoBehaviour
{
    public GameObject m_Goal;
    public AIData m_data;
    private void Start()
    {

    }
    void Update()
    {
        m_data.mTarget = m_Goal.transform.position;
        SteeringBehavior.Seek(m_data);
        SteeringBehavior.Move(m_data);
    }
    private void OnDrawGizmos()
    {
        if(m_data != null)
        {
            if(m_data.m_fMoveForce > 0.0f)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * m_data.m_fMoveForce * 4.0f);

            Gizmos.color = Color.green;

            Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 2.0f);

            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(this.transform.position, m_data.mTarget);
        }
    }
}
