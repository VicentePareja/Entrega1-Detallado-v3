using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Fire_Emblem_View;

namespace Fire_Emblem
{
    public class LogicaSetUp
    {
        private View _view;
        private string _teamsFolder;
        private List<Character> characters;
        private List<Skill> skills;
        private Player _player1;
        private Player _player2;

        public LogicaSetUp(View view, string teamsFolder)
        {
            _view = view;
            _teamsFolder = teamsFolder;
            characters = new List<Character>();
            
        }

        public bool CargarEquipos(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
    
            _view.WriteLine("Elige un archivo para cargar los equipos");

            var files = Directory.GetFiles(_teamsFolder, "*.txt");
            if (files.Length == 0)
            {
                _view.WriteLine("No hay archivos disponibles.");
                return false;
            }

            for (int i = 0; i < files.Length; i++)
            {
                _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");
            }

            string input = _view.ReadLine();
            if (int.TryParse(input, out int fileIndex) && fileIndex >= 0 && fileIndex < files.Length)
            {
                string selectedFile = files[fileIndex];
                string content = File.ReadAllText(selectedFile);
        
                // Llama a ImportarCharacters para cargar los personajes desde el JSON
                ImportarCharacters();
                ImportarSkills();
        
                // Ahora llama a ChooseCharacters pasándole la ruta del archivo seleccionado
                if (ValidTeams(selectedFile))
                {
                    ChooseCharacters(selectedFile);
                    return true;
                }
                else
                {
                    _view.WriteLine("Archivo de equipos no válido");
                    return false;
                }
                
            }
            else
            {
                _view.WriteLine("Selección inválida.");
                return false;
            }
        }

        public bool ValidTeams(string selectedFile)
{
    var lines = File.ReadAllLines(selectedFile);
    bool isPlayer1 = true; // Inicialmente asigna personajes al Jugador 1

    Team team1 = new Team();
    Team team2 = new Team();
    bool team1Populated = false; // Indica si se han añadido personajes al equipo del Jugador 1
    bool team2Populated = false; // Indica si se han añadido personajes al equipo del Jugador 2

    List<string> currentTeamNames = new List<string>();

    foreach (var line in lines)
    {
        if (line == "Player 1 Team")
        {
            // Al cambiar a equipo del Jugador 1, verifica y limpia el equipo del Jugador 2 si necesario
            if (!isPlayer1 && currentTeamNames.Any())
            {
                team2Populated = true; // Marca que se han añadido personajes al equipo del Jugador 2
                bool valid = ValidateAndClearCurrentTeam(currentTeamNames, team2);
                if (!valid) return false;
                currentTeamNames.Clear();
            }
            isPlayer1 = true;
        }
        else if (line == "Player 2 Team")
        {
            // Al cambiar a equipo del Jugador 2, verifica y limpia el equipo del Jugador 1 si necesario
            if (isPlayer1 && currentTeamNames.Any())
            {
                team1Populated = true; // Marca que se han añadido personajes al equipo del Jugador 1
                bool valid = ValidateAndClearCurrentTeam(currentTeamNames, team1);
                if (!valid) return false;
                currentTeamNames.Clear();
            }
            isPlayer1 = false;
        }
        else
        {
            currentTeamNames.Add(line); // Añade nombres (y habilidades) a la lista temporal
        }
    }

    // Verificación final para el último equipo
    if (currentTeamNames.Any())
    {
        if (isPlayer1)
        {
            team1Populated = true; // Marca que se han añadido personajes al equipo del Jugador 1
        }
        else
        {
            team2Populated = true; // Marca que se han añadido personajes al equipo del Jugador 2
        }
        bool valid = ValidateAndClearCurrentTeam(currentTeamNames, isPlayer1 ? team1 : team2);
        if (!valid) return false;
    }

    // Verifica que ambos equipos hayan sido poblados con al menos un personaje
    if (!team1Populated || !team2Populated)
    {
        return false;
    }

    return true; // Ambos equipos son válidos si pasaron las verificaciones
}



        
        private bool ValidateAndClearCurrentTeam(List<string> characterNames, Team team)
        {
            foreach (var fullCharacterLine in characterNames)
            {
                // Separa el nombre del personaje de sus habilidades (si las hay)
                var parts = fullCharacterLine.Split(" (", 2);
                var characterName = parts[0];
                var skillsText = parts.Length > 1 ? parts[1].TrimEnd(')') : string.Empty;
        
                // Crea una instancia de Character
                var character = new Character { Nombre = characterName };

                // Procesa y añade las habilidades si existen
                if (!string.IsNullOrEmpty(skillsText))
                {
                    var skillNames = skillsText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var skillName in skillNames)
                    {
                        var trimmedSkillName = skillName.Trim();
                        character.AddSkill(new Skill(trimmedSkillName, "Descripción no proporcionada")); // Asume descripción genérica
                    }
                }

                // Añade el personaje al equipo temporal
                team.Characters.Add(character);
            }

            // Verifica si el equipo es válido
            bool isValid = team.EsEquipoValido();

            // Limpia el equipo después de la validación para reutilizarlo
            team.Characters.Clear();

            return isValid;
        }



        public void ChooseCharacters(string selectedFilePath)
        {
            var lines = File.ReadAllLines(selectedFilePath);
            bool isPlayer1 = true; // Comienza asignando personajes al equipo del jugador 1

            foreach (var line in lines)
            {
                if (line == "Player 1 Team")
                {
                    isPlayer1 = true;
                }
                else if (line == "Player 2 Team")
                {
                    isPlayer1 = false;
                }
                else
                {
                    AssignCharacterToTeam(line, isPlayer1 ? _player1.Team : _player2.Team);
                }
            }
        }


        private void AssignCharacterToTeam(string characterLine, Team team)
        {
            var parts = characterLine.Split(" (", 2);
            var characterName = parts[0];
            var skillsText = parts.Length > 1 ? parts[1].TrimEnd(')') : string.Empty;

            var originalCharacter = characters.FirstOrDefault(c => c.Nombre == characterName);
            if (originalCharacter != null)
            {
                var newCharacter = new Character
                {
                    Nombre = originalCharacter.Nombre,
                    Arma = originalCharacter.Arma,
                    Género = originalCharacter.Género,
                    HPmáximo = originalCharacter.HPmáximo,
                    // Aquí se asigna HPactual al valor de HPmáximo
                    HPactual = originalCharacter.HPmáximo, // Actualizado para igualar a HPmáximo
                    Atk = originalCharacter.Atk,
                    Spd = originalCharacter.Spd,
                    Def = originalCharacter.Def,
                    Res = originalCharacter.Res,
                };

                // Procesa y añade las habilidades
                if (!string.IsNullOrEmpty(skillsText))
                {
                    var skillNames = skillsText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var skillName in skillNames)
                    {
                        var trimmedSkillName = skillName.Trim();
                        // Crear e incluir habilidades según sea necesario
                        newCharacter.AddSkill(new Skill(trimmedSkillName, "Descripción no proporcionada"));
                    }
                }

                team.Characters.Add(newCharacter);
            }
            else
            {
                _view.WriteLine($"Personaje no encontrado: {characterName}");
            }
        }




        public void ImportarCharacters()
        {
            // Ruta corregida al archivo characters.json
            string jsonPath = Path.Combine(_teamsFolder, "../..", "characters.json"); // Sube dos niveles en la jerarquía de directorios

            try
            {
                // Lee el archivo JSON
                string jsonString = File.ReadAllText(jsonPath);

                // Configura JsonSerializerOptions para usar el convertidor personalizado
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new StringToIntConverter() }
                };

                // Deserializa el contenido usando las opciones configuradas
                characters = JsonSerializer.Deserialize<List<Character>>(jsonString, options);
            }
            catch (Exception ex)
            {
                _view.WriteLine($"Error al importar personajes: {ex.Message}");
            }
        }
        
        public void ImportarSkills()
        {
            string jsonPath = Path.Combine(_teamsFolder, "../..", "skills.json"); // Sube dos niveles en la jerarquía de directorios

            try
            {
                // Lee el archivo JSON
                string jsonString = File.ReadAllText(jsonPath);

                // No es necesario el convertidor personalizado para habilidades si solo contienen cadenas y no números en formato de texto
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserializa el contenido del archivo en la lista de habilidades
                skills = JsonSerializer.Deserialize<List<Skill>>(jsonString, options);

                // O, si usas un diccionario:
                // skillsByName = JsonSerializer.Deserialize<List<Skill>>(jsonString, options)
                //     .ToDictionary(skill => skill.Name, skill => skill);

                //Console.WriteLine("Habilidades importadas correctamente.");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error al importar habilidades: {ex.Message}");
            }
        }



    }
}