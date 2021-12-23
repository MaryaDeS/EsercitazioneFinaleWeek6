using ADO.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOConnectedGestioneSpese.EsercitazioneFinale
{
    public class ConnectedModeClientGestioneSpese
    {
        static string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=GestioneSpese;Trusted_Connection=True;";

        #region
        internal static void ListSpese(bool prompt = true)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand selectCommand = connection.CreateCommand();
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM Spesa";

                SqlDataReader reader = selectCommand.ExecuteReader();

                Console.Clear();
                Console.WriteLine("---- Elenco Spese----");
                Console.WriteLine();
                Console.WriteLine("{0} {1}{2}{3},{4},{5},{6}", "ID", "Data", "Descrizione", "Utente", "Importo", "Approvata", "Categoria");
                Console.WriteLine(new String('-', 75));
                
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1}{2}{3},{4},{5},{6}", reader["ID"], reader["Insert_date"], reader["Description"], reader["Utente"], reader["Importo"], reader["Approvata"], reader ["Categoria"]);
                
                }
                Console.WriteLine(new String('-', 75));

            }
            catch (Exception ex)
            {
                Console.Write($"Errore: {ex.Message}");
            }
            finally
            {
                connection.Close();
                if (prompt)
                {
                    Console.WriteLine("---- Premi un tasto ----");
                    Console.ReadKey();
                }
            }
        }
        #endregion

        #region AddSpesa

        internal static void AddSpese()
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();
                SqlCommand insertCommand = connection.CreateCommand();
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO Spesa VALUES(@insertdate, @Description, @Utente, @Importo, @Categoria)";

                Console.Clear();
                Console.WriteLine("---- Inserire una nuova Spesa----");

                string Insert_date = ConsoleHelpers.GetData("Date");
                insertCommand.Parameters.AddWithValue("@insertdate", Insert_date);

                string descrizione = ConsoleHelpers.GetData("Descrizione");
                insertCommand.Parameters.AddWithValue("@Descrizione", descrizione);

                string utente = ConsoleHelpers.GetData("Utente");
                insertCommand.Parameters.AddWithValue("@Utente", utente);

                string Importo = ConsoleHelpers.GetData("Importo");
                insertCommand.Parameters.AddWithValue("@Importo", Importo);

                string Categoria = ConsoleHelpers.GetData("Categoria");
                insertCommand.Parameters.AddWithValue("@Categoria",Categoria);



                int result = insertCommand.ExecuteNonQuery();

                if (result != 1)
                    Console.WriteLine("Si è verificato un problema nell'inserimento della spesa.");
                else
                    Console.WriteLine("Spesa aggiunta.");
            }
            catch (Exception ex)
            {
                Console.Write($"Errore: {ex.Message}");
            }
            finally
            {
                connection.Close();
                Console.WriteLine("---- Premi un tasto ----");
                Console.ReadKey();
            }
        }
        #endregion
    }
}
