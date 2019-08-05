using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void CameraTargetting(Transform p_Target, float p_CamSpeed = 0.05f)
    {
        if(p_Target != null)
        {
            StopAllCoroutines();
            StartCoroutine(CameraTargettingCoroutine(p_Target, p_CamSpeed));
        }
    }

    IEnumerator CameraTargettingCoroutine(Transform p_Target, float p_CamSpeed = 0.05f)
    {
        Vector3 t_TargetPos = p_Target.position;
        Vector3 t_TargetFrontPos = t_TargetPos + p_Target.forward;
        Vector3 t_Direction = (t_TargetPos - t_TargetFrontPos).normalized;

        while(transform.position != t_TargetFrontPos || Quaternion.Angle(transform.rotation, Quaternion.LookRotation(t_Direction)) >= 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t_TargetFrontPos, p_CamSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(t_Direction), p_CamSpeed);
            yield return null;
        }
    }
}
