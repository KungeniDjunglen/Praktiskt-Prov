using System.Security.Cryptography.X509Certificates;
using Praktiskt_Prov;

//Att göra:
// Unika karaktärsdrag
// Kommentera
// Mer än 1 drabbning
// Väldokumenterad kod
// Förbättra Enemy turn


class Program{
    List<Praktiskt_Prov.Creature> creatures;
    Player player;
    int TotalScore;
    static void Main(){
        Program program = new Program();
        program.MainMenu();
    }
    private void MainMenu(){
        Console.WriteLine("Välkommen till spelet. Välj ett av alternativen nedanför:");
        Console.WriteLine("1. Spela");
        Console.WriteLine("2. Scoreboard");

        int val = int.Parse(Console.ReadLine());
        switch (val){
            case 1: // Spelar spelet
            Setup();
            Play();
                break;
            case 2: // Skriver ut scoreboard
            ScoreBoard();
                break;
        }
    }
    private void Setup(){
        creatures = new List<Creature>();

        Console.Write("Hur många fienden vill du möta ? ");
        int EnemyAmount = int.Parse(Console.ReadLine());
        Random rng = new Random();
        for (int x = 1; x <= EnemyAmount; x++){
            int Number = rng.Next(1,4); //Slumpar mellan alla fiendetyper
            switch (Number){
                case 1:
                    creatures.Add(new Skeleton());
                    break;
                case 2:
                    creatures.Add(new Snail());
                    break;
                case 3:
                    creatures.Add(new LeifGW());
                    break;
            }
        }
    }
    private void Play(){
        Console.Clear();
        bool GameOn = true;

        player = new Player(); // Skapar spelaren
        TotalScore = 0;

        while(GameOn){
            // Börja med att skriva ut fiendens information;
            WirteEnemyInfo();

            Console.WriteLine("Välj ett av nedanstående alternativ:");
            Console.WriteLine("1. Attackera");
            Console.WriteLine("2. Skriv ut ditt liv");
            int val = int.Parse(Console.ReadLine());

            switch(val){ //Spelaren gör sitt
                case 1:
                creatures[0].Health -= player.Damage;
                if (creatures[0].Health <= 0){
                    TotalScore += creatures[0].Score;
                    creatures.RemoveAt(0);
                }
                else{
                    EnemyTurn();
                }
                    break;
                case 2:
                Console.WriteLine("Du har " + player.Health + " HP");
                Thread.Sleep(1000);
                    break;
            }

            //Fienedens tur

            if (creatures.Count < 1){
                //Du vann
                GameOn = false;
                GameOver(true);
            }
            else if (player.Health < 1){
                //Du förlorade
                GameOn = false;
                GameOver(false);
            }
            Console.Clear();
        }
    }

    private void EnemyTurn(){
        player.Health -= creatures[0].Damage;

    }

    private void WirteEnemyInfo(){
        Console.WriteLine("Du möter " + creatures[0].Name);
        Console.WriteLine("Health: " + creatures[0].Health);
        Console.WriteLine("Damage: " + creatures[0].Damage);
    }

    private void GameOver(bool WhoWon ){ // True = du vann. False = du förlorde
        if (WhoWon){
            Console.WriteLine("Grattis du vann");
        }
        if (!WhoWon){
            Console.WriteLine("Du förlorade");
        }
        StreamReader sr = new StreamReader("scoreboard.txt");
        string data = sr.ReadToEnd();
        sr.Close();
        StreamWriter sw = new StreamWriter("scoreboard.txt");
        sw.Write(data + TotalScore + ".");
        sw.Close();
    }

    private void ScoreBoard(){
        StreamReader sr = new StreamReader("scoreboard.txt"); // Läser datan
        string data = sr.ReadToEnd();
        sr.Close();

        List<int> scoreList = new List<int>();

        // Gör om datan så att den blir i en INT lista.
        char[] chars = data.ToCharArray();
        string temp = "";
        foreach (char c in chars){ 
            if(c == '.'){
                scoreList.Add(int.Parse(temp));
                temp = "";
            }
            else{
                temp += c;
            }
        }

        //Sorterar datan
        scoreList.Sort();
        scoreList.Reverse();
        for (int x = 0; x < 3 && scoreList.Count > x; x++ ){
            Console.WriteLine(scoreList[x]);
        }
    }
}