# DataConveyer_HierarchicalJsonToCsv

DataConveyer_HierarchicalJsonToCsv is a sample console application to demonstate how Data Conveyer translates JSON data
of any hierarchy nesting into a 2-dimensional CSV structure. To do so, Data Conveyer assigns CSV field names confirming to
a dot-notation convention. Specifically, the field name is the same as the Json.NET [Path](https://www.newtonsoft.com/json/help/html/P_Newtonsoft_Json_JsonReader_Path.htm)
property of the corresponding JSON element.

The application works in reverse as well. Given a CSV file, it will examine the field names according to the notation mentioned above and 
create a JSON file with corresponding hierarchy nesting.

There are several sample input files located in the Data subfolder. They contain data on population, drivers and vehicles by US states.
The data was collected from public sources, such as https://www.census.gov/ or https://www.bts.gov/. Different files sort/group this data
differently, thus forming diffent nesting hierarchies. Sample fragments below illustrate a nested JSON hierarchy and a corresponding
flat CSV structure:

**JSON:**
```
[
  {
    "State": "Alabama",
    "Years": [
      {
        "Year": "2009",
        "Data": {
          "Population": "",
          "Drivers": "3782284",
          "Vehicles": "4610850"
        }
      },
      {
        "Year": "2010",
        "Data": {
          "Population": "4785514",
      ...
```

**CSV:**
```
State,Years[0].Year,Years[0].Data.Population,Years[0].Data.Drivers,Years[0].Data.Vehicles,Years[1].Year,Years[1].Data.Population,Years[1].Data.Drivers,Years[1].Data.Vehicles
Alabama,2009,,3782284,4610850,2010,4785514,3805751,4653840
...
```

## Installation

* Fork this repository and clone it onto your local machine, or

* Download this repository onto your local machine.

## Usage

1. Open DataConveyer_HierarchicalJsonToCsv solution in Visual Studio.

2. Build and run the application, e.g. hit F5.

    - A console window with directions will show up.

3. Copy any number of input files (either CSV or JSON) into the ...Data\In folder.

    - A message that the file was detected will appear in the console window.

4. Hit the spacebar to start the conversions.

    - The files will get processed as reported in the console window.

5. Review the contents of the output files placed in the ...Data\Out folder.

6. (optional) Repeat steps 3-5 for other additional input file(s).

7. To exit application, hit Enter key into the console window.


## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/)

## Copyright

```
Copyright © 2021 Mavidian Technologies Limited Liability Company. All Rights Reserved.
```
