using UnityEngine;
using System.Collections;
using System.IO.Ports;



public class SerialScript : MonoBehaviour
{
    //Set the port (com3) and the baud rate. Standard serial read/write functional settings
    public static SerialPort stream = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
    //angleSnd is the string that contains the angles to be sent serially to the Arduino
    string angleSnd;
    //angleOrigin represent the original angles at program startup. used as reference point or origin
    float[] angleOrigin = { 0, 0, 0 };
    //angles updating in real-time which represent the position of the Oculus headset
    float[] angles = { 0, 0, 0 };   

    void Start()
    {
        //Open the Serial Stream
        stream.Open(); 
        //timeout for serial stream 
        stream.ReadTimeout = 500;
        //original pitch
        angleOrigin[0] = transform.eulerAngles.x;
        //original yaw
        angleOrigin[1] = transform.eulerAngles.y;
        //original roll
        angleOrigin[2] = transform.eulerAngles.z;

    }

    // Update is called once per frame
    void Update()
    {
        //pitch
        angles[0] = transform.eulerAngles.x;
        //yaw
        angles[1] = transform.eulerAngles.y;
        //roll
        angles[2] = transform.eulerAngles.z;

        //positional conversion:
        //the Oculus/Unity outputs a default of 0/360 degrees
        //while the Storm32 accepts 90 as the default position for each motor
        //the angular output for -270 must be converted to 0, and 90 to 180
        for (int i = 0; i <= 2; i++)
        {
            if (angleOrigin[i] > 180 && angleOrigin[i] < 360)
                angleOrigin[i] -= 360;
            if (angles[i] > 180 && angles[i] < 360)
                angles[i] -= 360;
        }

        //find angular difference between origin and current position
        angles[0] = angleOrigin[0] - angles[0];
        angles[1] = angleOrigin[1] - angles[1];
        angles[2] = angleOrigin[2] - angles[2];

        //angular cutoff function for Storm32 min/max (90 degree range, 45:135 on all axis)
        for (int i = 0; i <= 2; i++)
        {
            angles[i] += 90;
            
            if (angles[i] < 45)
                angles[i] = 45;
            if (angles[i] > 135)
                angles[i] = 135;
            
            angles[i] = (int)angles[i];
        }
        
        //invert pitch angles- the Oculus/Unity output inverted angles from the req'd angles for the Storm32 
        angles[0] = 180 - angles[0];

        //compose string to be sent serially
        angleSnd = "" + angles[0] + ", " + angles[1] + ", " + angles[2] + "\n";

        //write string out to Unity console
        Debug.Log(angleSnd);

        //write string across the serial port
        stream.Write(angleSnd);
        //flush string from serial port
        stream.BaseStream.Flush();

    }

}