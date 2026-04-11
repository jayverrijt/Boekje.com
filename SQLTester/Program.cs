using MySqlConnector;

var conn = new MySqlConnection("Server=192.168.120.11;Port=3306;Database=boekje;User Id=jayv;Password=Woezel-2005!;SslMode=None;AllowPublicKeyRetrieval=True;");
try
{
    conn.Open();
    Console.WriteLine("CONNECTED");
}
catch (MySqlException ex)
{
    Console.WriteLine(ex.Message);
}