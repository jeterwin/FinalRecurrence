using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.AI;
public class RespawnChase : MonoBehaviour
{
    #region Transforms
    public Transform Player;
    public Transform RespawnPoint;
    public Transform Enemy;
    public Transform EnemyRespawnPoint;
    #endregion
    public Image AttackImg;
    public Image CloseChaseImg;
    public AudioSource Audio;
    public AudioSource Audio1;
    public UnityEvent Event;

    public void RespawanPlayer()
    {
        Event.Invoke();
        Player.position = new Vector3(RespawnPoint.position.x, RespawnPoint.position.y, RespawnPoint.position.z);
        Player.rotation = Quaternion.EulerAngles(RespawnPoint.rotation.x, RespawnPoint.rotation.y, RespawnPoint.rotation.z);
        Enemy.position = new Vector3(EnemyRespawnPoint.position.x, EnemyRespawnPoint.position.y, EnemyRespawnPoint.position.z);
        Enemy.rotation = Quaternion.EulerAngles(EnemyRespawnPoint.rotation.x, EnemyRespawnPoint.rotation.y, EnemyRespawnPoint.rotation.z);
        Enemy.gameObject.SetActive(false);
        AttackImg.gameObject.SetActive(false);
        Enemy.gameObject.SetActive(false);
        //Player.gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }
    public void StopAudio()
    {
        Audio.Stop();
        Audio1.enabled = false;
    }
    public void PlayAudio()
    {
        Audio1.Play();
    }
    public void IdleAnimation()
    {
        CloseChaseImg.gameObject.GetComponent<Animator>().Play("IdleState");
    }
    public void RespawanOPlayer()
    {

        Player.position = new Vector3(RespawnPoint.position.x, RespawnPoint.position.y, RespawnPoint.position.z);
        Player.rotation = Quaternion.EulerAngles(RespawnPoint.rotation.x, RespawnPoint.rotation.y, RespawnPoint.rotation.z);

    }
}
