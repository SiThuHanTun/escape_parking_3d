using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] bool isParkingCar;
    [SerializeField] int carLength;
    [SerializeField] LayerMask layers;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = RoundVector(transform.position);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - transform.forward * (carLength - 0.5f), 0.2f); //back
        Gizmos.DrawWireSphere(transform.position - transform.forward * -0.5f, 0.2f); //front
    }

    // Move Forward Button
    public void MoveForward()
    {
        bool isFreeToMove = CheckDirectionBlocked(0);
        if (isFreeToMove)
        {
            Vector3 newPosition = transform.position + transform.forward;
            transform.position = RoundVector(newPosition); //Round the vector
            GameManger.instance.AddTurn();
        }
    }

    // Move Backward Button
    public void MoveBackward()
    {
        bool isFreeToMove = CheckDirectionBlocked(carLength);
        if (isFreeToMove)
        {
            Vector3 newPosition = transform.position - transform.forward;
            transform.position = RoundVector(newPosition); //Round the vector
            GameManger.instance.AddTurn();
        }
    }

    Vector3 RoundVector(Vector3 vectorToRound)
    {
        return new Vector3(Mathf.Round(vectorToRound.x)
                            , Mathf.Round(vectorToRound.y)
                            , Mathf.Round(vectorToRound.z));
    }

    //Check If Way is Free
    bool CheckDirectionBlocked(int positionOffset)
    {
        Vector3 positionToCheck = transform.position - transform.forward * (positionOffset - 0.5f);
        var masks = layers;
        var myCollider = GetComponentInChildren<Collider>();
        myCollider.enabled = false;

        Collider[] colliders = Physics.OverlapSphere(positionToCheck, 0.2f, layers);

        myCollider.enabled = true;

        if (colliders.Length > 0)
        {
            return false;
        }

        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal") && isParkingCar)
        {
            GameManger.instance.ShowWinPanel();
            //Winner
            Debug.Log("You Win!!!");
        }
    }
}
