using UnityEngine;

public class TrainClick : MonoBehaviour {

    public int trainState = 0; // статус поезда (прибывает, отправился, прибыл, врезался)

    private void OnMouseUp()
    {
        if (trainState == 0)         //  прибывает
        {

        }
        else if (trainState == 1)   // отправился
        {  

        }
        else if (trainState == 2) {  // прибыл
            trainState = 1;
            GetComponent<TrainMove>().DepartTrain();
        }
        else if (trainState == 3)    // врезался
        {
            Destroy(gameObject);
        }
    }
}
