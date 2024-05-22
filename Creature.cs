namespace Praktiskt_Prov
{
    public class Creature
    {
        public int Health;
        public int Damage;
        public string Name;
        public int Score;
        public Creature(int health, int Damage, string name, int score){
            this.Health = health;
            this.Damage = Damage;
            Name = name;
            Score = score;
        }
    }
}