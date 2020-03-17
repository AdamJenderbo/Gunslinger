using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyHuntingArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bandit")
        {
            Bandit bandit = other.GetComponent<Bandit>();
            bandit.Capture();
            MoneyManager.instance.Increase(bandit.bounty);
            UI.instance.ShowPopup("You got a bounty of " + bandit.bounty + "$");
        }
    }
}