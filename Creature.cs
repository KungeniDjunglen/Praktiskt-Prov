namespace Praktiskt_Prov
{
    public abstract class Creature
    {
        protected int health;
        protected int damage;
        protected string name;
        protected int score;
        public Creature(int health, int damage, string name, int score){ //Detta information kommer från subklasserna
            this.health = health;
            this.damage = damage;
            this.name = name;
            this.score = score;
        }
        public virtual bool Attack(Creature target){ //Ska returnera true om fienden använder en special attack. Bara sniglen har det för tillfället.
            target.health -= damage;
            return false;
        }
        //Inkapslingsdelen. Bara sådant som används på andra ställen ges tillgång till.
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