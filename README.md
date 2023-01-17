# SonyREAC1000Module
Crestron Simpl Module for Sony REAC1000 PTZ Tracking


This is a Crestron module I created to be used in Simpl Windows for a Sony REAC1000 device, to be able to toggle autotracking on and off. 

After you pulse initialize, the module will poll the device for the relevant info for PTZ tracking. In order to be able to start the PTZ tracking application(turn on autotracking), the application has to be configured and ready to go. I added serial strings for all the relevant parameters, if you change the parameter in simpl it'll send it over to the device. Once the application is set up, you can just hit start/stop. 
