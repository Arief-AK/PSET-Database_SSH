namespace Database_SSH
{
    class Program
    {
        static void Main(string[] args)
        {
            var unparsed_list = DatabaseConnection.Connect();
            
            Parser py_parser = new Parser();
            Parser lht_parser = new Parser();

            py_parser.Parse(unparsed_list.First);
            lht_parser.Parse(unparsed_list.Second);

            py_parser.Print();
            lht_parser.Print();
        }
    }
}
