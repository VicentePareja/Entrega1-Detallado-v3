using Fire_Emblem_View;
namespace Fire_Emblem
{
    public class Battle
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        private View _view;

        public Battle(Player player1, Player player2, View view)
        {
            Player1 = player1;
            Player2 = player2;
            _view = view;
        }

        public void Start()
        {
            int counter = 0;
            bool run = true;
            while (run) 
            {
                counter++;
                if (counter % 2 == 1) 
                {
                    PerformTurn(Player1, Player2, counter);
                }
                else
                {
                    PerformTurn(Player2, Player1, counter);
                }
                run = !IsGameFinished();
            }
            AnnounceWinner();
        }
        

        private (Character attacker, Character defender, string advantage) PrepareAttack(Player attacker, Player defender, int turn)
        {
            Character attackerUnit = ChooseUnit(attacker);
            Character defenderUnit = ChooseUnit(defender);
            _view.WriteLine($"Round {turn}: {attackerUnit.Name} ({attacker.Name}) comienza");
            string advantage = CalculateAdvantage(attackerUnit, defenderUnit);
            PrintAdvantage(attackerUnit, defenderUnit, advantage);
    
            return (attackerUnit, defenderUnit, advantage);
        }
        
        
        private void PerformTurn(Player attacker, Player defender, int turn)
        {
            var (attackerUnit, defenderUnit, advantage) = PrepareAttack(attacker, defender, turn);
            Combat combat = new Combat(attackerUnit, defenderUnit, advantage, _view);
            combat.Start();
            
            if (attackerUnit.CurrentHP <= 0)
            {
                attacker.Team.Characters.Remove(attackerUnit);
            }
            if (defenderUnit.CurrentHP <= 0)
            {
                defender.Team.Characters.Remove(defenderUnit);
            }
        }

        

        public string CalculateAdvantage(Character attacker, Character defender)
        {
            var advantages = new Dictionary<string, string>
            {
                {"Sword", "Axe"},
                {"Axe", "Lance"},
                {"Lance", "Sword"}
            };

            if (advantages.ContainsKey(attacker.Weapon) && advantages[attacker.Weapon] == defender.Weapon)
            {
                return "atacante";
            }
            else if (advantages.ContainsKey(defender.Weapon) && advantages[defender.Weapon] == attacker.Weapon)
            {
                return "defensor";
            }
            return "ninguno";
        }

        public void PrintAdvantage(Character attacker, Character defender, string advantage)
        {
            switch (advantage)
            {
                case "atacante":
                    _view.WriteLine($"{attacker.Name} ({attacker.Weapon}) tiene ventaja con respecto a {defender.Name} ({defender.Weapon})");
                    break;
                case "defensor":
                    _view.WriteLine($"{defender.Name} ({defender.Weapon}) tiene ventaja con respecto a {attacker.Name} ({attacker.Weapon})");
                    break;
                default:
                    _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
                    break;
            }
        }

        private void PrintCharacterOptions(Player player)
        {
            _view.WriteLine($"{player.Name} selecciona una opción");
            for (int i = 0; i < player.Team.Characters.Count; i++)
            {
                _view.WriteLine($"{i}: {player.Team.Characters[i].Name}");
            }
        }

        private Character ChooseUnit(Player player)
        {
            PrintCharacterOptions(player); 
            int choice = -1; 
            do
            {
                string input = _view.ReadLine();
                if (int.TryParse(input, out choice) && choice >= 0 && choice < player.Team.Characters.Count)
                {
                    break;
                }
                else
                {
                    _view.WriteLine("Elección inválida. Por favor, elige de nuevo.");
                }
            } while (true); 

            return player.Team.Characters[choice];
        }


        private bool IsGameFinished()
        {
            return Player1.Team.Characters.Count == 0 || Player2.Team.Characters.Count == 0;
        }

        private void AnnounceWinner()
        {
            if (Player1.Team.Characters.Count == 0)
            {
                _view.WriteLine($"{Player2.Name} ganó");
            }
            else if (Player2.Team.Characters.Count == 0)
            {
                _view.WriteLine($"{Player1.Name} ganó");
            }
            else
            {
                _view.WriteLine("Empate!");
            }
        }
    }
}
