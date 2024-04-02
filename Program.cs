using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Libraries needed to access SQL Server
using System.Data;
using System.Data.SqlClient;

// Libraries used when converting dates
using System.Globalization;


namespace DeviceDb
{
    // CLASS DEFINITIONS FOR DIFFERENT DEVICE TYPES
    // --------------------------------------------

    // A super class for all kind of devices
    // =====================================
    class Device
    {
        // Fields and properties
        // ----------------------

        string identity = "Uusi laite";
             
        public string Identity { get { return identity; } set { identity = value; } }

        string dateBought = "1.1.2000";
        public string DateBought { get { return dateBought; } set { dateBought = value; } }
    
        double price = 0.00d;
        public double Price { get { return price; } set { price = value; } }

        int warranty = 12;
        public int Warranty { get { return warranty; } set { warranty = value; } }

        string processorType = "N/A";
        public string ProcessorType { get { return processorType; } set { processorType = value; } }

        int amountRAM = 0;
        public int AmountRam { get { return amountRAM; } set { amountRAM = value; } }

        int storageCapacity = 0;
        public int StorageCapacity { get { return storageCapacity; } set { storageCapacity = value; } }

        // Constructors 
        // ------------
        public Device()
        {

        }

        // A constructor with one argument
        public Device(string identity)
        {
            this.identity = identity;
        }

        // Another constructor with all arguments
        public Device(string identity, string dateBought, double price, int warranty)
        {
            this.identity = identity;
            this.dateBought = dateBought;
            this.price = price;
            this.warranty = warranty;
        }

        // Other methods in the superclass
        // -------------------------------

        public void ShowPurchaseInfo()
        {
            // Show purchasing data
            Console.WriteLine();
            Console.WriteLine("Laitteen hankintatiedot");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Laitteen nimi: " + this.identity);
            Console.WriteLine("Ostopäivä: " + this.dateBought);
            Console.WriteLine("Hankintahinta: " + this.price);
            Console.WriteLine("Takuu: " + this.warranty + " kk");
        }

        // Show technical data
        public void ShowBasicTechnicalInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Laitteen tekniset tiedot");
            Console.WriteLine("--------------------- --");
            Console.WriteLine("Koneen nimi: " + Identity);
            Console.WriteLine("Prosessori: " + ProcessorType);
            Console.WriteLine("Keskusmuisti: " + AmountRam);
            Console.WriteLine("Levytila: " + StorageCapacity);
        }

        // Calculate the ending date of warranty
        public void CalculateWarrantyEndingDate()
        {
            // Convert date string to date time
            DateTime startDate = DateTime.ParseExact(this.DateBought, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            // Add warranty months to puchase date
            DateTime endDate = startDate.AddMonths(this.Warranty);

            // Convert it to ISO standard format
            endDate = endDate.Date;
            string isoDate = endDate.ToString("yyyy-MM-dd");
            Console.WriteLine("Takuu päättyy: " + isoDate);
        }

    }

    // Computer class, a subclass of Device
    // ====================================  
    class Computer : Device
    {

        // Constructors
        public Computer() : base()
        { }

        public Computer(string identity) : base(identity)
        { }       

    }

 
    // Class for Tablets, a subclass of Device class
    // =============================================
    class Tablet : Device
    {
        // Subclass specific fields and properties
        // ---------------------------------------
        string operatingSystem; 
        public string OperatingSystem { get { return operatingSystem; } set { operatingSystem = value; } }
        bool stylusEnabled = false;
        public bool StylusEnabled { get { return stylusEnabled; } set { stylusEnabled = value; } }

        // Constructors
        // -------------

        public Tablet() : base() { }

        public Tablet(string identity) : base(identity) { }

        // Methods specific to Tablet class
        // --------------------------------
        public void TabletInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Tabletin erityistiedot");
            Console.WriteLine("------------------------");
            Console.WriteLine("Käyttöjärjestelmä: " + OperatingSystem);
            Console.WriteLine("Kynätuki: " + StylusEnabled);
        }

    }

    // THE PROGRAM -> Program.exe
    // ===========================
    internal class Program
    {       
        static void Main(string[] args)
        {
            // Forever loop to run the program
            while (true)
            {
                Console.WriteLine("Minkä laitteen tiedot tallennetaan?");
                Console.Write("1 tietokone, 2 tabletti");
                string type = Console.ReadLine();

                // Choises for different type of devices
                switch (type)
                {
                    case "1":
                     
                        // Prompt user to enter device information
                        Console.Write("Nimi: ");
                        string computerIdentity = Console.ReadLine();
                        Computer computer = new Computer(computerIdentity);
                        Console.Write("Ostopäivä muodossa vvvv-kk-pp: ");
                        computer.DateBought = Console.ReadLine();
                        Console.Write("Hankintahinta: ");
                        string price = Console.ReadLine();

                        // Use error handling while trying to convert string values to numerical values
                        try
                        {
                            computer.Price = double.Parse(price);
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("Virheellinen hintatieto, käytä desimaalipilkkua (,) " + ex.Message);

                            break;
                        }

                        Console.Write("Takuun kesto kuukausina: ");
                        string warranty = Console.ReadLine();

                        try
                        {
                            computer.Warranty = int.Parse(warranty);
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("Virheellinen takuutieto, vain kuukausien määrä kokonaislukuna" + ex.Message);
                            break;

                        }


                        Console.Write("Prosessorin tyyppi: ");
                        computer.ProcessorType = Console.ReadLine();
                        Console.Write("Keskusmuistin määrä (GB): ");
                        string amountRAM = Console.ReadLine();

                        try
                        {
                            computer.AmountRam = int.Parse(amountRAM);
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("Virheellinen muistin määrä, vain kokonaisluvut sallittu" + ex.Message);
                            break;
                        }

                        Console.Write("Tallennuskapasiteetti (GB): ");
                        string storageCapasity = Console.ReadLine();

                        try
                        {
                            computer.StorageCapacity = int.Parse(storageCapasity);
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("Virheellinen tallennustilan koko, vain kokonaisluvut sallittu" + ex.Message);
                            break;
                        }

                        // Use methods to show entered values
                        computer.ShowPurchaseInfo();
                        computer.ShowBasicTechnicalInfo();

                        try
                        {
                            computer.CalculateWarrantyEndingDate();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ostopäivä virhellinen " + ex.Message);
                            break;
                        }

                        // Add the computer to Device table
                        Console.WriteLine("Lisätään tietokone Laite-tauluun");


                        // Create a connection to DB server
                        // --------------------------------

                        // Connection string to the database using Windows authentication
                        string connectionString = "Data Source=LAPTOP-JEKO509J\\SQLEXPRESS;Initial Catalog=Laiterekisteri;Integrated Security=True";

                        // Convert all data types to a string, add quates to original string values
                        string valuesString = "'" + computer.Identity + "', " + computer.Price.ToString() + ", '" + computer.DateBought + "', " + computer.Warranty.ToString() + ", '" + computer.ProcessorType + "', " + computer.AmountRam.ToString() + ", " + computer.StorageCapacity.ToString() + ", " + "'Tietokone'";

                        string insertCommand = "INSERT INTO dbo.Laite (Nimi, Hankintahinta, Hankintapaiva, Takuu, Prosesssori, Keskusmuisti, Tallennustila, Laitetyyppi) VALUES(" + valuesString +");";             
                        
                        // More readable way to create the SQL clause
                        string insertCommand2 = $"INSERT INTO dbo.Laite (Nimi, Hankintahinta, Hankintapaiva, Takuu, Prosessori, Keskusmuisti, Tallennustila, Laitetyyppi) VALUES('{computer.Identity}', {computer.Price},                        '{computer.DateBought}', {computer.Warranty}, '{computer.ProcessorType}', {computer.AmountRam}, {computer.StorageCapacity}, 'Tietokone');";

                        // Connect to the database inside using clause 
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(insertCommand2, connection);

                            //Execute the sql clause, no result set
                            command.ExecuteNonQuery();
                            connection.Close();
                        }

                        break;               


                    case "2":

                        // Prompt user to enter device information

                        Console.Write("Nimi: ");
                        string tabletIdentity = Console.ReadLine();
                        Tablet tablet = new Tablet(tabletIdentity);

                        Console.Write("Ostopäivä muodossa vvvv-kk-pp: ");
                        tablet.DateBought = Console.ReadLine();

                        Console.Write("Hankintahinta:");
                        string tabletPrice = Console.ReadLine();
                        tablet.Price = double.Parse(tabletPrice);

                        Console.Write("Takuun kesto kuukausina: ");
                        string tabletWarranty = Console.ReadLine();

                        Console.Write("Keskusmuistin määrä (GB): ");
                        string amountRam = Console.ReadLine();
                        tablet.AmountRam = int.Parse(amountRam);

                        Console.Write("Tallennuskapasiteetti (GB): ");
                        string storageCapacity = Console.ReadLine();
                        tablet.StorageCapacity = int.Parse(storageCapacity);

                        // Methods to show entered values
                        tablet.ShowPurchaseInfo();
                        tablet.ShowBasicTechnicalInfo();

                        string connection3String = "Data Source=LAPTOP-JEKO509J\\SQLEXPRESS;Initial Catalog=Laiterekisteri;Integrated Security=True";


                        string insertCommand3 = $"INSERT INTO dbo.Laite (Nimi, Hankintahinta, Hankintapaiva, Takuu, Prosessori, Keskusmuisti, Tallennustila, Laitetyyppi) VALUES ('{tablet.Identity}', {tablet.Price}, '{tablet.DateBought}',{tablet.Warranty}, '{tablet.ProcessorType}', {tablet.AmountRam}, {tablet.StorageCapacity}, 'Tabletti');";

                        using (SqlConnection connection3 = new SqlConnection(connection3String))
                        {
                            connection3.Open ();
                            SqlCommand command = new SqlCommand (insertCommand3, connection3);

                            command.ExecuteNonQuery();
                            connection3.Close();

                        }
                        break;
                        
                        

                    default:
                        Console.WriteLine("Virheellinen valinta, anna pelkkä numero");
                        break;



                }

                // Exit the program, exit the loop
                Console.WriteLine("Haluatko jatkaa K/e");
                string continueAnswer = Console.ReadLine();
                continueAnswer = continueAnswer.Trim();
                continueAnswer = continueAnswer.ToLower();
                if (continueAnswer == "e")
                {
                    // Connection string to the database using Windows authentication
                    string connectionString = "Data Source=LAPTOP-JEKO509J\\SQLEXPRESS;Initial Catalog=Laiterekisteri;Integrated Security=True";
                    // Query only systems with processor information to avoid null exception
                    string sqlQuery = "SELECT Nimi, Prosessori, Keskusmuisti, Tallennustila, Laitetyyppi FROM dbo.Laite WHERE Prosessori IS NOT NULL;";
                    // Select all from Laite table
                    // Connect to the database inside using clause

                    using (SqlConnection connection2 = new SqlConnection(connectionString))
                    {
                        connection2.Open();
                        SqlCommand command = new SqlCommand(sqlQuery, connection2);
                        command.CommandTimeout = 10;

                        // Execute the sql clause using reader
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                           Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}", reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4));

                            connection2.Close();

                            break;
                        }

                    }
                 
                    break;
                }

               
            }
            Console.ReadLine();
        }
    }
}


