using UnityEngine;

[RequireComponent(typeof(PrometeoCarController))]
public class CarInputController : MonoBehaviour
{
    private PrometeoCarController PCC;
    
    private void Start()
    {
        PCC = GetComponent<PrometeoCarController>();
    
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (PCC.carSpeed > 0)
            {
                PCC.Brakes();
            }
            else
            {
                PCC.GoReverse();
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            PCC.GoForward();
        }
        if (Input.GetKey(KeyCode.A))
        {
            PCC.TurnLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            PCC.TurnRight();
        }
    }
}
