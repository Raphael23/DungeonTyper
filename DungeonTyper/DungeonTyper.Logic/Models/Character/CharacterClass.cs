﻿using DungeonTyper.Common.Utils;
using DungeonTyper.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonTyper.Logic.Models
{
    public class CharacterClass : ICharacterClass
    {
        public string ClassName { get; set; }
        public List<IAbility> ClassAbilities { get; set; }

        // Create class for each characterclass in the game.

        public CharacterClass()
        {
      //      IAbilityDataAccess abilityDataAccess = new AbilityDataAccess();
        }
    }
}