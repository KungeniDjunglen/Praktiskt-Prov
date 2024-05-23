using Praktiskt_Prov;


class Program{
    List<Praktiskt_Prov.Creature> creatures;
    Player player;
    int TotalScore;
    static void Main(){
        //Skapar ett objekt (fick felmedelande då Main metoden är static) och sedan öppnar MainMenu.
        Program program = new Program();
        program.MainMenu();
    }
    private void MainMenu(){
        Console.WriteLine("Välkommen till spelet. Välj ett av alternativen nedanför:"); //Skriver ut val som spelaren kan välja mellan.
        Console.WriteLine("1. Spela");
        Console.WriteLine("2. Scoreboard");
        Console.WriteLine("3. Avsluta");

        int val = int.Parse(Console.ReadLine()); //Tar spelaren val och parsar datan
        switch (val){ //Skickar spelaren dit den har valt.
            case 1: // Spelar spelet
            Setup(); //Settar upp lite grejer som fienderna
            Play(); //Nu spelar du faktiskt spelet.
                break;
            case 2: // Skriver ut scoreboard
            ScoreBoard();
                break;
            case 3: // Avslutar programmet. (Ska vara tom)
                break;
        }
    }
    private void Setup(){
        creatures = new List<Creature>();

        //Skapar fienderna
        Console.Write("Hur många fienden vill du möta ? ");
        int EnemyAmount = int.Parse(Console.ReadLine());
        Random rng = new Random();
        for (int x = 1; x <= EnemyAmount; x++){
            int Number = rng.Next(1,4); //Slumpar mellan alla fiendetyper
            switch (Number){
                case 1:
                    creatures.Add(new Skeleton()); //Lägger till ett skellet i fiendelistan
                    break;
                case 2:
                    creatures.Add(new Snail()); //Lägger till en snigel i fiendelistan
                    break;
                case 3:
                    creatures.Add(new LeifGW()); // Lägger till Leif GW Persson i fiendelistan.
                    break;
            }
        }
    }
    private void Play(){
        Console.Clear(); //Clearar konsollen
        bool GameOn = true; //Sätter igen gameloopen

        player = new Player(); // Skapar spelaren
        TotalScore = 0; //Resettar poängen

        while(GameOn){ //Här är gameloopen.
            // Börja med att skriva ut fiendens information;
            WirteEnemyInfo();

            Console.WriteLine("Välj ett av nedanstående alternativ:"); //Ger dig ett val att välja på.
            Console.WriteLine("1. Attackera");
            Console.WriteLine("2. Skriv ut ditt liv");
            int val = int.Parse(Console.ReadLine());

            switch(val){ //Spelaren gör sitt
                case 1:
                    creatures[0].Health -= player.Damage;
                    if (creatures[0].Health <= 0){       // Dödar fienden om de ska dö. Dör fienden få spelaren köra igen.
                        TotalScore += creatures[0].Score;
                        creatures.RemoveAt(0);
                    }
                    else{ 
                        EnemyTurn();
                    }
                    break;
                case 2: //val två skriver ut spelaren HP
                    Console.WriteLine("Du har " + player.Health + " HP");
                    Thread.Sleep(1000);
                    break;
            }

            //Kollar om någon har vunnit
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
        bool attack = creatures[0].Attack(player); // Attakerar spelaren
        if(attack){ //Spelaren har blivit träffad av special attack. Bara sniglen har en sådan och du blir förlamad i 1 runda.
            Thread.Sleep(5000);
            EnemyTurn();
        }

    }

    private void WirteEnemyInfo(){ // skriver ut informationen om fienden.
        Console.WriteLine("Du möter " + creatures[0].Name);
        Console.WriteLine("Health: " + creatures[0].Health);
        Console.WriteLine("Damage: " + creatures[0].Damage);
    }

    private void GameOver(bool WhoWon ){ // True = du vann. False = du förlorde
        if (WhoWon){ // skriver ut vem som vann.
            Console.WriteLine("Grattis du vann");
        }
        if (!WhoWon){
            Console.WriteLine("Du förlorade");
        }
        Console.WriteLine("Du fick " + TotalScore + " poäng"); // skriver ut din poäng.

        // Hämtar tidigare sparad data
        StreamReader sr = new StreamReader("scoreboard.txt");
        string data = sr.ReadToEnd();
        sr.Close();

        // Skriver ut ny data (dina poäng) och även den gammla.
        StreamWriter sw = new StreamWriter("scoreboard.txt");
        sw.Write(data + TotalScore + ".");
        sw.Close();

        //Frågar spelaren om den vill köra igen.
        Console.Write("Vill du spela igen (y/n) ? ");
        string val = Console.ReadLine().ToLower();
        if(val == "y"){
            MainMenu();
        }
    }

    private void ScoreBoard(){ //Läser ut scoreboarden från en sparad fil
        StreamReader sr = new StreamReader("scoreboard.txt"); // Läser scoreboard
        string data = sr.ReadToEnd();
        sr.Close();

        List<int> scoreList = new List<int>(); // skapar en lista

        // Gör om datan så att den blir i en INT lista.
        char[] chars = data.ToCharArray();
        string temp = ""; //Skapar en temporär tom string
        foreach (char c in chars){ // går igenom hela char arrayen
            if(c == '.'){ // OM den stöter på en punkt så parsar den temp och lägger in det i int listan.
                scoreList.Add(int.Parse(temp));
                temp = ""; // gör temp tom igen.
            }
            else{ // om den stöter på en icke punkt så lägger den till nuffran i temp
                temp += c;
            }
        }

        //Sorterar datan
        scoreList.Sort();
        scoreList.Reverse(); //Reversar datan

        //Skriver ut scoreboarden
        for (int x = 0; x < 3 && scoreList.Count > x; x++ ){
            Console.WriteLine(scoreList[x]);
        }
        MainMenu();
    }
}