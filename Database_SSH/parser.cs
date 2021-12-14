using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_SSH
{
    class parser
    {
        int count { get; set; }
        public void parse_sensoor(List<string> data)
        {
            int index = 0;
            sensor_Datas = new List<sensoor_data>();

            foreach (var n in data)
            {
                if(count == 0)
                {
                    sensoor_data data1 = new sensoor_data();
                    sensor_Datas.Add(data1);
                }
                switch (count)
                {
                    case 0:
                        sensor_Datas[index].senoor_id = int.Parse(n);
                        count++;
                    break;
                    case 1:
                        sensor_Datas[index].name = n;
                        count++;
                        break;
                    case 2:
                        sensor_Datas[index].temp = float.Parse(n);
                        count++;
                            break;
                    case 3:
                        sensor_Datas[index].pressure = float.Parse(n);
                        count++;
                        break;
                    case 4:
                        sensor_Datas[index].light = int.Parse(n);
                        count++;
                        break;
                    case 5:
                        sensor_Datas[index].time = DateTime.Parse( n);
                        count = 0;
                        index++;
                        break;
                    default:
                        Console.WriteLine("Index is out of scope...");
                            break;
                }
            }
        }
        public void print()
        {
            foreach (var i in sensor_Datas)
            {
                Console.WriteLine("parsed sensoor data: ");
                Console.WriteLine(i.name);
                Console.WriteLine(i.senoor_id);
                Console.WriteLine(i.temp);
                Console.WriteLine(i.pressure);
                Console.WriteLine(i.light);
                Console.WriteLine(i.time);
            }
        }
        List<sensoor_data> sensor_Datas { get; set; }
    }
}
