using EoE.Server.WarSystem;
using EoE.WarSystem.Interface;
using System.Windows;

namespace EoE.Client.WarSystem
{
    public class ClientWarTargetList : IClientWarTargetList
    {
        public Dictionary<string, WarTarget> WarTargetList { get; set; }
        public ClientWarTargetList()
        {
            WarTargetList = new Dictionary<string, WarTarget>();
        }
        public void ChangeClaim(string name, WarTarget warTarget)
        {
            if (WarTargetList.ContainsKey(name))
            {
                WarTargetList[name] = warTarget;
            }
            else
            {
                WarTargetList.Add(name, warTarget);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                WarDetail window = WindowManager.INSTANCE.GetWindows<WarDetail>();
                window.Silicon.Text = warTarget.SiliconClaim.ToString();
                window.Copper.Text = warTarget.CopperClaim.ToString();
                window.Iron.Text = warTarget.IronClaim.ToString();
                window.Aluminum.Text = warTarget.AluminumClaim.ToString();
                window.Electronic.Text = warTarget.ElectronicClaim.ToString();
                window.Industrial.Text = warTarget.IndustrialClaim.ToString();
                window.Blocks.Text = warTarget.FieldClaim.ToString();
                window.Population.Text = warTarget.PopClaim.ToString();
            });
        }
    }
}
