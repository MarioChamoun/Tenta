using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenta
{
    class Program
    {
        static string CS = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NORTHWND;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static void Main(string[] args)
        {
            //ProductsByCategoryName("Confections");
            //SalesByTerritory();
            //EmployeesPerRegion();
            CustomersWithNamesLongerThan25Characters();


            Console.ReadLine();
        }
        public static void ProductsByCategoryName(string CategoryName)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
               
                SqlCommand cmd = new SqlCommand("SELECT dbo.Products.ProductName, dbo.Products.UnitPrice, dbo.Products.UnitsInStock FROM dbo.Categories INNER JOIN dbo.Products ON dbo.Categories.CategoryID = dbo.Products.CategoryID WHERE (dbo.Categories.CategoryName = @CategoryName)", con);
                cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Console.WriteLine("Productname: " + rdr.GetString(0));
                    Console.WriteLine("UnitPrice: " + rdr.GetDecimal(1));
                    Console.WriteLine("UnitsInStock: " + rdr.GetInt16(2));
                    Console.WriteLine();
                }
            }
        }

        public static void SalesByTerritory()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {

                SqlCommand cmd = new SqlCommand("SELECT Top 5 dbo.[Order Details].Quantity, dbo.Territories.TerritoryDescription FROM dbo.Employees INNER JOIN dbo.EmployeeTerritories ON dbo.Employees.EmployeeID = dbo.EmployeeTerritories.EmployeeID INNER JOIN dbo.Orders ON dbo.Employees.EmployeeID = dbo.Orders.EmployeeID INNER JOIN dbo.[Order Details] ON dbo.Orders.OrderID = dbo.[Order Details].OrderID INNER JOIN dbo.Territories ON dbo.EmployeeTerritories.TerritoryID = dbo.Territories.TerritoryID GROUP BY dbo.[Order Details].Quantity, dbo.Territories.TerritoryDescription, dbo.[Order Details].UnitPrice Order by UnitPrice DESC", con);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Console.Write(rdr.GetInt16(0) + " ");
                    Console.Write(rdr.GetString(1));
                    Console.WriteLine();
                }
            }
        }

        public static void EmployeesPerRegion()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {

                SqlCommand cmd = new SqlCommand("SELECT COUNT(dbo.Employees.EmployeeID) As AntalAnställda, dbo.Region.RegionDescription AS Region FROM dbo.Employees INNER JOIN dbo.EmployeeTerritories ON dbo.Employees.EmployeeID = dbo.EmployeeTerritories.EmployeeID INNER JOIN dbo.Territories ON dbo.EmployeeTerritories.TerritoryID = dbo.Territories.TerritoryID INNER JOIN dbo.Region ON dbo.Territories.RegionID = dbo.Region.RegionID GROUP BY dbo.Region.RegionDescription", con);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Console.Write(rdr.GetInt32(0) + " ");
                    Console.Write(rdr.GetString(1));
                    Console.WriteLine();
                }
            }
        }

        public static void CustomersWithNamesLongerThan25Characters()
        {
          using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                var namn = db.Customers.Where(x => x.CompanyName.Length > 25);
                foreach (var item in namn)
                {
                    Console.WriteLine("CompanyName: " + item.CompanyName);
                }
            }
        }
    }
}
