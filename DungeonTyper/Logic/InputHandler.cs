﻿using System;
using DungeonTyper.Interfaces;

namespace DungeonTyper.Logic
{
    public class InputHandler 
    {
        public string Handle(string input)
        {
            string output;

            if(input == "attack")
            {
                output = "You attack!";   
            }
            else
            {
               output  = "You didn't attack.";
            }

            return output;
        }
    }
}