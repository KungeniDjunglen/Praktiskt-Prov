namespace Praktiskt_Prov
{
    public class Creature
    {
        protected int health;
        protected int damage;
        protected string name;
        protected int score;
        public Creature(int health, int damage, string name, int score){
            this.health = health;
            this.damage = damage;
            this.name = name;
            this.score = score;
        }
        public virtual bool Attack(Creature target){ //Ska returnera true om fienden har en special attack;
            target.health -= damage;
            return false;
        }
        public int Health{
            get{return health;}
            set{health = value;}
        }
        public int Damage{
            get{return damage;}
        }
        public string Name{
            get{return name;}
        }
        public int Score{
            get{return score;}
        }
        
    }
}