# Tyre Degradation Simulation

### C# WPF application to calculate tyre degradation based on gathered circuit data

### Requirements
- Visual Studio 2019 with C# .NET tools
- [Newtonsoft Json.NET](https://github.com/JamesNK/Newtonsoft.Json) package

### Abstract
Calculating the average tyre degradation for a given track based on:
- the track degradation coefficient (data from TXT file)
- current temperature of the track (JSON data from [Open Weather API](https://openweathermap.org/api))
- coefficient provided by the tyre manufacturer (data from XML file)
