using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    class MineCell
    {
        public enum Flags
        {
            Hidden = 0,
            IsMine = 1,
            IsFlagged = 2,
            IsPopped = 4,
        }
        public int Neighbours = 0;

        private Flags _settings;
        public Flags Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }
        public MineCell() { }
        public MineCell(Flags settings)
        {
            Settings = settings;
        }

        public bool Pop()
        {
            if ((Settings & Flags.IsFlagged) != 0) //check if its flagged
            {
                return false;
            }
            Settings |= Flags.IsPopped;
            return (Settings & Flags.IsMine) == 0; //returns true if theres no mine
        }

        public bool ToggleFlag()
        {
            Settings ^= Flags.IsFlagged;
            return (Settings & Flags.IsFlagged) != 0; //returns true if the flag was added
        }

        public override String ToString()
        {
            String str;
            //str = Settings.ToString();
            if (Settings.HasFlag(Flags.IsMine) && Settings.HasFlag(Flags.IsPopped)) //clicked mine
            {
                str = "#";
            }
            else if (Settings == Flags.IsMine) //if its only a mine
            {
                str = "█";
            }
            else if (Settings == Flags.Hidden) //if its hidden
            {
                str = "█";
            }
            else if ((Settings & Flags.IsFlagged) != 0) //check for flag
            {
                str = "?";
            }
            else // else print number of neighbours
            {
                str = Neighbours > 0 ? Neighbours.ToString() : " ";
            }
            //str = Settings == Flags.Hidden || Settings == Flags.IsMine ? "█"  : (Settings & Flags.IsFlagged) != 0 ? "?" : Neighbours.ToString();
            return str;
        }
    }
}
