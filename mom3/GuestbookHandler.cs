/* Mathilda Eriksson, DT071G, HT23 */

using System.Text.Json;

namespace mom3
{
    /* En statisk klass som hanterar lagring och hämtning av gästboksinlägg.
       Denna klass använder JSON för att serialisera och deserialisera inlägg
       och sparar dem i en fil */
    public static class GuestbookHandler
    {
        // Sökväg till filen där gästboksinlägg lagras
        private static string filepath = "guestbook.json";

        /* Sparar en lista av inlägg till en JSON-fil.
           Tar en lista av Post-objekt som argument och serialiserar den till JSON.
           Skriver sedan den serialiserade strängen till en fil.*/
        public static void SavePosts(List<Post> post)
        {
            string json = JsonSerializer.Serialize(post, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, json);
        }

        /* Läser inlägg från en JSON-fil och returnerar dem som en lista.
           Kontrollerar först om filen existerar och skapar en ny tom lista om den inte gör det.
           Om filen finns, deserialiserar den JSON-data till en lista av Post-objekt.*/
        public static List<Post> ReadPosts()
        {
            if (!File.Exists(filepath))
            {
                return new List<Post>();
            }

            string json = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<Post>>(json);
        }
    }
}
