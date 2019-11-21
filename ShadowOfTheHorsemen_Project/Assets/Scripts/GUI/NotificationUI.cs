using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUI : MonoBehaviour {


    public GameObject Notification;

	void Start () {

        Notification.SetActive(false);

	}

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Notification.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                Notification.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }
}
