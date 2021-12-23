using SupportLibraryGestioneSpeseDisconnected;

bool quit = false;
do
{
    string command = ConsoleHelpers.BuildMenu("Main Menu",
        new List<string> {
                        "[ 1 ] - Elimina una Spesa",
                        "[ 2 ] - Visualizza la spesa per utente",
                        "[ q ] - QUIT"
        });

    switch (command)
    {
        case "1":
           
            DisconnectedModeClient.DeleteTicket();
            break;
        case "2":
            
            DisconnectedModeClient.GroupByUtente();
            break;
        case "q":
            quit = true;
            break;
        default:
            Console.WriteLine("Comando sconosciuto.");
            break;
    }

} while (!quit);
