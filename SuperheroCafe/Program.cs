
using System.Data;
using System.Data.SQLite;

//DisplayProduct(CreateConnection());
//DisplayProductWithCategory(CreateConnection());
//InsertCustomer(CreateConnection());
DeleteCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=bar.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        //Console.WriteLine("DB found");
    }
    catch
    {
        Console.WriteLine("DB not found");
    }
    return connection;
}

static void DisplayCustomers(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid,* FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}");
    }
    myConnection.Close();
}

static void DisplayProduct(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, ProductName, Price FROM product";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(1);
        int readerProductPrice = reader.GetInt32(2);

        Console.WriteLine($"{readerRowid}. {readerProductName}. Price: {readerProductPrice}");
    }
    myConnection.Close();
}

static void DisplayProductWithCategory(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();

    command.CommandText = "SELECT product.rowid, product.ProductName, ProductCategory.CategoryName FROM product " +
        "JOIN ProductCategory ON ProductCategory.rowid = Product.CategoryId";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(1);
        string readerProductCategory = reader.GetString(2);

        Console.WriteLine($"{readerRowid}. {readerProductName}. Category: {readerProductCategory}");
    }
    myConnection.Close();

}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName;

    Console.WriteLine("First name:");
    fName = Console.ReadLine();

    Console.WriteLine("Last name:");
    lName = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer (firstName,lastName) " +
        $"VALUES ('{fName}', '{lName}')";

    int rowsInserted = command.ExecuteNonQuery();
    Console.WriteLine($"{rowsInserted} new row has been inserted.");

    DisplayCustomers(myConnection);
}

static void DeleteCustomer(SQLiteConnection myConnection)
{

    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete:");

    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowsDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"{rowsDeleted} has been deleted.");
    DisplayCustomers(myConnection);


}
