using TMPro;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    [SerializeField]
    float[] floatArray = new float[10];
    string[] stringArray = new string[10];
    int i, rInt, lastInt;
    float rfloat;
    TextMeshProUGUI uiText;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        uiText.text = "SpaceBar: Generate an average of two random numbers\n R: Get the range of the random numbers generated";
        floatArray = AssignArray(floatArray);
        stringArray = AssignMessageArray(stringArray);
        lastInt = 0;
    }


    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (i == floatArray.Length - 2)
            {
                while(rInt == lastInt)
                {
                    rInt = Random.Range(0, 9);
                }
                lastInt = rInt;
                uiText.text = stringArray[rInt] + Average(floatArray[i], floatArray[i+1]).ToString();
                i = 0;
            }
            else
            {
                while (rInt == lastInt)
                {
                    rInt = Random.Range(0, 9);
                }
                lastInt = rInt;
                uiText.text = stringArray[rInt] + Average(floatArray[i], floatArray[i+1]).ToString();
                i++;
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            uiText.text = "The Range is:\n" + Range(floatArray).ToString();
        }
    }

    float Average(float num1, float num2)
    {
        float avgNum = (num1 + num2)/2;

        return avgNum;
    }

    float[] AssignArray(float[] array)
    {
        for (int j = 0; j < array.Length; j++)
        {
            rfloat = Random.Range(0f, 100f);
            array[j] = rfloat;
        }

        return array;
    }

    string[] AssignMessageArray(string[] array)
    {
        array[0] = "Here’s the magic number for today. Let’s see what we get!\n";
        array[1] = "And the result is in… drumroll, please!\n";
        array[2] = "Here’s a little math surprise for you!\n";
        array[3] = "Hold tight, here comes the number of the moment!\n";
        array[4] = "Voilà, the number you’ve been waiting for!\n";
        array[5] = "Check it out, here’s a random number just for you!\n";
        array[6] = "Guess what? Here’s your number of the day!\n";
        array[7] = "Surprise! Here’s a number pulled straight from the universe!\n";
        array[8] = "Ready for it? Here’s your magic number!\n";
        array[9] = "And the random number is... hope you like it!\n";

        return array;
    }

    float Range(float[] array)
    {
        System.Array.Sort(array);

        float min = array[0];
        float max = array[array.Length - 1];

        return max - min;
    }
}
