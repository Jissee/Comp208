﻿namespace EoE.War.Interface
{
    public interface IClientTreatyList
    {
        void AddTreatyList(string signName);
        void ChangeTreatyList(string[] signName);
        void RemoveTreatyList(string signName);
    }
}
