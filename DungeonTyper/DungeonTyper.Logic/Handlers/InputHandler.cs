﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DungeonTyper.DAL;
using DungeonTyper.Common.Utils;
using DungeonTyper.Common.Models;
using System.Linq;

namespace DungeonTyper.Logic.Handlers
{
    public class InputHandler : IInputHandler
    {
        private List<ICharacterClass> _allCharacterClasses = new List<ICharacterClass>();
        private string _input;

        private readonly IOutputHandler _outputHandler;
        private readonly IAbilityDataAccess _abilityDataAccess;
        private readonly ICharacterClassDataAccess _characterClassDataAccess;
        private readonly ICharacterDataAccess _characterDataAccess;

        // Here i construct the inputhandler and give it the outputhandler so that outputhandler does not rely on the inputhandler, but the inputhandler does need the outputhandler interface. This is because the outputhandler is at a higher level.
        public InputHandler(
            IOutputHandler outputhandler, 
            IAbilityDataAccess abilityDataAccess, 
            ICharacterClassDataAccess characterClassDataAccess,
            ICharacterDataAccess characterDataAccess)
        {
            _outputHandler = outputhandler;
            _abilityDataAccess = abilityDataAccess;
            _characterClassDataAccess = characterClassDataAccess;
            _characterDataAccess = characterDataAccess;
        }

        public void HandleInput(string input)
        {
            _input = input;

            if (string.IsNullOrWhiteSpace(_input))
            {
                return;
            }

            _outputHandler.HandleOutput(_input);

            // CHECKS BELOW THIS COMMENT
            if (CheckInputCaseInsensitive(_characterClassDataAccess.GetAllCharacterClasses().Select(c => c.ClassName).ToList()))
            {
                CreateNewCharacter();
            }          
            else if (CheckInputCaseInsensitive("Sit"))
            {
                _outputHandler.HandleOutput("You sit down.");
            }
            else if (CheckInputCaseInsensitive("All Abilities"))
            {
                DisplayAllAbilities();
            }
        }
        private void DisplayAllAbilities()
        {
            List<string> allAbilities = _abilityDataAccess.GetAllAbilities_CharacterClass();
            _outputHandler.HandleOutput(allAbilities);
        }

        public void CreateNewCharacter()
        {
            _allCharacterClasses = _characterClassDataAccess.GetAllCharacterClasses();
            // Check for valid input here
            if (InputIsValidCharacterClass())
            {
                // newCharacter.SetCharacterClass(characterClassDataAccess.GetCharacterClass(_input));
                int characterId = _characterDataAccess.CreateCharacter(_input);
                ICharacter character = _characterDataAccess.GetCharacterById(characterId);
                _outputHandler.HandleOutput("You chose: " + character.CharacterClass.ClassName);
            }
            else
            {
                _outputHandler.HandleOutput("That's not a valid class.");
            }
        }

        private bool InputIsValidCharacterClass()
        {
            foreach (var c in _allCharacterClasses)
            {
                if (_input == c.ClassName) return true;
            }
            return false;
        }

        private bool CheckInputCaseInsensitive(string expectation)
        {
            return String.Equals(_input, expectation, StringComparison.OrdinalIgnoreCase);
        }

        private bool CheckInputCaseInsensitive(List<string> expectations)
        {
            foreach (string expectation in expectations)
            {
                if(String.Equals(_input, expectation, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
