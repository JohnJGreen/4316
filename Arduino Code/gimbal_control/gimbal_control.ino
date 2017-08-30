#include <Servo.h> 

// create servos
Servo pitch;   
Servo yaw;
Servo roll;

void setup() {
    Serial.begin(115200);
    pitch.attach(7);   
    yaw.attach(5);
    roll.attach(6);
}

void loop() {
 
}

void serialEvent() {
   // set servo position.
   // 90 is center for all axis
   if (Serial.available()>0) { 
       // receives 3 angles separated per a comma
       pitch.write(Serial.parseInt()); //Returns first angle
       yaw.write(Serial.parseInt()); //Returns second angle
       roll.write(Serial.parseInt()); //Returns third angle
       
       Serial.read(); // read the '\n'. I am not sure if it is necessary.
       
   } 
}
