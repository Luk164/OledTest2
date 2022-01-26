# WIP AM2320 sensor integration for nano-framework

- AM2320.pdf datasheet for AM2320 sensor
- Adafruit_AM2320.cpp tested and working library in c++
- OledTest2/AM2320.cs WIP sensor source code

## Notes

- Currently, the recieved and converted values seem to be wrong, returning tempereatures below absolute 0 and over 100% humidity
- HW setup has been tested with existing adafruit library to make sure wiring is ok

### A sample of buffer contents
Each line represents a single successful read, separated to eight numbers each representing a byte.

3 4 1 162 0 205 144 99

3 4 1 162 0 218 208 109

3 4 1 160 0 218 113 173

3 4 1 159 0 218 65 161