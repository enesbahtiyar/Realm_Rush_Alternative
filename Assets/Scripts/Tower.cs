using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int buildCost = 50;
    [SerializeField] float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(Build());
    }

    IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);          
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandChild in child)
            {                
                grandChild.gameObject.SetActive(true);
            }
        }
    }
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance >= buildCost )
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(buildCost);
            return true;
        }

        return false;
    }
}
