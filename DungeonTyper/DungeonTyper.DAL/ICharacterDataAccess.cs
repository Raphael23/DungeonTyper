﻿using DungeonTyper.Common.Models;

namespace DungeonTyper.DAL
{
    public interface ICharacterDataAccess
    {
        int CreateCharacter(string characterClass, string characterName = "TempName");
        ICharacterCommon GetCharacterById(int id);
        string GetCountCharacterAliveAndDead();
    }
}