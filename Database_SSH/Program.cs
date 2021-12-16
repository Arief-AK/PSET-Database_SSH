﻿using System;
using System.Collections.Generic;
using MySqlConnector;
using Renci.SshNet;

namespace Database_SSH
{
    class Program
    {
        public static (SshClient SshClient, uint Port) ConnectSsh(string sshHostName, string sshUserName,
            string sshPassword = null, string sshKeyFile = null, string sshPassPhrase = null, int sshPort = 22,
            string databaseServer = "192.168.68.108", int databasePort = 8455)
        {
            // check arguments
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
            if (string.IsNullOrEmpty(sshHostName))
                throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
            if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
                throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
            if (string.IsNullOrEmpty(databaseServer))
                throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));
            
            // define the authentication methods to use (in order)
            var authenticationMethods = new List<AuthenticationMethod>();
            
            if (!string.IsNullOrEmpty(sshKeyFile))
            {
                authenticationMethods.Add(new PrivateKeyAuthenticationMethod(sshUserName,
                    new PrivateKeyFile(sshKeyFile, string.IsNullOrEmpty(sshPassPhrase) ? null : sshPassPhrase)));
            }
            if (!string.IsNullOrEmpty(sshPassword))
            {
                authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
            }

            // connect to the SSH server
            var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
            sshClient.Connect();
            
            // forward a local port to the database server and port, using the SSH server
            var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint) databasePort);
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();

            return (sshClient, forwardedPort.BoundPort);
            
        }
        
        //Select statement
        public static List<string> Select(string queryString, SshClient client, MySqlConnection connection)
        {
            string query = queryString;

            //Create a list to store the result
            List<string> list = new List<string>();

            //Open connection
            if (client.IsConnected)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                int fieldCOunt = dataReader.FieldCount;
                while (dataReader.Read())
                {
                    for (int i = 0; i < fieldCOunt; i++) {
                        list.Add(dataReader.GetValue(i).ToString());
                    }
                }

                //close Data Reader
                dataReader.Close();

                //return list to be displayed
                return list;
            }
            return list;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var server = "82.75.86.150";
            var sshUserName = "pi";
            var sshPassword = "SaxionTest";
            var databaseUserName = "arief";
            var databasePassword = "SaxionTest";

            var (sshClient, localPort) = ConnectSsh(server, sshUserName, sshPassword);
            
            Console.WriteLine($"\nHost: {sshClient.ConnectionInfo.Host}");
            Console.WriteLine($"Local port: {localPort.ToString()}");

            using (sshClient)
            {
                MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
                {
                    Server = "127.0.0.1",
                    Port = localPort,
                    UserID = databaseUserName,
                    Password = databasePassword,
                };

                using var connection = new MySqlConnection(csb.ConnectionString);
                connection.Open();
                
                Console.WriteLine($"\nDatabase connection status: {connection.State}");
                Console.WriteLine($"Database name: {connection}");
                Console.WriteLine($"Connection string: {connection.ConnectionString}");
                
                var py_query = "SELECT"+" * FROM PSET_test_db.PyData  ORDER BY idPyData desc LIMIT 3";
                var py_list = Select(py_query, sshClient, connection);

                var lht_query = "SELECT" + " * FROM PSET_test_db.LhtData  ORDER BY idLhtData desc LIMIT 3";
                var lht_list = Select(lht_query, sshClient, connection);
                // list[0] -> id
                // list[1] -> device name
                // list[2] -> temp
                // list[3] -> light  
                // list[4] -> preserure
                // list[5] -> timestap
                // list[6] -> 
                // list[

                // Added this comment for github commit

                //Console.WriteLine($"\nData: {list[6]} {list[1]}");
                parser py_pars = new parser();
                py_pars.pars(py_list);
                py_pars.print();

                Console.WriteLine( py_pars.sensor_list);

                parser lht_pars = new parser();
                py_pars.pars(lht_list);
                py_pars.print();
            }
        
        }
    }
}
