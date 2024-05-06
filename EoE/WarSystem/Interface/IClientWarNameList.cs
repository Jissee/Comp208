﻿namespace EoE.WarSystem.Interface
{
    public interface IClientWarNameList
    {
        List<string> WarNameList { get; set; }
        void ChangeWarName(string warName);
        void ChangeWarNames(string[] warNames);
    }
}
