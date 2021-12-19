using System;

namespace Database_SSH
{
  public  class sensor_data
    {
       public int sensor_id { get; set; }
       public string name { get; set; }
       public float temp { get; set; }
       public float pressure { get; set; }
        public float humidity { get; set; }
        public int light { get; set; } 
        public DateTime time { get; set; }
    }
}