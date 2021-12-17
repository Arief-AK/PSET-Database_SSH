using System;
using System.Collections.Generic;
using MySqlConnector;
using Renci.SshNet;

namespace Database_SSH
{
    class Program
    {
        static void Main(string[] args)
        {

            Connect_databases dataBases = new Connect_databases();
                var unparsed_list  =   dataBases.connect();
                parser py_pars = new parser();
            parser lht_pars = new parser();

            py_pars.pars(unparsed_list.First);
            lht_pars.pars(unparsed_list.Second);

           Console.WriteLine( py_pars.sensor_list[1].temp);
           Console.WriteLine( lht_pars.sensor_list[1].temp);

            py_pars.print();
            lht_pars.print();


            
        
        }
    }
}

