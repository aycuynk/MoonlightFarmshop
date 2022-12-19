using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] List<Customer> inactiveCustomers;
    [SerializeField] Transform counter, spawnPoint;
    [SerializeField] float timer;

    private void CreateCustomer(int index)
    {
        if (inactiveCustomers[index] == null) return;
        inactiveCustomers[index].transform.position = spawnPoint.position;
        inactiveCustomers[index].gameObject.SetActive(true);
        GameObject targetPoint = Instantiate(counter.gameObject, counter.transform);
        inactiveCustomers[index].SetTarget(targetPoint.transform, spawnPoint);
        inactiveCustomers[index].SetMovement(true);
    }

    private void Update()
    {
        if (GameManager.instance.counterManager.GetFilledSlots().Count > 0)
        {
            if (timer <= 2)
            {
                timer += Time.deltaTime;
            }
            else
            {
                int rnd = Random.Range(0, inactiveCustomers.Count);
                if (!inactiveCustomers[rnd].gameObject.activeInHierarchy)
                {
                    CreateCustomer(rnd);
                }

                timer = 0;
            } 
        }
    }
}
