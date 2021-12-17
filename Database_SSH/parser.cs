using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_SSH
{
    class parser
    {
       private int count { get; set; }
       public List<sensor_data> sensor_list { get; private set; }

        public void pars(List<string> data)
        {
            bool check_py = false;
            int index = 0; //keep track of py sensor values
            sensor_list = new List<sensor_data>();

            foreach (var n in data)
            {
                if(count == 0)
                {
                    sensor_data data1 = new sensor_data();                                    
                        sensor_list.Add(data1);        
                }
                switch (count)
                {
                    case 0:
                        try
                        {
                            sensor_list[index].senoor_id = int.Parse(n);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{n}'");
                        }
                        count++;
                    break;
                    case 1:
                        try
                        {
                            sensor_list[index].name = n;
                            check_py = n.Contains("py");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{n}'");
                        }
                        count++;
                        break;
                    case 2:
                        try
                        {
                            sensor_list[index].temp = float.Parse(n);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{n}'");
                        }
                        count++;
                            break;
                    case 3:
                        if (check_py)
                        {
                            try
                            {
                                sensor_list[index].pressure = float.Parse(n);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{n}'");
                            }
                        }
                        else
                        {
                            try
                            {
                                sensor_list[index].humidity = float.Parse(n);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{n}'");
                            }
                        }
                        check_py = false;
                        count++;
                        break;
                    case 4:
                        try
                        {
                            sensor_list[index].light = int.Parse(n);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{n}'");
                        }
                        count++;
                        break;
                    case 5:
                        try
                        {
                            sensor_list[index].time = DateTime.Parse(n);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{n}'");
                        }
                        count = 0;
                        index++;
                        break;
                    default:
                        Console.WriteLine("Index is out of scope...");
                            break;
                }
            }
        }
        public void print()//just to show if we have the data does need to be delete after final sprint 
        {
            foreach (var i in sensor_list)
            {
                Console.WriteLine($"parsed {i.name} sensoor data: ");
                Console.WriteLine($" senor id: {i.senoor_id}");
                Console.WriteLine($" name: {i.name}");
                Console.WriteLine($" temp: {i.temp}");
                Console.WriteLine($" light: {i.light}");
                Console.WriteLine($" pressure: {i.pressure}");
                Console.WriteLine($" humidity: {i.humidity}");
                Console.WriteLine($" time: {i.time}");
            }
        }
    
    }
}
