#include <Servo.h> 

// create servos
Servo pitch;   
Servo yaw;
Servo roll;
int x,y,z;

void setup() {
  // initialize serial:
  Serial.begin(115200);
  pitch.attach(9);   
  roll.attach(6);
  yaw.attach(5);
}

void serialEvent(){

  
    x = Serial.parseInt();
    y = Serial.parseInt();
    z = Serial.parseInt();
    
    pitch.write(x);
    yaw.write(y);
    roll.write(z);
    
}
void loop() {}
