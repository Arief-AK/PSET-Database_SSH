using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_SSH
{
  public  class sensoor_data
    {
       public int senoor_id { get; set; }
       public string name { get; set; }
       public float temp { get; set; }
       public float pressure { get; set; }
        public int light { get; set; } 
        public DateTime time { get; set; }
    }
}