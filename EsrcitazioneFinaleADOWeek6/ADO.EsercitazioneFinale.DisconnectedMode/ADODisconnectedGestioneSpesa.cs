using Microsoft.Extensions.Configuration;
using SupportLibraryGestioneSpeseDisconnected;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.EsercitazioneFinale.DisconnectedMode
{
    static class ADODisconnectedGestioneSpesa
    {
        static IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(@"C:\Users\maria.de.stefano\Desktop\Academy\Week6_ADONet\EsrcitazioneFinaleADOWeek6\ADO.EsercitazioneFinale.DisconnectedMode\ADO.EsercitazioneFinale.DisconnectedMode")
             .AddJsonFile("appsetting.json")
             .Build();

        static string ConnectionString = config.GetConnectionString("GestioneSpeseDB");

        #region Private members

        private static SqlConnection connection;
        private static DataSet SpesaDs = new DataSet();
        private static SqlDataAdapter SpesaAdapter = new SqlDataAdapter();

        private static List<Spesa> spesaList = new List<Spesa>();


        private static SqlCommand spesaDeleteCmd;
        private static SqlCommand spesaSelectCmd;


        #endregion

        static void  DisconnectedModeClient()
        {
            connection = new SqlConnection(ConnectionString);

            try
            {
                SpesaDs = new DataSet();
                SpesaAdapter = new SqlDataAdapter();

                #region Select Command

                spesaSelectCmd = new SqlCommand("SELECT * FROM Spesa ORDER BY Utente", connection);
                SpesaAdapter.SelectCommand = spesaSelectCmd;

                #endregion



                #region Delete Command

                spesaDeleteCmd = connection.CreateCommand();
                spesaDeleteCmd.CommandType = System.Data.CommandType.Text;
                spesaDeleteCmd.CommandText = "DELETE FROM Spesa WHERE ID = @id";

                spesaDeleteCmd.Parameters.Add(
                    new SqlParameter(
                        "@id",
                        SqlDbType.Int,
                        100,
                        "Id"
                    )
                );

                SpesaAdapter.DeleteCommand = spesaDeleteCmd;

                #endregion



                SpesaAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                SpesaAdapter.Fill(SpesaDs, "Spesa");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DisconnectedModeClient] Ctor Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }



        internal static void GroupByUtente()
        {
            ListSpesa(true);
            var groupTicket = spesaList.GroupBy(x => x.Utente).Select(x => new
            {
                x.Key,
                Spesa = x.Select(x => x)
            });
            foreach (var item in groupSpese)
            {
                Console.WriteLine(item.Key);
                foreach (var spesa in item.Spese)
                {
                    Console.WriteLine("{0,-5}{1,-40}{2,10}{3,20}{4,5}",
                     item.ID, item.Date, item.Descrizione, item.Utente, item.Importo, item.Approvata);
                }
            }

            Console.WriteLine("Premi un tasto per tornare al menù");
            Console.ReadKey();
        }



        public static void DeleteSpesa()
        {
            Console.Clear();
            Console.WriteLine("---- Cancellare una Spesa ----");

            ListSpesa(false);

            string idValue = ConsoleHelpers.GetData("ID della spesa da cancellare");

            DataRow rowToBeDeleted = SpesaDs.Tables["Spesa"].Rows.Find(idValue);
            // marco la riga come cancellata
            rowToBeDeleted?.Delete();

            Refresh();

            Console.WriteLine("---- Premi un tasto ----");
            Console.ReadKey();
        }

        private static void ListSpesa(bool v)
        {
            throw new NotImplementedException();
        }



        public static void Refresh()
        {
            // update db
            SpesaAdapter.Update(SpesaDs, "Spesa");
            // refresh ds
            SpesaDs.Reset();
            SpesaAdapter.Fill(SpesaDs, "Spesa");
        }

        public static void PrintQueryResult(IEnumerable<Spesa> result)
        {
            Console.WriteLine("Risultato");
            Console.WriteLine();
            foreach (var item in result)
            {
                Console.WriteLine("{0} {1}{2}{3}{4}",
                    item.ID, item.Date, item.Descrizione, item.Utente, item.Importo, item.Approvata);
            }
            Console.WriteLine("Premi un tasto per tornare al menù");
            Console.ReadKey();
        }
    }

}

