using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// player가 vaccine에 닿았다면, 한 번은 감염 예방

public class HandleVaccine : MonoBehaviour
{
    public AudioSource VaccinateSound;
    public GameObject vaccine;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Photon.Realtime.Player targetPlayer = collision.GetComponentInParent<PhotonView>().Owner;
            photonView.RPC("VaccineRPC", targetPlayer);
            VaccinateSound.Play();
            vaccine.SetActive(false);
        }
    }

    [PunRPC]
    private void VaccineRPC()   // 백신 접종
    {
        NetworkManager.instance.SetVaccinated(true);
    }

}
