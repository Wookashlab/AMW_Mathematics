using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMW_Mathematics.Function
{
    class UnitFactor
    {


        public double unitF;
        public double temp;

        public UnitFactor(string category, string unit1, string unit2) {

            var list = new List<Tuple<string, string, double, double>>();

            switch (category)
            {
                case "Time":
                    list.Add(new Tuple<string, string, double, double>("microseconds", "miliseconds", 0.001, 1000));
                    list.Add(new Tuple<string, string, double, double>("microseconds", "seconds", 1E-06, 1000000));
                    list.Add(new Tuple<string, string, double, double>("microseconds", "minutes", 1.66666666666667E-08, 60000000));
                    list.Add(new Tuple<string, string, double, double>("microseconds", "hours", 2.77777777777778E-10, 3600000000));
                    list.Add(new Tuple<string, string, double, double>("microseconds", "days", 1.15740740740741E-11, 86400000000));

                    list.Add(new Tuple<string, string, double, double>("miliseconds", "seconds", 0.001, 1000));
                    list.Add(new Tuple<string, string, double, double>("miliseconds", "minutes", 1.66666666666667E-05, 60000));
                    list.Add(new Tuple<string, string, double, double>("miliseconds", "hours", 2.77777777777778E-07, 3600000));
                    list.Add(new Tuple<string, string, double, double>("miliseconds", "days", 1.15740740740741E-08, 86400000));

                    list.Add(new Tuple<string, string, double, double>("seconds", "minutes", 0.0166666666666667, 60));
                    list.Add(new Tuple<string, string, double, double>("seconds", "hours", 0.000277777777777778, 3600));
                    list.Add(new Tuple<string, string, double, double>("seconds", "days", 1.15740740740741E-05, 86400));

                    list.Add(new Tuple<string, string, double, double>("minutes", "hours", 0.0166666666666667, 60));
                    list.Add(new Tuple<string, string, double, double>("minutes", "days", 0.000694444444444444, 1440));

                    list.Add(new Tuple<string, string, double, double>("hours", "days", 0.0416666666666667, 24));
                    break;
                case "Temperature": //Farenhite problem
                    list.Add(new Tuple<string, string, double, double>("degrees Celsius", "degrees Fahrenheit", 1.8, 32));
                    list.Add(new Tuple<string, string, double, double>("degrees Celsius", "Kelvins", 1, 273.15));
                    list.Add(new Tuple<string, string, double, double>("Kelvins", "degrees Celsius", 1, -273.15));
                    list.Add(new Tuple<string, string, double, double>("Kelvins", "degrees Fahrenheit", 1.8, -459.67));

                    break;
                case "Velocity":   
                    list.Add(new Tuple<string, string, double, double>("meters/second", "miles/hour", 2.23693629, 0.44704));
                    list.Add(new Tuple<string, string, double, double>("meters/second", "feet/hour", 11811, 8.46668360003387E-05));
                    list.Add(new Tuple<string, string, double, double>("meters/second", "kilometers/hour", 3.6, 0.277777778));

                    list.Add(new Tuple<string, string, double, double>("miles/hour", "feet/hour", 5280, 0.000189393939));
                    list.Add(new Tuple<string, string, double, double>("miles/hour", "kilometers/hour", 1.609344, 0.621371192));

                    list.Add(new Tuple<string, string, double, double>("feet/hour", "kilometers/hour", 0.0003048, 3280.83333333333));
                    break;
                case "Mass":
                    list.Add(new Tuple<string, string, double, double>("miligrams", "grams", 0.001, 1000));
                    list.Add(new Tuple<string, string, double, double>("miligrams", "kilograms", 1E-06, 1000000));
                    list.Add(new Tuple<string, string, double, double>("miligrams", "tonnes", 1E-09, 1000000000));
                    list.Add(new Tuple<string, string, double, double>("miligrams", "ounces", 3.5273962105112E-05, 28349.523));
                    list.Add(new Tuple<string, string, double, double>("miligrams", "pounds", 2.20462262184878E-06, 453592.37));
                    list.Add(new Tuple<string, string, double, double>("miligrams", "short tons", 1.10231131092439E-09, 907184740));
                    list.Add(new Tuple<string, string, double, double>("miligrams", "long tons", 9.84206527417328E-10, 1016046909));

                    list.Add(new Tuple<string, string, double, double>("grams", "kilograms", 0.001, 1000));
                    list.Add(new Tuple<string, string, double, double>("grams", "tonnes", 1E-06, 1000000));
                    list.Add(new Tuple<string, string, double, double>("grams", "ounces", 0.035273962105112, 28.349523));
                    list.Add(new Tuple<string, string, double, double>("grams", "pounds", 0.00220462262184878, 453.59237));
                    list.Add(new Tuple<string, string, double, double>("grams", "short tons", 1.10231131092439E-06, 907184.74));
                    list.Add(new Tuple<string, string, double, double>("grams", "long tons", 9.84206527417328E-07, 1016046.909));

                    list.Add(new Tuple<string, string, double, double>("kilograms", "tonnes", 0.001, 1000));
                    list.Add(new Tuple<string, string, double, double>("kilograms", "ounces", 35.273962105112, 0.028349523));
                    list.Add(new Tuple<string, string, double, double>("kilograms", "pounds", 2.20462262184878, 0.45359237));
                    list.Add(new Tuple<string, string, double, double>("kilograms", "short tons", 0.00110231131092439, 907.18474));
                    list.Add(new Tuple<string, string, double, double>("kilograms", "long tons", 0.000984206527417328, 1016.046909));

                    list.Add(new Tuple<string, string, double, double>("tonnes", "ounces", 35273.962105112, 2.8349523E-05));
                    list.Add(new Tuple<string, string, double, double>("tonnes", "pounds", 2204.62262184878, 0.00045359237));
                    list.Add(new Tuple<string, string, double, double>("tonnes", "short tons", 1.10231131092439, 0.90718474));
                    list.Add(new Tuple<string, string, double, double>("tonnes", "long tons", 0.984206527417328, 1.016046909));

                    list.Add(new Tuple<string, string, double, double>("ounces", "pounds", 0.0624999997244222, 16.0000000705479));
                    list.Add(new Tuple<string, string, double, double>("ounces", "short tons", 3.12499998622111E-05, 32000.0001410958));
                    list.Add(new Tuple<string, string, double, double>("ounces", "long tons", 2.79017855857677E-05, 35840.0001650821));

                    list.Add(new Tuple<string, string, double, double>("pounds", "short tons", 0.0005, 2000));
                    list.Add(new Tuple<string, string, double, double>("pounds", "long tons", 0.000446428571340696, 2240.00000044092));

                    list.Add(new Tuple<string, string, double, double>("short tons", "long tons", 0.892857142681392, 1.12000000022046));

                    break;
                case "Area":
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "squere centimeters", 0.01, 100));
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "squere meters", 1E-06, 1000000));
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "hectares", 1E-10, 10000000000));
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "squere kilometers", 1E-12, 1000000000000));
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "squere inches", 0.0015500031000062, 645.16));
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "squere feet", 1.07639104167097E-05, 92903.04));
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "squere yards", 1.19599004630108E-06, 836127.36));
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "acres", 2.47105381467165E-10, 4046856422.4));
                    list.Add(new Tuple<string, string, double, double>("squere milimeters", "square miles", 3.86102158542446E-13, 2589988110336));

                    list.Add(new Tuple<string, string, double, double>("squere centimeters", "squere meters", 0.0001, 10000));
                    list.Add(new Tuple<string, string, double, double>("squere centimeters", "hectares", 1E-08, 100000000));
                    list.Add(new Tuple<string, string, double, double>("squere centimeters", "squere kilometers", 1E-10, 10000000000));
                    list.Add(new Tuple<string, string, double, double>("squere centimeters", "squere inches", 0.15500031000062, 6.4516));
                    list.Add(new Tuple<string, string, double, double>("squere centimeters", "squere feet", 0.00107639104167097, 929.0304));
                    list.Add(new Tuple<string, string, double, double>("squere centimeters", "squere yards", 0.000119599004630108, 8361.2736));
                    list.Add(new Tuple<string, string, double, double>("squere centimeters", "acres", 2.47105381467165E-08, 40468564.224));
                    list.Add(new Tuple<string, string, double, double>("squere centimeters", "square miles", 3.86102158542446E-11, 25899881103.36));

                    list.Add(new Tuple<string, string, double, double>("squere meters", "hectares", 0.0001, 10000));
                    list.Add(new Tuple<string, string, double, double>("squere meters", "squere kilometers", 1E-06, 1000000));
                    list.Add(new Tuple<string, string, double, double>("squere meters", "squere inches", 1550.0031000062, 0.00064516));
                    list.Add(new Tuple<string, string, double, double>("squere meters", "squere feet", 10.7639104167097, 0.09290304));
                    list.Add(new Tuple<string, string, double, double>("squere meters", "squere yards", 1.19599004630108, 0.83612736));
                    list.Add(new Tuple<string, string, double, double>("squere meters", "acres", 0.000247105381467165, 4046.8564224));
                    list.Add(new Tuple<string, string, double, double>("squere meters", "square miles", 3.86102158542446E-07, 2589988.110336));

                    list.Add(new Tuple<string, string, double, double>("hectares", "squere kilometers", 0.01, 100));
                    list.Add(new Tuple<string, string, double, double>("hectares", "squere inches", 15500031.000062, 6.4516E-08));
                    list.Add(new Tuple<string, string, double, double>("hectares", "squere feet", 107639.104167097, 9.290304E-06));
                    list.Add(new Tuple<string, string, double, double>("hectares", "squere yards", 11959.9004630108, 8.3612736E-05));
                    list.Add(new Tuple<string, string, double, double>("hectares", "acres", 2.47105381467165, 0.40468564224));
                    list.Add(new Tuple<string, string, double, double>("hectares", "square miles", 0.00386102158542446, 258.9988110336));

                    list.Add(new Tuple<string, string, double, double>("squere kilometers", "squere inches", 1550003100.0062, 6.4516E-10));
                    list.Add(new Tuple<string, string, double, double>("squere kilometers", "squere feet", 10763910.4167097, 9.290304E-08));
                    list.Add(new Tuple<string, string, double, double>("squere kilometers", "squere yards", 1195990.04630108, 8.3612736E-07));
                    list.Add(new Tuple<string, string, double, double>("squere kilometers", "acres", 247.105381467165, 0.0040468564224));
                    list.Add(new Tuple<string, string, double, double>("squere kilometers", "square miles", 0.386102158542446, 2.589988110336));

                    list.Add(new Tuple<string, string, double, double>("squere inches", "squere feet", 0.00694444444444444, 144));
                    list.Add(new Tuple<string, string, double, double>("squere inches", "squere yards", 0.000771604938271605, 1296));
                    list.Add(new Tuple<string, string, double, double>("squere inches", "acres", 1.59422507907356E-07, 6272640));
                    list.Add(new Tuple<string, string, double, double>("squere inches", "square miles", 2.49097668605244E-10, 4014489600));

                    list.Add(new Tuple<string, string, double, double>("squere feet", "squere yards", 0.111111111111111, 9));
                    list.Add(new Tuple<string, string, double, double>("squere feet", "acres", 2.29568411386593E-05, 43560));
                    list.Add(new Tuple<string, string, double, double>("squere feet", "square miles", 3.58700642791552E-08, 27878400));

                    list.Add(new Tuple<string, string, double, double>("squere yards", "acres", 0.000206611570247934, 4840));
                    list.Add(new Tuple<string, string, double, double>("squere yards", "square miles", 3.22830578512397E-07, 3097600));

                    list.Add(new Tuple<string, string, double, double>("acres", "square miles", 0.0015625, 640));
                    break;
                case "Length":
                    list.Add(new Tuple<string, string, double, double>("microns", "milimeters", 0.001, 1000));
                    list.Add(new Tuple<string, string, double, double>("microns", "centimeters", 0.0001, 10000));
                    list.Add(new Tuple<string, string, double, double>("microns", "meters", 1E-06, 1000000));
                    list.Add(new Tuple<string, string, double, double>("microns", "kilometers", 1E-09, 1000000000));
                    list.Add(new Tuple<string, string, double, double>("microns", "inches", 3.93700787401575E-05, 25400));
                    list.Add(new Tuple<string, string, double, double>("microns", "rods", 5.02921015550418E-06, 198838.38));
                    list.Add(new Tuple<string, string, double, double>("microns", "feet", 3.28083989501312E-06, 304800));
                    list.Add(new Tuple<string, string, double, double>("microns", "yards", 1.09361329833771E-06, 914400));
                    list.Add(new Tuple<string, string, double, double>("microns", "fathoms", 5.46806649168854E-07, 1828800));
                    list.Add(new Tuple<string, string, double, double>("microns", "miles", 6.21371192237334E-10, 1609344000));
                    list.Add(new Tuple<string, string, double, double>("microns", "nautical miles", 5.39956803455723E-10, 1852000000));
                    list.Add(new Tuple<string, string, double, double>("microns", "astronomical units", 6.68455676876547E-18, 1.4959855E+17));

                    list.Add(new Tuple<string, string, double, double>("milimeters", "centimeters", 0.1, 10));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "meters", 0.001, 1000));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "kilometers", 1E-06, 1000000));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "inches", 0.0393700787401575, 25.4));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "rods", 0.00502921015550418, 198.83838));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "feet", 0.00328083989501312, 304.8));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "yards", 0.00109361329833771, 914.4));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "fathoms", 0.000546806649168854, 1828.8));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "miles", 6.21371192237334E-07, 1609344));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "nautical miles", 5.39956803455724E-07, 1852000));
                    list.Add(new Tuple<string, string, double, double>("milimeters", "astronomical units", 6.68455676876547E-15, 149598550000000));

                    list.Add(new Tuple<string, string, double, double>("centimeters", "meters", 0.01, 100));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "kilometers", 1E-05, 100000));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "inches", 0.393700787401575, 2.54));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "rods", 0.0502921015550418, 19.883838));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "feet", 0.0328083989501312, 30.48));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "yards", 0.0109361329833771, 91.44));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "fathoms", 0.00546806649168854, 182.88));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "miles", 6.21371192237334E-06, 160934.4));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "nautical miles", 5.39956803455724E-06, 185200));
                    list.Add(new Tuple<string, string, double, double>("centimeters", "astronomical units", 6.68455676876547E-14, 14959855000000));

                    list.Add(new Tuple<string, string, double, double>("meters", "kilometers", 0.001, 1000));
                    list.Add(new Tuple<string, string, double, double>("meters", "inches", 39.3700787401575, 0.0254));
                    list.Add(new Tuple<string, string, double, double>("meters", "rods", 5.02921015550418, 0.19883838));
                    list.Add(new Tuple<string, string, double, double>("meters", "feet", 3.28083989501312, 0.3048));
                    list.Add(new Tuple<string, string, double, double>("meters", "yards", 1.09361329833771, 0.9144));
                    list.Add(new Tuple<string, string, double, double>("meters", "fathoms", 0.546806649168854, 1.8288));
                    list.Add(new Tuple<string, string, double, double>("meters", "miles", 0.000621371192237334, 1609.344));
                    list.Add(new Tuple<string, string, double, double>("meters", "nautical miles", 0.000539956803455724, 1852));
                    list.Add(new Tuple<string, string, double, double>("meters", "astronomical units", 6.68455676876547E-12, 149598550000));

                    list.Add(new Tuple<string, string, double, double>("kilometers", "inches", 39370.0787401575, 2.54E-05));
                    list.Add(new Tuple<string, string, double, double>("kilometers", "rods", 5029.21015550418, 0.00019883838));
                    list.Add(new Tuple<string, string, double, double>("kilometers", "feet", 3280.83989501312, 0.0003048));
                    list.Add(new Tuple<string, string, double, double>("kilometers", "yards", 1093.61329833771, 0.0009144));
                    list.Add(new Tuple<string, string, double, double>("kilometers", "fathoms", 546.806649168854, 0.0018288));
                    list.Add(new Tuple<string, string, double, double>("kilometers", "miles", 0.621371192237334, 1.609344));
                    list.Add(new Tuple<string, string, double, double>("kilometers", "nautical miles", 0.539956803455724, 1.852));
                    list.Add(new Tuple<string, string, double, double>("kilometers", "astronomical units", 6.68455676876547E-09, 149598550));

                    list.Add(new Tuple<string, string, double, double>("inches", "rods", 0.127741937949806, 7.82828267716535));
                    list.Add(new Tuple<string, string, double, double>("inches", "feet", 0.0833333333333333, 12));
                    list.Add(new Tuple<string, string, double, double>("inches", "yards", 0.0277777777777778, 36));
                    list.Add(new Tuple<string, string, double, double>("inches", "fathoms", 0.0138888888888889, 72));
                    list.Add(new Tuple<string, string, double, double>("inches", "miles", 1.57828282828283E-05, 63360));
                    list.Add(new Tuple<string, string, double, double>("inches", "nautical miles", 1.37149028077754E-05, 72913.3858267717));
                    list.Add(new Tuple<string, string, double, double>("inches", "astronomical units", 1.69787741926643E-13, 5889706692913.39));

                    list.Add(new Tuple<string, string, double, double>("rods", "feet", 0.652356889763779, 1.53290325539768));
                    list.Add(new Tuple<string, string, double, double>("rods", "yards", 0.217452296587927, 4.59870976619303));
                    list.Add(new Tuple<string, string, double, double>("rods", "fathoms", 0.108726148293963, 9.19741953238605));
                    list.Add(new Tuple<string, string, double, double>("rods", "miles", 0.00012355244124314, 8093.72918849972));
                    list.Add(new Tuple<string, string, double, double>("rods", "nautical miles", 0.000107364136069114, 9314.09720799375));
                    list.Add(new Tuple<string, string, double, double>("rods", "astronomical units", 1.32914643891936E-12, 752362546908.7));

                    list.Add(new Tuple<string, string, double, double>("feet", "yards", 0.333333333333333, 3));
                    list.Add(new Tuple<string, string, double, double>("feet", "fathoms", 0.166666666666667, 6));
                    list.Add(new Tuple<string, string, double, double>("feet", "miles", 0.000189393939393939, 5280));
                    list.Add(new Tuple<string, string, double, double>("feet", "nautical miles", 0.000164578833693305, 6076.1154855643));
                    list.Add(new Tuple<string, string, double, double>("feet", "astronomical units", 0.000164578833693305, 490808891076.115));

                    list.Add(new Tuple<string, string, double, double>("yards", "fathoms", 0.5, 2));
                    list.Add(new Tuple<string, string, double, double>("yards", "miles", 0.000568181818181818, 1760));
                    list.Add(new Tuple<string, string, double, double>("yards", "nautical miles", 0.000493736501079914, 2025.37182852143));
                    list.Add(new Tuple<string, string, double, double>("yards", "astronomical units", 6.11235870935915E-12, 163602963692.038));

                    list.Add(new Tuple<string, string, double, double>("fathoms", "miles", 0.00113636363636364, 880));
                    list.Add(new Tuple<string, string, double, double>("fathoms", "nautical miles", 0.000987473002159827, 1012.68591426072));
                    list.Add(new Tuple<string, string, double, double>("fathoms", "astronomical units", 1.22247174187183E-11, 81801481846.0192));

                    list.Add(new Tuple<string, string, double, double>("miles", "nautical miles", 0.868976241900648, 1.15077944802354));
                    list.Add(new Tuple<string, string, double, double>("miles", "astronomical units", 1.07577513284721E-08, 92956229.3704764));

                    list.Add(new Tuple<string, string, double, double>("nautical miles", "astronomical units", 1.23797991357537E-08, 80776754.8596112));


                    break;


            }
            foreach (var lst in list)
            {
                if (unit1.Equals(unit2))
                    unitF = 1;
                else
                {
                    if (lst.Item1.Equals(unit1) && lst.Item2.Equals(unit2))
                    {
                        unitF = lst.Item3;
                        if (category.Contains("Temperature"))
                        {
                            temp = lst.Item4;
                        }
                        break;
                    }
                    else
                    {
                        if (lst.Item1.Equals(unit2) && lst.Item2.Equals(unit1))
                        {
                            if (category.Contains("Temperature"))
                            {

                            }
                            else { 
                            unitF = lst.Item4;
                            break;
                            }

                        }
                    }
                }
            }
        }
    }

   
}
