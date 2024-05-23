namespace Praktiskt_Prov
{
    public class Snail: Creature
    {
        private int SpecialAttackChance;
        public Snail(): base(50, 10, "Giant Snail", 150){ //Sniglen stats
            Random rng = new Random();
            SpecialAttackChance = rng.Next(10,50); //ger oss ett värde mellan 10 och 50. Varje snigel är olika farlig med sitt slem.
        }
        public override bool Attack(Creature target)
        {
            Random rng = new Random();
            int RandomNumber = rng.Next(0,100);
            // OM sniglen träffar med sitt slem blir spelaren förlamad och kan inte göra något nästa runda.
            if(SpecialAttackChance >= RandomNumber){ // ger sniglen en random chans att träffa med sitt snigelslem
                Console.WriteLine("Du har blivet snigelslemmad! Du är förlamad i en runda");
                target.Health -= damage;
                return true;
            }
            else{ // Om sniglen missar slemmet gör den en vanlig attack
                return base.Attack(target);

            }
        }
    }
}