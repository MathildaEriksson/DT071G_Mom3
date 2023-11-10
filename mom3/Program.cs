/* Mathilda Eriksson, DT071G, HT23 */
using mom3;
// Huvudklassen för gästboksapplikationen.
// Den hanterar användarinteraktion och utför olika åtgärder baserat på användarens val.
class Program
{
    // En lista som lagrar alla inlägg lästa från filen vid programstart.
    static List<Post> posts = GuestbookHandler.ReadPosts();

    static void Main(string[] args)
    {
        bool runProgram = true;

        while (runProgram)
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine();
            Console.WriteLine();
            ShowPosts();

            switch (Console.ReadLine())
            {
                case "1":
                    AddPost();
                    break;
                case "2":
                    RemovePost();
                    break;
                case string s when s.Equals("x", StringComparison.OrdinalIgnoreCase): // Gör så att både X och x avslutar
                    runProgram = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }

            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }

    // Visar huvudmenyn
    static void ShowMenu()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("**************************************");
        Console.WriteLine("* Välkommen till Mathildas gästbok!  *");
        Console.WriteLine("**************************************");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("1. Lägg till inlägg");
        Console.WriteLine("2. Ta bort inlägg");
        Console.WriteLine("X. Avsluta");
    }

    // Visar alla inlägg i gästboken.
    static void ShowPosts()
    {
        Console.WriteLine("Inlägg i gästboken:");
        for (int i = 0; i < posts.Count; i++)
        {
            var post = posts[i];
            Console.WriteLine($"[{i}] {post.Author} - {post.Content}");
        }
        Console.WriteLine();
    }

    // Låter användaren lägga till ett nytt inlägg.
    // Kräver att författarens namn och innehåll inte är tomma.
    static void AddPost()
    {
        string author;
        do
        {
            Console.WriteLine("Ange författarens namn:");
            author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Författare får inte vara tom.");
            }
        }
        while (string.IsNullOrWhiteSpace(author));

        string content;
        do
        {
            Console.WriteLine("Innehållstext:");
            content = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Innehållstext får inte vara tom.");
            }
        }
        while (string.IsNullOrWhiteSpace(content));

        posts.Add(new Post { Author = author, Content = content });
        GuestbookHandler.SavePosts(posts);
    }

    // Låter användaren ta bort ett inlägg baserat på dess index.
    // Kontrollerar att inmatningen är ett giltigt index.
    static void RemovePost()
    {
        ShowPosts();

        Console.WriteLine("Ange index för inlägget du vill ta bort:");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < posts.Count)
        {
            posts.RemoveAt(index);
            GuestbookHandler.SavePosts(posts);
        }
        else
        {
            Console.WriteLine("Ogiltigt index.");
        }
    }
}
