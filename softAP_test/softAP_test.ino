#include <ESP8266WiFi.h>
#include <WiFiUDP.h>
extern "C" {
#include "user_interface.h"
}

//Access Point Setting
const char *APSSID = "ESP_WROOM_02";
const char *APPASS = "hoge4444";
unsigned int localPort = 22222;

WiFiUDP UDP;
char packetBuffer[255];

static const char *udpReturnAddr = "192.168.144.100";
static const int udpReturnPort = 22223;

void setup() {

  Serial.begin(115200);
  Serial.println();
  
  WiFi.softAP(APSSID, APPASS);

  IPAddress myIP = WiFi.softAPIP();
  Serial.print("AP IP address: ");
  Serial.println(myIP);
  UDP.begin(localPort);
}

void loop() {
<<<<<<< HEAD
  if(WiFi.softAPgetStationNum() >= 1)
  {
    uint ADC_Value = 0;
    ADC_Value = system_adc_read();
    delay(3);
    String str = String(ADC_Value) ;
    int a = str.length();
    char msg[a+1];
    str.toCharArray(msg, a+1);

    UDP.beginPacket(udpReturnAddr, udpReturnPort);
    UDP.write(msg, a+1);
    UDP.endPacket();
  }
=======
  uint ADC_Value = 0;
  ADC_Value = system_adc_read();
  delay(10);

  String str = String(ADC_Value) ;
  int a = str.length();
  char msg[a+1];
  str.toCharArray(msg, a+1);

  UDP.beginPacket(udpReturnAddr, udpReturnPort);
  UDP.write(msg,a+1);
  UDP.endPacket();  
>>>>>>> ee28ddeff3d9d6dafde4540de075f22c85255f36
}
