[![Build Status](https://travis-ci.org/veryhumble/MyStroem.svg?branch=master)](https://travis-ci.org/veryhumble/MyStroem)
# MyStroem
A simple App that gets Energy Data from MyStrom devices and sends them to InfluxDB


Ready for use Docker Image for Linux Arm Devices such as Raspberry PI or ASUS Tinkerboard :
`docker pull veryhumble/my_stroem`

Simple configuration with environment variables:

```
MYSTROM_DEVICES=TV=192.168.1.10;Lamp=192.168.1.11
INTERVAL_SECONDS=60
INFLUXDB_ADDRESS=http://127.0.0.1:8086
INFLUXDB_DATABASE=mystroem
INFLUXDB_MEASUREMENT=mystrom_sensor
INFLUXDB_USERNAME=****
INFLUXDB_PASSWORD=****
```


