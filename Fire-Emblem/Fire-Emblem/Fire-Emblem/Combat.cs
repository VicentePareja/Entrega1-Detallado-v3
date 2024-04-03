using Fire_Emblem_View;
namespace Fire_Emblem

{
    public class Combat
    {
        public Player Jugador1 { get; set; }
        public Player Jugador2 { get; set; }
        private View _view;

        public Combat(Player jugador1, Player jugador2, View view)
        {
            Jugador1 = jugador1;
            Jugador2 = jugador2;
            _view = view;
        }

        public void Iniciar()
        {
            int contador = 0;
            bool run;
            run = true;
            while (run) 
            {
                contador++;
                if (contador % 2 == 1) 
                {
                    RealizarTurno(Jugador1, Jugador2, contador);
                }
                else
                {
                    RealizarTurno(Jugador2, Jugador1, contador);
                }
                run = !JuegoTerminado();
            }
            AnunciarGanador();
        }


        private void IniciarAtaqueYContraAtaque(Character unidadAtacante, Character unidadDefensora, string ventaja
            , int turno)
        {

            Atack ataque = new Atack(unidadAtacante, unidadDefensora, _view);
            ataque.RealizarAtaque(ventaja);
            
            if (unidadDefensora.HPactual > 0)
            {
                ataque.RealizarContraAtaque(ventaja);
            }
        }

        private void VerificarYRealizarFollowUp(Character unidadAtacante, Character unidadDefensora, string ventaja)
    {
        if (unidadAtacante.HPactual > 0 && unidadDefensora.HPactual > 0)
        {
            if (unidadAtacante.Spd >= unidadDefensora.Spd + 5)
            {
                new Atack(unidadAtacante, unidadDefensora, _view).RealizarAtaque(ventaja);
            }
            else if (unidadDefensora.Spd >= unidadAtacante.Spd + 5)
            {
                new Atack(unidadAtacante, unidadDefensora , _view).RealizarContraAtaque(ventaja);
            }
            else
            {
                _view.WriteLine("Ninguna unidad puede hacer un follow up");
            }
        }
    }

        private (Character unidadAtacante, Character unidadDefensora, string ventaja) PrepararAtaque(Player atacante, Player defensor, int turno)
        {
            Character unidadAtacante = EscogerUnidad(atacante);
            Character unidadDefensora = EscogerUnidad(defensor);
            _view.WriteLine($"Round {turno}: {unidadAtacante.Nombre} ({atacante.Name}) comienza");
            string ventaja = CalcularVentaja(unidadAtacante, unidadDefensora);
            ImprimirVentaja(unidadAtacante, unidadDefensora, ventaja);
    
            return (unidadAtacante, unidadDefensora, ventaja);
        }
        
        private void EjecutarCombate(Player atacante, Player defensor, Character unidadAtacante
            , Character unidadDefensora, string ventaja, int turno)
        {
            IniciarAtaqueYContraAtaque(unidadAtacante, unidadDefensora, ventaja, turno);
            VerificarYRealizarFollowUp(unidadAtacante, unidadDefensora, ventaja);

            if (unidadAtacante.HPactual <= 0)
            {
                atacante.Team.Characters.Remove(unidadAtacante);
            }

            if (unidadDefensora.HPactual <= 0)
            {
                defensor.Team.Characters.Remove(unidadDefensora);
            }
            _view.WriteLine($"{unidadAtacante.Nombre} ({unidadAtacante.HPactual}) : {unidadDefensora.Nombre} ({unidadDefensora.HPactual})");
        }
        
        private void RealizarTurno(Player atacante, Player defensor, int turno)
        {
            var (unidadAtacante, unidadDefensora, ventaja) = PrepararAtaque(atacante, defensor, turno);
            EjecutarCombate(atacante, defensor, unidadAtacante, unidadDefensora, ventaja, turno);
        }
        

        public string CalcularVentaja(Character atacante, Character defensor)
        {
            // Definir el triángulo de armas
            var ventajas = new Dictionary<string, string>
            {
                {"Sword", "Axe"},
                {"Axe", "Lance"},
                {"Lance", "Sword"}
            };

            // Comprobar si el atacante tiene ventaja
            if (ventajas.ContainsKey(atacante.Arma) && ventajas[atacante.Arma] == defensor.Arma)
            {
                return "atacante";
            }
            // Comprobar si el defensor tiene ventaja
            else if (ventajas.ContainsKey(defensor.Arma) && ventajas[defensor.Arma] == atacante.Arma)
            {
                return "defensor";
            }
            // Si ninguno tiene ventaja
            return "ninguno";
        }

        public void ImprimirVentaja(Character atacante, Character defensor, string ventaja)
        {
            switch (ventaja)
            {
                case "atacante":
                    _view.WriteLine($"{atacante.Nombre} ({atacante.Arma}) tiene ventaja con respecto a {defensor.Nombre} ({defensor.Arma})");
                    break;
                case "defensor":
                    _view.WriteLine($"{defensor.Nombre} ({defensor.Arma}) tiene ventaja con respecto a {atacante.Nombre} ({atacante.Arma})");
                    break;
                case "ninguno":
                    _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
                    break;
            }
        }

        private void ImprimirOpcionesDePersonajes(Player jugador)
        {
            // Asumiendo que "Name" es una propiedad pública de Player para obtener su nombre
            _view.WriteLine($"{jugador.Name} selecciona una opción");
            for (int i = 0; i < jugador.Team.Characters.Count; i++)
            {
                _view.WriteLine($"{i}: {jugador.Team.Characters[i].Nombre}");
            }
        }

        private Character EscogerUnidad(Player jugador)
        {
            ImprimirOpcionesDePersonajes(jugador); 
            int eleccion = -1; 
            do
            {
                string input = _view.ReadLine();
                if (int.TryParse(input, out eleccion) && eleccion >= 0 && eleccion < jugador.Team.Characters.Count)
                {
                    break;
                }
                else
                {
                    _view.WriteLine("Elección inválida. Por favor, elige de nuevo.");
                }
            } while (true); 

            return jugador.Team.Characters[eleccion];
        }


        private bool JuegoTerminado()
        {
            return Jugador1.Team.Characters.Count == 0 || Jugador2.Team.Characters.Count == 0;
        }

        private void AnunciarGanador()
        {
            if (Jugador1.Team.Characters.Count == 0)
            {
                _view.WriteLine($"{Jugador2.Name} ganó");
            }
            else if (Jugador2.Team.Characters.Count == 0)
            {
                _view.WriteLine($"{Jugador1.Name} ganó");
            }
            else
            {
                _view.WriteLine("Empate!");
            }
        }
    }
}