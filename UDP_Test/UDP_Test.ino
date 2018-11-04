#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
extern "C" {
#include "user_interface.h"
}
const char* ssid     = "ESP_WROOM_02";
const char* password = "hoge4444";

WiFiUDP UDPTestServer;
IPAddress udpReturnAddr = (192,168,150,64);
unsigned int UDPPort = 22222;

const int packetSize = 2;
byte packetBuffer[packetSize];

void setup() {
  Serial.begin(115200);
  delay(10);

  Serial.println();
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);
  
  WiFi.begin(ssid, password);
  WiFi.config(IPAddress(192, 168, 40, 100), IPAddress(192, 168, 40, 10), IPAddress(255, 255, 255, 0));
  WiFi.softAPConfig(IPAddress(192, 168, 40, 100), IPAddress(192, 168, 40, 10), IPAddress(255, 255, 255, 0));
  while (WiFi.softAPgetStationNum() <= 0) 
  {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");  
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());

  IPAddress myIP = WiFi.softAPIP();
  Serial.print("AP IP address: ");
  Serial.println(myIP);

  byte mac[6];
  WiFi.macAddress(mac);
  char buf[20];
  Serial.print("MAC address:   ");
  sprintf(buf,"%02X:%02X:%02X:%02X:%02X:%02X",mac[0],mac[1],mac[2],mac[3],mac[4],mac[5]);
  Serial.print(buf);
  sprintf(buf,"   %02x%02x%02x%02x%02x%02x",mac[0],mac[1],mac[2],mac[3],mac[4],mac[5]);
  Serial.println(buf);
  
  int a = UDPTestServer.begin(UDPPort);
  Serial.println(a);
}

int count = 0;

void loop() 
{
   if(WiFi.softAPgetStationNum() <= 0) 
   {
     delay(500);
     Serial.print(".");
   }
   else
   {
     handleUDPServer();
     Serial.print(WiFi.status());
     count += 1;
     if(count >= 100)
     {
      Serial.println("");
      count = 0;
     }
   }
   delay(100);
}

void handleUDPServer() 
{
   uint ADC_Value = 0;
   ADC_Value = system_adc_read();
   String str = String(ADC_Value) ;
   int a = str.length();
   char msg[a+1];
   str.toCharArray(msg, a+1);
   if(UDPTestServer.beginPacket(udpReturnAddr, UDPPort))
   {
    UDPTestServer.write("a", 2);
    UDPTestServer.endPacket();
   }
}
